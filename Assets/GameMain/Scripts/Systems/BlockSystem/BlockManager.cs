using System;
using System.Collections.Generic;
using cfg;
using cfg.MapSystem;
using MycroftToolkit.MathTool;
using QuickGameFramework.Runtime;
using UnityEngine;

namespace OasisProject3D.BlockSystem {
    public class BlockManager : IModule {
        #region 模块API相关
        public int Priority { get; set; }
        public bool IsFrameworkModule => false;
        public bool IsManualUpdate => true;

        public void OnModuleCreate(params object[] createParam) {
            Factory = new BlockFactory();
        }

        public void OnModuleUpdate(float intervalSeconds) { }

        public void OnModuleFixedUpdate(float intervalSeconds) {
            if (!IsInit) {
                return;
            }

            float gr = 0;
            MapBlockForEach_Y((_) => {
                UpdateBlock(_);
                gr += _.greenRate;
            });
            GamePlayEnter.MapMgr.GreenRate = gr / _posBlockMap.Count *100;
        }

        public void OnModuleDestroy() {
            throw new NotImplementedException();
        }
        #endregion
        
        public BlockFactory Factory;
        public Dictionary<EBlockType, BlockConfig> BlockConf;
        private Vector2Int MapSize => GamePlayEnter.MapMgr.MapSize;
        private Dictionary<Vector2Int, BlockCtrl> _posBlockMap;
        private Dictionary<BlockCtrl, Vector2Int> _blockPosMap;
        
        public bool IsInit { get; private set; }

        public void Init() {
            BlockConf = GameEntry.DataTableMgr.Tables.DTBlockConfig.DataMap;
            _posBlockMap = new Dictionary<Vector2Int, BlockCtrl>();
            _blockPosMap = new Dictionary<BlockCtrl, Vector2Int>();
            Factory.Init();
            IsInit = true;
        }

        public void InitBlockInMapByRandom(int mixingAreaSize, QuickRandom random, Vector2Int pos) {
            int desertCol = (int)(MapSize.x * BlockConf[EBlockType.Desert].GenerateRate);
            int gobiCol = (int)(MapSize.x * (BlockConf[EBlockType.Desert].GenerateRate + BlockConf[EBlockType.Gobi].GenerateRate));
            // int oasisCol = mapSize.x - desertCol - gobiCol;
            
            var y = pos.y;
            EBlockType blockType;
            if (y < desertCol - mixingAreaSize)
                blockType = EBlockType.Desert;

            else if (y < desertCol)
                blockType = random.GetBool() ? EBlockType.Desert : EBlockType.Gobi;

            else if (y < desertCol + gobiCol - mixingAreaSize)
                blockType = EBlockType.Gobi;

            else if (y < desertCol + gobiCol)
                blockType = random.GetBool() ? EBlockType.Gobi : EBlockType.Oasis;
            else
                blockType = EBlockType.Oasis;
            BlockCtrl newBlock = Factory.CreateEntity($"Block_{pos.ToString()}",(pos, blockType));
            _posBlockMap.Add(pos, newBlock);
            _blockPosMap.Add(newBlock, pos);
        }

        public void InitBlockInMapByData() {
            // todo: 地图按存档加载补全
        }
        
        public bool HasBlock(Vector2Int pos) => _posBlockMap.ContainsKey(pos);
        
        public bool HasBlock(BlockCtrl block) => _blockPosMap.ContainsKey(block);

        public BlockCtrl GetBlock(Vector2Int pos) {
            return HasBlock(pos) ? _posBlockMap[pos] : null;
        }
        
        public List<BlockCtrl> GetBlocks(List<Vector2Int> poses) {
            List<BlockCtrl> output = new List<BlockCtrl>();
            foreach (Vector2Int pos in poses) {
                BlockCtrl bc = GetBlock(pos);
                if (bc != null) output.Add(bc);
            }
            return output;
        }
        
        public BlockCtrl GetBlock(Vector3 worldPos) {
            var logicPos = GamePlayEnter.MapMgr.WorldPos2LogicPos(worldPos);
            return GetBlock(logicPos);
        }
        
        public Vector2Int GetBlockPos(BlockCtrl block) {
            return HasBlock(block) ? _blockPosMap[block] : default;
        } 
        
        public EBlockType GetBlockTypeByVc(float vc) {
            if (vc < BlockConf[EBlockType.Desert].GreennessRange.y) return EBlockType.Desert;
            return vc < BlockConf[EBlockType.Gobi].GreennessRange.y ? EBlockType.Gobi : EBlockType.Oasis;
        }

        public void UpdateBlock(BlockCtrl blockCtrl) => blockCtrl.Ticker.DoTick();
        
        public void MapBlockForEach_X(Action<BlockCtrl> callback) {
            var px1 = 0;
            var px2 = MapSize.x;
            for (var y = 0; y < MapSize.y; y++) {
                for (var x = px1; x < px2; x++) {
                    callback(GetBlock(new Vector2Int(x, y)));
                }

                if (y % 2 == 0) continue;
                px1--;
                px2--;
            }
        }
        public void MapBlockForEach_Y(Action<BlockCtrl> callback) {
            var px2 = MapSize.x;
            for (var x = 0; x < px2; x++) {
                var t = x;
                for (var y = 0; y < MapSize.y; y++) {
                    callback(GetBlock(new Vector2Int(x, y)));
                    if (y % 2 != 0) x--;
                }
                x = t;
            }
        }
    }
}

