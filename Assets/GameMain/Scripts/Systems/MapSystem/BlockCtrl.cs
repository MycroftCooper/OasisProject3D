using cfg.MapSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MycroftToolkit.DiscreteGridToolkit.Hex;

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
                _vegetationCoverage = value;
                UpdateBlockType();
            }
        }
        public float _vegetationCoverage;
        [ShowInInspector]
        public InfectionData infectionData;

        public bool canInfectious;
        public bool canBeInfectious;
        public bool buildable;


        // Start is called before the first frame update
        void Start() {

        }

        private float _deltaTime = 0;
        // Update is called once per frame
        void Update() {
            if (canInfectious) {
                _deltaTime += Time.deltaTime;
                if (_deltaTime * 100 >= 200) {
                    _deltaTime = 0;
                    DoInfect();
                }
            }

        }

        public void DoInfect() {
            MapManager mm = MapManager.Instance;
            List<Vector2Int> targertPos = HexGridTool.Coordinate_Axial.GetPointsInHexagon(logicalPos, infectionData.Range);
            foreach (Vector2Int pos in targertPos) {
                if (!mm.HasBlock(pos)) continue;
                float oldVC = mm.GetBlockVC(pos);
                float newVC = (_vegetationCoverage - oldVC) * infectionData.Factor;
                mm.SetBlockVC(pos, newVC);
            }

        }
        public void UpdateBlockType() {
            EBlockType newType = MapManager.Instance.GetBlockTypeByVC(_vegetationCoverage);
            if (newType == blockType) return;
            BlockAnimaPlayer.Instance.OnTypeChange(this, () => {
                blockTypeGO[blockType].SetActive(false);
                blockTypeGO[newType].SetActive(true);
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

            VegetationCoverage = data.VegetationCoverage;
            infectionData = data.InfectionConf;

            buildable = data.Buildable;
        }
    }
}
