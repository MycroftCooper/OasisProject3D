using cfg.MapSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using MycroftToolkit.MathTool;
using MycroftToolkit.DiscreteGridToolkit.Hex;
using MycroftToolkit.QuickCode;
using cfg;
using QuickGameFramework.Runtime;

namespace OasisProject3D.MapSystem {
    public class BlockCtrl : Entity {
        [LabelText("地块类型")]
        public EBlockType blockType;
        [LabelText("世界坐标")]
        public Vector3 worldPos;
        [LabelText("逻辑坐标")]
        public Vector2Int logicalPos;
        public float height;

        [ShowInInspector, LabelText("绿化率")]
        public float VegetationCoverage {
            get => vegetationCoverage;
            set {
                vegetationCoverage = Mathf.Clamp(value, 0, 2);
                UpdateBlockType();
            }
        }
        public float vegetationCoverage;
        [ShowInInspector]
        public InfectionData InfectionData;

        public TickerAuto Ticker;

        public bool buildable;
        
        public void Init(float randomStartRange) {
            Ticker = new TickerAuto(InfectionData.Time);
            Ticker.OnTick += UpdateBlock;
            Timer.Register(QuickRandom.Simple.GetFloat(10), Ticker.Start);
        }

        public void UpdateBlock() {
            MapManager mm = MapManager.Instance;
            List<Vector2Int> targetPos = HexGridTool.Coordinate_Axial.GetPointsInHexagon(logicalPos, InfectionData.Range);
            List<BlockCtrl> targetBlock = mm.GetBlocks(targetPos);
            float targetVc = 0;
            targetBlock.ForEach(block => {
                if (block.blockType != blockType)
                    targetVc += mm.BlockConf[block.blockType].InfectionData.Factor;
            });
            VegetationCoverage += targetVc;
        }
        public void UpdateBlockType() {
            EBlockType newType = MapManager.Instance.GetBlockTypeByVc(vegetationCoverage);
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
            height = data.Height;

            vegetationCoverage = data.VegetationCoverage;
            InfectionData = data.InfectionConf;

            buildable = data.Buildable;
        }
    }
}
