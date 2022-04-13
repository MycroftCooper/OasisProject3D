using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OasisProject3D.MapSystem {
    public class MapData {
        public Dictionary<Vector3, BlockData> Map;
        public Vector2Int MapSize;
        public float VegetationCoverage;
    }
}
