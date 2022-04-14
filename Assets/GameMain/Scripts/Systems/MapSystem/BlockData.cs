using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OasisProject3D.MapSystem {
    public class BlockData {
        public EBlockType BlockType;

        private float vegetationCoverage;
        public float VegetationCoverage {
            get => vegetationCoverage;
            set {
                vegetationCoverage = value;
                BlockType = MapManager.GetBlockTypeByVC(value);
            }
        }

        public Vector3 WorldPos;
        public Vector3Int LogicalPos;

    }
}
