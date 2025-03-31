using UnityEngine;
using Sydewa; // 如果 LightingManager 在 Sydewa 命名空间下需要这个

public class StageController : MonoBehaviour
{
    [Header("Lighting")]
    public LightingManager lightingManager;

    [Header("Time Settings")]
    public float startTime = 3.5f;
    public float targetTime = 8f;
    public float transitionDuration = 5f;

    [Header("Trigger Settings")]
    public KeyCode triggerKey = KeyCode.F1;
    public Triggerscript triggerScript;

    [Header("Terrain Settings")]
    public Terrain terrain;
    public int originalTextureIndex = 2;
    public int targetTextureIndex = 4;
    public float blendDuration = 3f;

    private bool isTransitioning = false;
    private float transitionTimer = 0f;

    void Start()
    {
        if (lightingManager != null)
        {
            lightingManager.IsDayCycleOn = false;
            lightingManager.TimeOfDay = startTime;
        }

        if (triggerScript == null)
        {
            triggerScript = FindObjectOfType<Triggerscript>();
        }

        if (terrain == null)
        {
            terrain = FindObjectOfType<Terrain>();
        }
    }

    void Update()
    {
        bool shouldTrigger = Input.GetKeyDown(triggerKey) || (triggerScript != null && triggerScript.isTrig);

        if (shouldTrigger && !isTransitioning)
        {
            transitionTimer = 0f;
            isTransitioning = true;
        }

        if (isTransitioning)
        {
            transitionTimer += Time.deltaTime;
            float t = Mathf.Clamp01(transitionTimer / transitionDuration);
            float newTime = Mathf.Lerp(startTime, targetTime, t);

            if (lightingManager != null)
            {
                lightingManager.TimeOfDay = newTime;
            }

            if (t >= 1f)
            {
                isTransitioning = false;

                if (terrain != null)
                {
                    StartCoroutine(BlendTerrainTextures(terrain, originalTextureIndex, targetTextureIndex, blendDuration));
                }
            }
        }
    }

    System.Collections.IEnumerator BlendTerrainTextures(Terrain terrain, int fromIndex, int toIndex, float duration)
    {
        TerrainData terrainData = terrain.terrainData;
        int width = terrainData.alphamapWidth;
        int height = terrainData.alphamapHeight;
        int layers = terrainData.alphamapLayers;

        float[,,] alphamaps = terrainData.GetAlphamaps(0, 0, width, height);
        float[,,] startAlphas = alphamaps.Clone() as float[,,];

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float blendFactor = Mathf.Clamp01(timer / duration);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float fromValue = startAlphas[y, x, fromIndex];
                    float toValue = Mathf.Lerp(0f, fromValue, blendFactor);

                    alphamaps[y, x, fromIndex] = fromValue - toValue;
                    alphamaps[y, x, toIndex] += toValue;
                }
            }

            terrainData.SetAlphamaps(0, 0, alphamaps);
            yield return null;
        }
    }
}
