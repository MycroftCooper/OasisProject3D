using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OasisProject3D.MapSystem {
    public class BlockCtrl : MonoBehaviour {
        public EBlockType BlockType;
        public Dictionary<EBlockType, GameObject> BlockTypeGO;
        [ShowInInspector]
        public float VegetationCoverage {
            get => vegetationCoverage;
            set {
                vegetationCoverage = value;
                UpdateBlockType();
            }
        }
        private float vegetationCoverage;

        public Vector3 WorldPos {
            get => transform.position;
            set => transform.position = value;
        }
        public Vector2Int LogicalPos;

        public bool Buildable;

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public void UpdateBlockType() {
            EBlockType newType = MapManager.GetBlockTypeByVC(vegetationCoverage);
            if (newType == BlockType) return;
            BlockTypeGO[BlockType].SetActive(false);
            BlockTypeGO[newType].SetActive(true);
            BlockType = newType;
        }

        public BlockData GetBlockData() {
            BlockData data = new BlockData();
            return data;
        }
    }
}
