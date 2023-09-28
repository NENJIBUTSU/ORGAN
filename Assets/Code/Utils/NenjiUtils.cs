using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NenjiUtils
{
    class TextUtils
    {
        public static TextMesh CreateDebugWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000)
        {
            if (color == null)
            {
                color = Color.white;
            }
            return CreateDebugWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
        }

        public static TextMesh CreateDebugWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
        {
            GameObject gameObject = new GameObject("Debug_WorldText", typeof(TextMesh));

            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;

            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.color = color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

            return textMesh;
        }
    }

    class MouseUtils
    {
        public static Vector3 GetMouseWorldPosition2D()
        {
            Vector3 v = GetMouseWorldPosition(Input.mousePosition, Camera.main);
            v.z = 0;
            return v;
        }

        public static Vector3 GetMouseWorldPosition()
        {
            return GetMouseWorldPosition(Input.mousePosition, Camera.main);
        }

        public static Vector3 GetMouseWorldPosition(Camera worldCamera)
        {
            return GetMouseWorldPosition(Input.mousePosition, worldCamera);
        }

        public static Vector3 GetMouseWorldPosition(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }

        //NOTE: Possibly add a raycast world position point? IDK if ScreenToWorldPoint's raycast is infinite until it makes contact.
    }

    class MeshUtils
    {
        private static Quaternion[] cachedQuaternionEulerArr;
        private static void CacheQuaternionEuler()
        {
            if (cachedQuaternionEulerArr != null)
            {
                return;
            }

            cachedQuaternionEulerArr = new Quaternion[360];

            for (int i = 0; i < 360; i++)
            {
                cachedQuaternionEulerArr[i] = Quaternion.Euler(0, 0, i);
            }
        }
        private static Quaternion GetQuaternionEuler(float rotFloat)
        {
            int rot = Mathf.RoundToInt(rotFloat);
            rot = rot % 360;
            if (rot < 0)
            {
                rot += 360;
            }

            if (cachedQuaternionEulerArr == null)
            {
                CacheQuaternionEuler();
            }
            return cachedQuaternionEulerArr[rot];
        }



        public static void CreateEmptyMeshArrays(int quadCount, out Vector3[] verts, out Vector2[] uvs, out int[] tris)
        {
            verts = new Vector3[4 * quadCount];
            uvs = new Vector2[4 * quadCount];
            tris = new int[6 * quadCount];
        }

        public static void AddToMeshArrays(Vector3[] verts, Vector2[] uvs, int[] tris, int index, Vector3 pos, float rot, Vector3 baseSize, Vector2 uv00, Vector2 uv11)
        {
            int vIndex = index * 4;
            int vIndex0 = vIndex;
            int vIndex1 = vIndex + 1;
            int vIndex2 = vIndex + 2;
            int vIndex3 = vIndex + 3;

            baseSize *= .5f;

            bool skewed = baseSize.x != baseSize.y;

            if (skewed)
            {
                verts[vIndex0] = pos + GetQuaternionEuler(rot) * new Vector3(-baseSize.x,  baseSize.y);
                verts[vIndex1] = pos + GetQuaternionEuler(rot) * new Vector3(-baseSize.x, -baseSize.y);
                verts[vIndex2] = pos + GetQuaternionEuler(rot) * new Vector3( baseSize.x, -baseSize.y);
                verts[vIndex3] = pos + GetQuaternionEuler(rot) * baseSize;
            } 
            else
            {
                verts[vIndex0] = pos + GetQuaternionEuler(rot - 270) * baseSize;
                verts[vIndex1] = pos + GetQuaternionEuler(rot - 180) * baseSize;
                verts[vIndex2] = pos + GetQuaternionEuler(rot -  90) * baseSize;
                verts[vIndex3] = pos + GetQuaternionEuler(rot -   0) * baseSize;
            }


            uvs[vIndex0] = new Vector2(uv00.x, uv11.y);
            uvs[vIndex1] = new Vector2(uv00.x, uv00.y);
            uvs[vIndex2] = new Vector2(uv11.x, uv00.y);
            uvs[vIndex3] = new Vector2(uv11.x, uv11.y);

            int tIndex = index * 6;

            tris[tIndex + 0] = vIndex0;
            tris[tIndex + 1] = vIndex3;
            tris[tIndex + 2] = vIndex1;

            tris[tIndex + 3] = vIndex1;
            tris[tIndex + 4] = vIndex3;
            tris[tIndex + 5] = vIndex2;

        }
    }

}
