using cfg.MapSystem;
using UnityEngine;


namespace OasisProject3D.MapSystem {
    public struct BlockData {
        public Vector3 WorldPos;
        public Vector2Int LogicalPos;
        public float Height;

        public float VegetationCoverage;
        public InfectionData InfectionConf;

        public bool Buildable;
    }
}
