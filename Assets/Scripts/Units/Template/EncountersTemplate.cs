using Sirenix.OdinInspector;
using UnityEngine;

namespace Units.Template
{
    [CreateAssetMenu(fileName = "Encounters", menuName = "Unit/Encounters")]
    public class EncountersTemplate : ScriptableObject
    {
        [MinValue(1)] private int columns = 2;
        
        [MinValue(1)] private int rows = 5;

        [HideInInspector]
        public EnemyTemplate[] slots;

        public int TileCount => columns * rows;

        private void ResizeSlots()
        {
            int size = columns * rows;
            var old = slots ?? new EnemyTemplate[0];
            var next = new EnemyTemplate[size];
            for (int i = 0; i < Mathf.Min(old.Length, size); i++)
                next[i] = old[i];
            slots = next;
        }

#if UNITY_EDITOR
        [OnInspectorGUI]
        private void DrawGrid()
        {
            if (slots == null || slots.Length != columns * rows)
                ResizeSlots();

            float cellWidth = 80f;
            float cellHeight = 40f;

            UnityEditor.EditorGUILayout.Space(4);
            UnityEditor.EditorGUILayout.LabelField("Spawn Layout", UnityEditor.EditorStyles.boldLabel);

            for (int row = 0; row < rows; row++)
            {
                UnityEditor.EditorGUILayout.BeginHorizontal();
                for (int col = 0; col < columns; col++)
                {
                    int idx = col * rows + row;

                    UnityEditor.EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(cellWidth));
                    UnityEditor.EditorGUILayout.LabelField($"#{idx + 1}", UnityEditor.EditorStyles.centeredGreyMiniLabel, GUILayout.Width(cellWidth));
                    slots[idx] = (EnemyTemplate)UnityEditor.EditorGUILayout.ObjectField(
                        slots[idx], typeof(EnemyTemplate), false,
                        GUILayout.Width(cellWidth), GUILayout.Height(cellHeight));
                    UnityEditor.EditorGUILayout.EndVertical();
                }
                UnityEditor.EditorGUILayout.EndHorizontal();
            }

            UnityEditor.EditorGUILayout.Space(4);

            if (GUILayout.Button("Reset Grid"))
            {
                if (UnityEditor.EditorUtility.DisplayDialog("Reset Grid", "Clear all slots?", "Reset", "Cancel"))
                {
                    slots = new EnemyTemplate[columns * rows];
                    UnityEditor.EditorUtility.SetDirty(this);
                }
            }

            UnityEditor.EditorGUILayout.Space(4);
        }
#endif
    }
}