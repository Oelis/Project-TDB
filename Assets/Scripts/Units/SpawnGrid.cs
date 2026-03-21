using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Units
{
    [Serializable]
    public class SpawnGrid
    {
        public Vector3 origin = Vector3.zero;

        public Vector3 rotation = Vector3.zero;

        [MinValue(1)]
        public int columns = 2;

        [MinValue(1)]
        public int rows = 5;

        [MinValue(0.1f)]
        public float tileSize = 1.5f;

        public int TileCount => columns * rows;

        private Quaternion Rotation => Quaternion.Euler(rotation);

        // Col 0 = inner (+X local), increases outward (-X). Row 0 = front (+Z local), increases backward (-Z).
        private Vector3 GridCenter => new Vector3(-(columns - 1) * tileSize / 2f, 0f, -(rows - 1) * tileSize / 2f);

        public bool mirrorColumns;

        public Quaternion GetTileRotation() => Rotation;

        public Vector3 GetTilePosition(int index)
        {
            index = Mathf.Clamp(index, 0, TileCount - 1);
            int col = index % columns;
            int row = index / columns;
            if (mirrorColumns) col = (columns - 1) - col;
            Vector3 localOffset = new Vector3(-col * tileSize, 0f, -row * tileSize);
            return origin + Rotation * (localOffset - GridCenter);
        }

#if UNITY_EDITOR
        public void DrawGizmos(Color color)
        {
            Color prev = Gizmos.color;
            Gizmos.color = color;
            for (int i = 0; i < TileCount; i++)
            {
                Matrix4x4 prevMatrix = Gizmos.matrix;
                Gizmos.matrix = Matrix4x4.TRS(GetTilePosition(i), Rotation, Vector3.one);
                Gizmos.DrawWireCube(Vector3.zero, new Vector3(tileSize * 0.9f, 0.05f, tileSize * 0.9f));
                Gizmos.matrix = prevMatrix;
            }

            Vector3 center = origin;
            Vector3 forward = Rotation * Vector3.forward;
            float arrowLength = tileSize * 2f;
            Vector3 tip = center + forward * arrowLength;
            Gizmos.DrawLine(center, tip);
            Vector3 right = Rotation * Vector3.right * tileSize * 0.3f;
            Vector3 back = -forward * tileSize * 0.4f;
            Gizmos.DrawLine(tip, tip + back + right);
            Gizmos.DrawLine(tip, tip + back - right);

            Gizmos.color = prev;
        }
#endif
    }
}