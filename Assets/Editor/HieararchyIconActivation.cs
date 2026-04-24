using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class HieararchyIconActivation
{
    static HieararchyIconActivation()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
    }

    private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        GameObject obj = EditorUtility.EntityIdToObject(instanceID) as GameObject;
        if (!obj) return;
        
        Rect rect = new Rect(selectionRect.x, selectionRect.y, 15f, selectionRect.height);
        if (Event.current.type == EventType.MouseDown && Event.current.button == 0 &&
            rect.Contains(Event.current.mousePosition))
        {
            if(!Application.isPlaying)
                Undo.RecordObject(obj, "Changing active state of object");
            obj.SetActive(!obj.activeSelf);
            if(!Application.isPlaying)
                EditorSceneManager.MarkSceneDirty(obj.scene);
            Event.current.Use();
        }
    }
}
