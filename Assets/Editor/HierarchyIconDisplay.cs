using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [InitializeOnLoad]
    public static class HierarchyIconDisplay
    {
        static bool _hierarchyHasFocus = false;
        static EditorWindow _hierarchyEditorWindow;
    
        static HierarchyIconDisplay()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
            EditorApplication.update += OnEditorUpdate;
        }

        private static void OnEditorUpdate()
        {
            if(_hierarchyEditorWindow == null)
                _hierarchyEditorWindow = EditorWindow.GetWindow(System.Type.GetType("UnityEditor.SceneHierarchyWindow,UnityEditor"));
        
            _hierarchyHasFocus = EditorWindow.focusedWindow != null && EditorWindow.focusedWindow ==  _hierarchyEditorWindow;
        }

        private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (gameObject == null) return;

            if (PrefabUtility.GetCorrespondingObjectFromSource(gameObject) != null) return;
        
            Component[] components = gameObject.GetComponents<Component>();
            if (components == null || components.Length == 0) return;
        
            Component component = components.Length > 1 ?  components[1] :  components[0];
        
            Type type = component.GetType();

            GUIContent content = EditorGUIUtility.ObjectContent(component, type);
            content.text = null;
            content.tooltip = type.Name;

            if (content.image == null) return;
        
            bool isSelected = Selection.instanceIDs.Contains(instanceID);
            bool isHovering = selectionRect.Contains(Event.current.mousePosition);
        
    
            Color color = UnityEditorBackgroundColor.Get(isSelected, isHovering,_hierarchyHasFocus);
        
            Rect backgroundRect = selectionRect;
            backgroundRect.width = 18.5f;
        
            EditorGUI.DrawRect(backgroundRect, color);
        
            EditorGUI.LabelField(selectionRect, content);
        }
    }
}
