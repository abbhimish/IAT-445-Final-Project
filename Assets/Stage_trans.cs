using UnityEngine;
using System.Collections.Generic;

public class Stage1To2Controller : MonoBehaviour
{
    [Header("Terrain Settings")]
    public Terrain terrain;
    public int originalTextureIndex = 2;
    public int targetTextureIndex = 4;
    public float blendDuration = 3f;

    [Header("Water Settings")]
    public Transform[] waterPlanes;
    public float waterLowerDistance = 5f;
    public float waterLowerDuration = 3f;

    [Header("Prefab Control")]
    public GameObject[] objectsToHide;
    public GameObject[] objectsToShow;
    public GameObject[] replaceFrom;
    public GameObject[] replaceTo;

    [Header("Stage Trigger")]
    public KeyCode triggerKey = KeyCode.W;

    private float[,,] originalMap, backupMap;
    private int width, height, layers;
    private bool isBlending = false;
    private float blendTimer = 0f;

    private bool isLoweringWater = false;
    private float waterTimer = 0f;
    private Vector3[] originalWaterPositions;

    private Dictionary<GameObject, bool> originalActiveStates = new();
    private List<GameObject> spawnedPrefabs = new();
    private List<(GameObject prefab, Vector3 position, Quaternion rotation)> replacedPrefabs = new();

    void Start()
    {
        var terrainData = terrain.terrainData;
        width = terrainData.alphamapWidth;
        height = terrainData.alphamapHeight;
        layers = terrainData.alphamapLayers;

        originalMap = terrainData.GetAlphamaps(0, 0, width, height);
        backupMap = originalMap;

        originalWaterPositions = new Vector3[waterPlanes.Length];
        for (int i = 0; i < waterPlanes.Length; i++)
            if (waterPlanes[i] != null)
                originalWaterPositions[i] = waterPlanes[i].position;

        foreach (var obj in objectsToHide)
            if (obj != null) originalActiveStates[obj] = obj.activeSelf;

        foreach (var obj in objectsToShow)
        {
            if (obj != null)
            {
                originalActiveStates[obj] = false;
                obj.SetActive(false);
            }
        }

        foreach (var obj in replaceFrom)
            if (obj != null) originalActiveStates[obj] = obj.activeSelf;
    }

    void Update()
    {
        if (Input.GetKeyDown(triggerKey) && !isBlending)
        {
            ApplyStage2Changes();
        }

        if (isBlending)
        {
            blendTimer += Time.deltaTime;
            float t = Mathf.Clamp01(blendTimer / blendDuration);

            float[,,] blendedMap = new float[width, height, layers];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int l = 0; l < layers; l++)
                    {
                        if (originalMap[x, y, originalTextureIndex] > 0)
                        {
                            blendedMap[x, y, targetTextureIndex] = Mathf.Lerp(originalMap[x, y, targetTextureIndex], 1f, t);
                            blendedMap[x, y, originalTextureIndex] = 1f - blendedMap[x, y, targetTextureIndex];
                        }
                        else
                        {
                            blendedMap[x, y, l] = originalMap[x, y, l];
                        }
                    }
                }
            }

            terrain.terrainData.SetAlphamaps(0, 0, blendedMap);
            if (t >= 1f) isBlending = false;
        }

        if (isLoweringWater)
        {
            waterTimer += Time.deltaTime;
            float t = Mathf.Clamp01(waterTimer / waterLowerDuration);

            for (int i = 0; i < waterPlanes.Length; i++)
            {
                if (waterPlanes[i] != null)
                {
                    Vector3 start = originalWaterPositions[i];
                    Vector3 end = start + Vector3.down * waterLowerDistance;
                    waterPlanes[i].position = Vector3.Lerp(start, end, t);
                }
            }

            if (t >= 1f) isLoweringWater = false;
        }
    }

    void ApplyStage2Changes()
    {
        isBlending = true;
        blendTimer = 0f;

        isLoweringWater = true;
        waterTimer = 0f;

        for (int i = 0; i < replaceFrom.Length && i < replaceTo.Length; i++)
        {
            if (replaceFrom[i] != null && replaceTo[i] != null)
            {
                Vector3 pos = replaceFrom[i].transform.position;
                Quaternion rot = replaceFrom[i].transform.rotation;

                replacedPrefabs.Add((replaceFrom[i], pos, rot));

                StageMoveEffect moveOut = replaceFrom[i].AddComponent<StageMoveEffect>();
                moveOut.duration = 1f;
                moveOut.moveDistance = 5f;
                moveOut.destroyOnComplete = true;
                moveOut.MoveDown();

                GameObject newObj = Instantiate(replaceTo[i], pos, rot);
                StageMoveEffect moveIn = newObj.AddComponent<StageMoveEffect>();
                moveIn.duration = 1f;
                moveIn.moveDistance = 5f;
                moveIn.MoveUp();

                spawnedPrefabs.Add(newObj);
            }
        }

        foreach (var obj in objectsToHide)
        {
            if (obj != null)
            {
                StageMoveEffect mover = obj.GetComponent<StageMoveEffect>() ?? obj.AddComponent<StageMoveEffect>();
                mover.duration = 1f;
                mover.moveDistance = 5f;
                mover.MoveDown();
            }
        }

        foreach (var obj in objectsToShow)
        {
            if (obj != null)
            {
                obj.SetActive(true);
                StageMoveEffect mover = obj.GetComponent<StageMoveEffect>() ?? obj.AddComponent<StageMoveEffect>();
                mover.duration = 1f;
                mover.moveDistance = 5f;
                mover.MoveUp();
            }
        }
    }

    void OnApplicationQuit()
    {
        if (terrain != null)
            terrain.terrainData.SetAlphamaps(0, 0, backupMap);

        for (int i = 0; i < waterPlanes.Length; i++)
        {
            if (waterPlanes[i] != null)
                waterPlanes[i].position = originalWaterPositions[i];
        }

        foreach (var kvp in originalActiveStates)
        {
            if (kvp.Key != null)
                kvp.Key.SetActive(kvp.Value);
        }

        foreach (var obj in spawnedPrefabs)
        {
            if (obj != null)
                DestroyImmediate(obj);
        }

        foreach (var entry in replacedPrefabs)
        {
            GameObject newOld = Instantiate(entry.prefab, entry.position, entry.rotation);
            newOld.name = entry.prefab.name + "_Restored";
        }
    }
}