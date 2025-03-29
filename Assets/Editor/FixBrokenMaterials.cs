using UnityEditor;
using UnityEngine;

public class FixBrokenMaterials : EditorWindow
{
    Shader targetShader;

    [MenuItem("Tools/Fix Broken Materials")]
    public static void ShowWindow()
    {
        GetWindow<FixBrokenMaterials>("Fix Broken Materials");
    }

    void OnGUI()
    {
        GUILayout.Label("修复粉红色材质工具", EditorStyles.boldLabel);
        targetShader = (Shader)EditorGUILayout.ObjectField("目标 Shader", targetShader, typeof(Shader), false);

        if (GUILayout.Button("开始修复所有材质"))
        {
            if (targetShader == null)
            {
                EditorUtility.DisplayDialog("错误", "请先选择一个目标 Shader", "OK");
                return;
            }

            FixAllMaterials();
        }
    }

    void FixAllMaterials()
    {
        string[] guids = AssetDatabase.FindAssets("t:Material");
        int fixedCount = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat.shader.name == "Hidden/InternalErrorShader")
            {
                mat.shader = targetShader;
                EditorUtility.SetDirty(mat);
                fixedCount++;
                Debug.Log($"修复材质: {mat.name}");
            }
        }

        AssetDatabase.SaveAssets();
        EditorUtility.DisplayDialog("完成", $"共修复 {fixedCount} 个材质", "好！");
    }
}
