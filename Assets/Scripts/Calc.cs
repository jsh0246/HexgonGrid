using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculation
{
    public static class Calc
    {
        public static Vector2Int Vector3to2Int(Vector3Int v)
        {
            return new Vector2Int(v.x, v.y);
        }

        public static int ManhattenDistance(Vector2Int s, Vector2Int t)
        {
            return Mathf.Abs(s.x - t.x) + Mathf.Abs(s.y - t.y);
        }
    }
}