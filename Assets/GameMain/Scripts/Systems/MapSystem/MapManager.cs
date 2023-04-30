using System;
using System.Collections.Generic;
using Borodar.FarlandSkies.LowPoly;
using UnityEngine;
using MycroftToolkit.MathTool;
using MycroftToolkit.DiscreteGridToolkit;
using MycroftToolkit.DiscreteGridToolkit.Hex;
using OasisProject3D.BlockSystem;
using QuickGameFramework.Runtime;

namespace OasisProject3D.MapSystem {
    public class MapManager : IModule {
        #region 模块API相关
        public int Priority { get; set; }
        public bool IsFrameworkModule => false;
        public bool IsManualUpdate => false;
        
        public void OnModuleCreate(params object[] createParam) {
            
        }

        public void OnModuleUpdate(float intervalSeconds) { }

        public void OnModuleFixedUpdate(float intervalSeconds) {
            if (!IsInit) {
                return;
            }
            _blockMgr.OnModuleFixedUpdate(intervalSeconds);
            SkyboxDayNightCycle​.Instance.TimeOfDay += (Time.deltaTime / 1000) * 100f;
            SkyboxController.Instance.CloudsRotation += (Time.deltaTime / 1000) * 100f;
            OnMapUpdate?.Invoke();
        }

        public void OnModuleDestroy() {
        }
        #endregion
        
        public MapGenerateConfig MapGenerateConfig;
        
        public QuickRandom Random;
        private float _randomStartRange; // 随机开始范围
        
        public float GreenRate; // 全图植被覆盖率
        public Vector2Int MapSize { get; private set; }
        private HashSet<Vector2Int> _map;
        private BlockManager _blockMgr;
        public Action OnMapUpdate;

        #region 初始化相关
        public bool IsInit { get; private set; }
        public void Init(MapGenerateConfig config = null, MapData data = null) {
            SkyboxDayNightCycle​.Instance.TimeOfDay = 50;
            
            if (data != null) {
                MapGenerateConfig = data.MapGenerateConfig;
            } else {
                if (config == null) {
                    config = ScriptableObject.CreateInstance<MapGenerateConfig>();
                }
                MapGenerateConfig = config;
            }
            
            MapSize = MapGenerateConfig.mapSize;
            Random = new QuickRandom(MapGenerateConfig.seed);
            Random.Noise.SetNoiseType(QuickNoise.NoiseType.Cellular);
            Random.Noise.SetCellularReturnType(QuickNoise.CellularReturnType.CellValue);
            Random.Noise.SetFrequency(MapGenerateConfig.densityParameter);
            _blockMgr = GamePlayEnter.BlockMgr;
            IsInit = true;
        }
        
        public void InitMap(MapData data = null) {
            var mapSize = MapGenerateConfig.mapSize;
            var mixingAreaSize = MapGenerateConfig.mixingAreaSize;
            _map = new HashSet<Vector2Int>(mapSize.x*mapSize.y);
            
            int px2 = mapSize.x;
            for (int x = 0; x < px2; x++) {
                int t = x;
                for (int y = 0; y < mapSize.y; y++) {
                    Vector2Int pos = new Vector2Int(x, y);
                    _map.Add(pos);
                    if (data == null) {
                        _blockMgr.InitBlockInMapByRandom(mixingAreaSize , Random, pos);
                    } else {
                        _blockMgr.InitBlockInMapByData();
                    }
                    
                    if (y % 2 != 0) x--;
                }
                x = t;
            }
            
            UpdateGreenRate();
        }
        #endregion


        #region 存档相关
        public MapData GetMapData() {
            MapData data = new MapData();
            // todo: 生成地图存档
            return data;
        }
        #endregion
        
        
        #region 地图工具相关
        public Vector2Int WorldPos2LogicPos(Vector3 worldPos) {
            return HexGridTool.Coordinate_Axial.ContinuityToDiscrete(worldPos.SwapYZ().ToVec2(), BlockFactory.BlockSize, false);
        }
        #endregion

        public void UpdateGreenRate() {
            float gr = 0;
            _blockMgr.MapBlockForEach_Y((_) => {
                gr += _.greenRate;
            });
            GamePlayEnter.MapMgr.GreenRate = gr / _map.Count *100;
        }
    }
}
