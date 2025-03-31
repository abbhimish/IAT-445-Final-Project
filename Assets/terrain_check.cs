using UnityEngine;

public class TerrainLayerDebugger : MonoBehaviour
{
    public Terrain terrain;

    [ContextMenu("Print Terrain Layers Info")]
    public void PrintLayers()
    {
        if (terrain == null)
        {
            Debug.LogWarning("���� Inspector �з��� Terrain��");
            return;
        }

        var layers = terrain.terrainData.terrainLayers;

        Debug.Log($"Terrain ��ע���� {layers.Length} �� Layer��");

        for (int i = 0; i < layers.Length; i++)
        {
            string layerName = layers[i] != null ? layers[i].name : "��δ���� Layer��";
            Debug.Log($"Layer Index {i}: {layerName}");
        }
    }
}
