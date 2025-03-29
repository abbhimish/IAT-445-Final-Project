using UnityEngine;

public class TerrainLayerDebugger : MonoBehaviour
{
    public Terrain terrain;

    [ContextMenu("Print Terrain Layers Info")]
    public void PrintLayers()
    {
        if (terrain == null)
        {
            Debug.LogWarning("请在 Inspector 中分配 Terrain。");
            return;
        }

        var layers = terrain.terrainData.terrainLayers;

        Debug.Log($"Terrain 共注册了 {layers.Length} 个 Layer：");

        for (int i = 0; i < layers.Length; i++)
        {
            string layerName = layers[i] != null ? layers[i].name : "（未命名 Layer）";
            Debug.Log($"Layer Index {i}: {layerName}");
        }
    }
}
