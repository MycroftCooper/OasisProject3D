using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OasisProject3D.MapSystem {
    public class BlockCtrl : MonoBehaviour {
        [LabelText("地块类型")]
        public EBlockType BlockType;
        [LabelText("世界坐标")]
        public Vector3 WorldPos;
        [LabelText("逻辑坐标")]
        public Vector2Int LogicalPos;
        [LabelText("高度")]
        public float Hight;
        public Dictionary<EBlockType, GameObject> BlockTypeGO;

        [ShowInInspector, LabelText("绿化率")]
        public float VegetationCoverage {
            get => vegetationCoverage;
            set {
                vegetationCoverage = value;
                UpdateBlockType();
            }
        }
        public float vegetationCoverage;
        [ShowInInspector]
        public BlockInfectionConf InfectionConf;


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
            BlockAnimaPlayer.Instance.OnTypeChange(this, () => {
                BlockTypeGO[BlockType].SetActive(false);
                BlockTypeGO[newType].SetActive(true);
                BlockType = newType;
            });
        }

        public BlockData GetBlockData() {
            BlockData data = new BlockData();
            return data;
        }
        public void LoadBlockData(BlockData data) {
            WorldPos = data.WorldPos;
            LogicalPos = data.LogicalPos;
            Hight = data.Hight;

            VegetationCoverage = data.VegetationCoverage;
            InfectionConf = data.InfectionConf;

            Buildable = data.Buildable;
        }
    }
}
