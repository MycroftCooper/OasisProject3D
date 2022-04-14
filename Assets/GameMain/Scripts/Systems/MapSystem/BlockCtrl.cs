using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OasisProject3D.MapSystem {
    public class BlockCtrl : MonoBehaviour {
        public EBlockType BlockType;

        [ShowInInspector]
        public float VegetationCoverage {
            get => vegetationCoverage;
            set {
                vegetationCoverage = value;
                BlockType = MapManager.GetBlockTypeByVC(value);
            }
        }
        private float vegetationCoverage;

        public Vector3 WorldPos;
        public Vector3Int LogicalPos;

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
        public BlockData GetBlockData() {
            BlockData data = new BlockData();
            return data;
        }
    }
}
