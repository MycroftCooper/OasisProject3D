using System;
using cfg.MapSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using MycroftToolkit.DiscreteGridToolkit.Hex;
using MycroftToolkit.QuickCode;
using cfg;
using DG.Tweening;
using QuickGameFramework.Runtime;

namespace OasisProject3D.BlockSystem {
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
            get => greenRate;
            set {
                greenRate = Mathf.Clamp(value, 0, 2);
                UpdateBlockType();
            }
        }
        public float greenRate;
        [ShowInInspector]
        public InfectionData InfectionData;

        public Ticker Ticker;

        public bool buildable;

        private static BlockManager BlockMgr => GamePlayEnter.BlockMgr;

        public override void Init(string entityID, IEntityFactory<Entity> factory, object data = null) {
            base.Init(entityID, factory, data);
            Ticker = new Ticker(InfectionData.Time);
            Ticker.OnExecute += UpdateBlock;
            Ticker.Pause();
            float randomStartTime = ((BlockFactory)factory).BlockRandom.GetFloat(InfectionData.Time);
            Timer.Register(randomStartTime, Ticker.Resume);
        }

        public void UpdateBlock(int updateTime) {
            List<Vector2Int> targetPos = HexGridTool.Coordinate_Axial.GetPointsInHexagon(logicalPos, InfectionData.Range);
            List<BlockCtrl> targetBlock = BlockMgr.GetBlocks(targetPos);
            float targetVc = 0;
            targetBlock.ForEach(block => {
                if (block.blockType != blockType)
                    targetVc += BlockMgr.BlockConf[block.blockType].InfectionData.Factor;
            });
            VegetationCoverage += targetVc;
        }
        
        public void UpdateBlockType() {
            EBlockType newType = BlockMgr.GetBlockTypeByVc(greenRate);
            if (newType == blockType)
                return;
            TypeChangeAnim(() => {
                BlockMgr.Factory.ChangeBlockMaterials(this, newType);
                BlockMgr.Factory.AddBlockElement(this, newType);
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

            greenRate = data.VegetationCoverage;
            InfectionData = data.InfectionConf;

            buildable = data.Buildable;
        }

        #region 动画相关

        public float playTimeSelected = 0.5f;
        public float playDistanceSelected = 1f;
        public float playTimeTypeChange = 1f;

        public void OnSelectedAnim(Action callback) {
            transform.DOMoveY(worldPos.y + playDistanceSelected, playTimeSelected).OnComplete(() => { callback?.Invoke(); });
        }
        public void OffSelectedAnim(Action callback) {
            transform.DOMoveY(worldPos.y, playTimeSelected).OnComplete(() => { callback?.Invoke(); });
        }
        public void TypeChangeAnim(Action callback) {
            bool isCall = false;
            transform.DORotate(Vector3.right * 360, playTimeTypeChange, RotateMode.FastBeyond360).OnUpdate(() => {
                if (!(transform.rotation.eulerAngles.x > 180) || isCall) return;
                callback?.Invoke();
                isCall = true;
            });
        }
        #endregion
    }
}
