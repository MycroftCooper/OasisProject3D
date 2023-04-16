using System.Collections.Generic;
using Borodar.FarlandSkies.LowPoly;
using UnityEngine;
using Sirenix.OdinInspector;
using MycroftToolkit.QuickCode;
using MycroftToolkit.MathTool;
using cfg.MapSystem;
using cfg;
using MycroftToolkit.DiscreteGridToolkit;
using MycroftToolkit.DiscreteGridToolkit.Hex;
using QuickGameFramework.Runtime;

namespace OasisProject3D.MapSystem {
    public class MapManager : IModule {
        #region 地图配置相关
        [TitleGroup("地图生成相关", order: 0)]
        [TitleGroup("地图生成相关"), ShowInInspector, LabelText("随机种子")]
        public int seed = 114514;
        [TitleGroup("地图生成相关"), ShowInInspector, LabelText("地形落差"), Range(max: 7f, min: 0f)]
        public float steepParameter = 5f;
        [TitleGroup("地图生成相关"), ShowInInspector, LabelText("地形密度"), Range(max: 0.4f, min: 0f)]
        public float densityParameter = 0.1f;
        [TitleGroup("地图生成相关"), ShowInInspector, LabelText("地图大小")]
        public Vector2Int mapSize = new Vector2Int(30, 50);

        [TitleGroup("地图生成相关"), ShowInInspector, LabelText("地块工厂")]
        private BlockFactory _factory;
        [TitleGroup("地图生成相关"), ShowInInspector, LabelText("混合区域大小")]
        public int mixingAreaSize = 3;

        [TitleGroup("地图刷新相关", order: 1)]
        public Dictionary<Vector2Int, BlockCtrl> Map;
        [TitleGroup("地图刷新相关"), ShowInInspector, LabelText("全图植被覆盖率")]
        public float vegetationCoverage;

        public Dictionary<EBlockType, BlockConfig> BlockConf;
        #endregion
        public QuickRandom Random;
        [TitleGroup("地图刷新相关"), ShowInInspector, LabelText("随机开始范围")]
        public float randomStartRange;
        [TitleGroup("地图刷新相关"), ShowInInspector, LabelText("地图刷新速度范围")]
        public Vector2Int updateSpeedRange;
        private int _updateSpeed = 1;
        [TitleGroup("地图刷新相关"), ShowInInspector, LabelText("地图刷新速度")]
        public int UpdateSpeed {
            get => _updateSpeed;
            set {
                if (value == _updateSpeed) return;
                _updateSpeed = Mathf.Clamp(value, updateSpeedRange.x, updateSpeedRange.y);
                MapBlockForEach_Y((x) =>
                    Timer.Register(
                        QuickRandom.Simple.GetFloat(randomStartRange),
                        () => x.Ticker.Interval = x.InfectionData.Time / _updateSpeed
                     )
                );
            }
        }

        public int Priority { get; set; }
        public bool IsFrameworkModule => false;
        public bool IsManualUpdate => false;
        public void OnModuleCreate(params object[] createParam) {
            Init();
            InitMap();
            MapBlockForEach_Y((x) => x.Init(randomStartRange));
            SkyboxDayNightCycle​.Instance.TimeOfDay = 50;
        }

        public void OnModuleUpdate(float intervalSeconds) {
            SkyboxDayNightCycle​.Instance.TimeOfDay += (Time.deltaTime / 1000) * 100f;
            SkyboxController.Instance.CloudsRotation += (Time.deltaTime / 1000) * 100f;
        }

        public void OnModuleDestroy() {
        }
        
        public void Init() {
            BlockConf = GameEntry.DataTableMgr.Tables.DTBlockConfig.DataMap;
            
            Random = new QuickRandom(seed);
            Random.Noise.SetNoiseType(QuickNoise.NoiseType.Cellular);
            Random.Noise.SetCellularReturnType(QuickNoise.CellularReturnType.CellValue);
            Random.Noise.SetFrequency(densityParameter);
            
            _factory = BlockFactory.Instance;
            _factory.Init();
        }

        public void InitMap(MapData data = null) {
            Map = new Dictionary<Vector2Int, BlockCtrl>();

            int desertCol = (int)(mapSize.x * BlockConf[EBlockType.Desert].GenerateRate);
            int gobiCol = (int)(mapSize.x * (BlockConf[EBlockType.Desert].GenerateRate + BlockConf[EBlockType.Gobi].GenerateRate));
            int oasisCol = mapSize.x - desertCol - gobiCol;

            int px2 = mapSize.x;
            for (int x = 0; x < px2; x++) {
                int t = x;
                for (int y = 0; y < mapSize.y; y++) {
                    EBlockType blockType;
                    if (y < desertCol - mixingAreaSize)
                        blockType = EBlockType.Desert;

                    else if (y < desertCol)
                        blockType = Random.GetBool() ? EBlockType.Desert : EBlockType.Gobi;

                    else if (y < desertCol + gobiCol - mixingAreaSize)
                        blockType = EBlockType.Gobi;

                    else if (y < desertCol + gobiCol)
                        blockType = Random.GetBool() ? EBlockType.Gobi : EBlockType.Oasis;
                    else
                        blockType = EBlockType.Oasis;
                    Vector2Int pos = new Vector2Int(x, y);
                    BlockCtrl newBlock = _factory.CreateEntity($"Block_{pos.ToString()}",(pos, blockType));
                    Map.Add(pos, newBlock);

                    if (y % 2 != 0) x--;
                }
                x = t;
            }
        }

        public MapData GetMapData() {
            MapData data = new MapData();
            return data;
        }

        #region 地图工具相关
        public bool HasBlock(Vector2Int pos) => Map.ContainsKey(pos);
        public BlockCtrl GetBlock(Vector2Int pos) => HasBlock(pos) ? Map[pos] : null;

        public Vector2Int WorldPos2LogicPos(Vector3 worldPos) {
            return HexGridTool.Coordinate_Axial.ContinuityToDiscrete(worldPos.SwapYZ().ToVec2(), BlockFactory.BlockSize, false);
        }
        public BlockCtrl GetBlock(Vector3 worldPos) {
            var logicPos = HexGridTool.Coordinate_Axial.ContinuityToDiscrete(worldPos.SwapYZ().ToVec2(), BlockFactory.BlockSize, false);
            return GetBlock(logicPos);
        }
        
        public List<BlockCtrl> GetBlocks(List<Vector2Int> poses) {
            List<BlockCtrl> output = new List<BlockCtrl>();
            foreach (Vector2Int pos in poses) {
                BlockCtrl bc = GetBlock(pos);
                if (bc != null) output.Add(bc);
            }
            return output;
        }

        public EBlockType GetBlockTypeByVc(float vc) {
            if (vc < BlockConf[EBlockType.Desert].GreennessRange.y) return EBlockType.Desert;
            if (vc < BlockConf[EBlockType.Gobi].GreennessRange.y) return EBlockType.Gobi;
            return EBlockType.Oasis;
        }
        public delegate void MapBlockForeachCallback(BlockCtrl theBlock);
        public void MapBlockForEach_X(MapBlockForeachCallback callback) {
            int px1 = 0;
            int px2 = mapSize.x;
            for (int y = 0; y < mapSize.y; y++) {
                for (int x = px1; x < px2; x++) {
                    callback(Map[new Vector2Int(x, y)]);
                }
                if (y % 2 != 0) {
                    px1--;
                    px2--;
                }
            }
        }
        public void MapBlockForEach_Y(MapBlockForeachCallback callback) {
            int px2 = mapSize.x;
            for (int x = 0; x < px2; x++) {
                int t = x;
                for (int y = 0; y < mapSize.y; y++) {
                    callback(Map[new Vector2Int(x, y)]);
                    if (y % 2 != 0) x--;
                }
                x = t;
            }
        }
        #endregion
    }
}
