using cfg.MapSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OasisProject3D.MapSystem {
    public class BlockData {
        public Vector3 WorldPos;
        public Vector2Int LogicalPos;
        public float Hight;

        public float VegetationCoverage;
        public InfectionData InfectionConf;

        public bool Buildable;
    }
}
