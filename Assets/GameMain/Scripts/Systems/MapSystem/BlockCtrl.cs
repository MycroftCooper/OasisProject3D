using cfg.MapSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MycroftToolkit.MathTool;
using MycroftToolkit.DiscreteGridToolkit.Hex;
using MycroftToolkit.QuickCode;
using System.Linq;

namespace OasisProject3D.MapSystem {
    public class BlockCtrl : MonoBehaviour {
        [LabelText("地块类型")]
        public EBlockType blockType;
        [LabelText("世界坐标")]
        public Vector3 worldPos;
        [LabelText("逻辑坐标")]
        public Vector2Int logicalPos;
        [LabelText("高度")]
        public float hight;
        public Dictionary<EBlockType, GameObject> blockTypeGO;

        [ShowInInspector, LabelText("绿化率")]
        public float VegetationCoverage {
            get => _vegetationCoverage;
            set {
                _vegetationCoverage = Mathf.Clamp(value, 0, 2);
                UpdateBlockType();
            }
        }
        public float _vegetationCoverage;
        [ShowInInspector]
        public InfectionData infectionData;

        public bool buildable;

        void Start() {
            _deltaTime = Random.Range(-10, 0);
        }

        private float _deltaTime = 0;

        void Update() {

            if (infectionData.CanInfectious) {
                _deltaTime += Time.deltaTime;
                if (_deltaTime >= infectionData.Time) {
                    _deltaTime = 0;
                    UpdateBlock();
                }
            }

        }

        public void UpdateBlock() {
            MapManager mm = MapManager.Instance;
            List<Vector2Int> targetPos = HexGridTool.Coordinate_Axial.GetPointsInHexagon(logicalPos, infectionData.Range);
            List<BlockCtrl> targetBlock = mm.GetBlocks(targetPos);
            float targetVC = 0;
            targetBlock.ForEach(block => {
                if (block.blockType != blockType)
                    targetVC += mm.BlockConf[block.blockType].InfectionData.Factor;
            });
            VegetationCoverage += targetVC;
        }
        public void UpdateBlockType() {
            EBlockType newType = MapManager.Instance.GetBlockTypeByVC(_vegetationCoverage);
            if (newType == blockType)
                return;
            BlockAnimaPlayer.Instance.OnTypeChange(this, () => {
                BlockFactory.Instance.ChangeBlockMaterials(this, newType);
                BlockFactory.Instance.AddBlock_Element(this, newType);
                blockType = newType;
            });
        }


        public BlockData GetBlockData() {
            BlockData data = new BlockData();
            return data;
        }
        public void LoadBlockData(BlockData data) {
            worldPos = data.WorldPos;
            logicalPos = data.LogicalPos;
            hight = data.Hight;

            _vegetationCoverage = data.VegetationCoverage;
            infectionData = data.InfectionConf;

            buildable = data.Buildable;
        }
    }
}
