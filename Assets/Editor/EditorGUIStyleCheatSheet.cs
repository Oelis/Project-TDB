using UnityEditor;
using UnityEngine;

public class EditorGUIStyleCheatSheet : EditorWindow
{
    private Vector2 _scrollPos;
    private GUIContent _copyIcon;
    private GUIStyle _copyButtonStyle;
    private string _searchTerm;
    
    [MenuItem("Window/UI/Editor GUI Style Cheat Sheet", priority = 3000)]
    public static void ShowWindow()
    {
        GetWindow<EditorGUIStyleCheatSheet>("Editor GUI Style");
    }

    void OnGUI()
    {
        GUILayout.BeginVertical(EditorStyles.toolbar,GUILayout.Width(Screen.width));
        string searchTerm = GUILayout.TextField(_searchTerm, EditorStyles.toolbarSearchField, GUILayout.Width(200));
        if(searchTerm != _searchTerm)
            _searchTerm = searchTerm;
        
        GUILayout.EndHorizontal();
        
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

        foreach (var style in GUI.skin)
        {
            if (style is GUIStyle guiStyle)
            {
                if(!string.IsNullOrEmpty(_searchTerm) && !guiStyle.name.ToLower().Contains(_searchTerm.ToLower()))
                    continue;
                
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button(_copyIcon ??= EditorGUIUtility.IconContent("Grid.PickingTool"),
                        _copyButtonStyle ??= GUI.skin.FindStyle("ToolbarSearchTextFieldJumpButton")))
                {
                    EditorGUIUtility.systemCopyBuffer = guiStyle.name;
                    this.ShowNotification(new GUIContent($"Style \"{guiStyle.name}\"Copied !"));
                }
                
                EditorGUILayout.LabelField(guiStyle.name);
                
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.LabelField(!guiStyle.normal.background ? guiStyle.name : string.Empty,guiStyle);
                
                EditorGUILayout.Space(guiStyle.CalcHeight(GUIContent.none, Screen.width));
            }
        }
        
        EditorGUILayout.EndScrollView();
        
    }
}
