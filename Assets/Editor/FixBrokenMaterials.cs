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
        GUILayout.Label("�޸��ۺ�ɫ���ʹ���", EditorStyles.boldLabel);
        targetShader = (Shader)EditorGUILayout.ObjectField("Ŀ�� Shader", targetShader, typeof(Shader), false);

        if (GUILayout.Button("��ʼ�޸����в���"))
        {
            if (targetShader == null)
            {
                EditorUtility.DisplayDialog("����", "����ѡ��һ��Ŀ�� Shader", "OK");
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
                Debug.Log($"�޸�����: {mat.name}");
            }
        }

        AssetDatabase.SaveAssets();
        EditorUtility.DisplayDialog("���", $"���޸� {fixedCount} ������", "�ã�");
    }
}
