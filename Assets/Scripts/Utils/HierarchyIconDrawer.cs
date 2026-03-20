using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.TypeSearch;
using UnityEditor;
using UnityEngine;

namespace Utils
{
    [InitializeOnLoad]
    public static class HierarchyIconDrawer
    {
        static readonly Texture2D _requiredTexture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Art/Textures/exclamation.png");

        private static readonly Dictionary<Type, FieldInfo[]> cachedFieldInfo = new();
        
        static HierarchyIconDrawer()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
        }

        private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            if(EditorUtility.InstanceIDToObject(instanceID) is not GameObject gameObject) return;

            foreach (var component in gameObject.GetComponents<Component>())
            {
                if (component == null) continue;
                
                var fields = GetCachedFieldsWithRequiredAttribute(component.GetType());
                if (fields == null) continue;
                if (fields.Any(field => IsFieldUnassigned(field.GetValue(component))))
                {
                    var iconRect = new Rect(selectionRect.xMax-20, selectionRect.y, 16, 16);
                    GUI.Label(iconRect, new GUIContent(_requiredTexture,"This component is missing required fields"));
                    break;
                }
            }
        }

        private static bool IsFieldUnassigned(object fieldValue)
        {
            if (fieldValue == null) return true;
            
            if(fieldValue is string stringValue && string.IsNullOrEmpty(stringValue)) return true;

            if (fieldValue is IEnumerable<object> enumerable)
            {
                foreach (var item in enumerable)
                {
                    if (item == null) return true;
                }
            }

            return false;
        }
        
        static FieldInfo[] GetCachedFieldsWithRequiredAttribute(Type componentType)
        {
            if (!cachedFieldInfo.TryGetValue(componentType, out FieldInfo[] fields))
            {
                fields = componentType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                List<FieldInfo> requiredField = new();

                foreach (FieldInfo field in fields)
                {
                    bool isSerialized = field.IsPublic || field.IsDefined(typeof(SerializeField), false);
                    bool isRequired = field.IsDefined(typeof(RequiredAttribute), false);

                    if (isSerialized && isRequired)
                    {
                        requiredField.Add(field);   
                        
                    }
                }
                fields = requiredField.ToArray();
                cachedFieldInfo[componentType] = fields;
            }

            return fields;
        }
        
    }
    
    

    
}