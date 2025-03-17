using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[InitializeOnLoad]
public class PrefabPlacementTool
{
    private static GameObject prefabToPlace;
    private static bool isActive = false;
    private const string MenuName = "Tools/启用物体放置工具";

    static PrefabPlacementTool()
    {
        SceneView.duringSceneGui += OnSceneGUI;
        EditorApplication.delayCall += () => {
            isActive = EditorPrefs.GetBool(MenuName, false);
            Menu.SetChecked(MenuName, isActive);
        };
    }

    [MenuItem(MenuName)]
    private static void ToggleAction()
    {
        isActive = !isActive;
        Menu.SetChecked(MenuName, isActive);
        EditorPrefs.SetBool(MenuName, isActive);
        SceneView.RepaintAll();
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        if (!isActive) return;

        Event e = Event.current;
        HandleDragAndDrop();

        // 鼠标点击放置逻辑
        if (e.type == EventType.MouseDown && e.button == 0 &&
            !e.alt && !e.control && !e.shift)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                if (prefabToPlace != null)
                {
                    GameObject newObj = PrefabUtility.InstantiatePrefab(prefabToPlace) as GameObject;
                    Undo.RegisterCreatedObjectUndo(newObj, "Create " + newObj.name);
                    newObj.transform.position = hit.point;
                    newObj.transform.rotation = Quaternion.identity;
                }
                else
                {
                    Debug.LogWarning("请先拖拽一个预制体到场景视图");
                }
            }

            e.Use();
        }

        // 绘制界面提示
        Handles.BeginGUI();
        GUILayout.Window(0, new Rect(50, 10, 200, 100),
            (id) => {
                GUILayout.Label("当前预制体: " + (prefabToPlace != null ?
                    $"<color=#00ff00>{prefabToPlace.name}</color>" :
                    "<color=#ff0000>无</color>"),
                    new GUIStyle() { richText = true });

                GUILayout.Space(10);
                GUILayout.Label("使用方法：");
                GUILayout.Label("1. 从Project窗口拖拽预制体");
                GUILayout.Label("2. 左键点击地面放置");

                if (GUILayout.Button("清除选择"))
                {
                    prefabToPlace = null;
                }
            },
            "放置工具");
        Handles.EndGUI();
    }

    private static void HandleDragAndDrop()
    {
        Event evt = Event.current;
        Rect sceneViewRect = new Rect(0, 0, Screen.width, Screen.height);

        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!sceneViewRect.Contains(evt.mousePosition))
                    return;

                bool isValid = false;
                foreach (Object obj in DragAndDrop.objectReferences)
                {
                    if (obj is GameObject gameObj)
                    {
                        // 验证是否为预制体
                        if (PrefabUtility.GetPrefabAssetType(gameObj) != PrefabAssetType.NotAPrefab)
                        {
                            isValid = true;
                            break;
                        }
                    }
                }

                if (isValid)
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                    if (evt.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();

                        foreach (Object obj in DragAndDrop.objectReferences)
                        {
                            if (obj is GameObject gameObj &&
                                PrefabUtility.GetPrefabAssetType(gameObj) != PrefabAssetType.NotAPrefab)
                            {
                                prefabToPlace = gameObj;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
                }

                evt.Use();
                break;
        }
    }
}