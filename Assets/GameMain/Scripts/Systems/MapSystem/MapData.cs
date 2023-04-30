using System.Collections.Generic;
using OasisProject3D.BlockSystem;
using UnityEngine;

namespace OasisProject3D.MapSystem {
    public class MapData {
        public MapGenerateConfig MapGenerateConfig;
        public Dictionary<Vector3, BlockData> Map;
        public Vector2Int MapSize;
        public float VegetationCoverage;
    }
}
