using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

using MycroftToolkit.QuickCode;
using MycroftToolkit.MathTool;
using Sirenix.Serialization;
using MycroftToolkit.DiscreteGridToolkit.Hex;
using System;
using cfg.MapSystem;
using cfg;
using Borodar.FarlandSkies.LowPoly;

namespace OasisProject3D.MapSystem {
    public class MapManager : MonoSingleton<MapManager> {
        #region 地图配置相关
        [TitleGroup("地图生成相关", order: 0)]
        [TitleGroup("地图生成相关"), ShowInInspector, LabelText("随机种子")]
        public int Seed;
        [TitleGroup("地图生成相关"), ShowInInspector, LabelText("地形落差"), Range(max: 7f, min: 0f)]
        public float SteepParameter = 5f;
        [TitleGroup("地图生成相关"), ShowInInspector, LabelText("地形密度"), Range(max: 0.4f, min: 0f)]
        public float DensityParameter = 0.1f;
        [TitleGroup("地图生成相关"), ShowInInspector, LabelText("地图大小")]
        public Vector2Int MapSize = new Vector2Int(30, 50);

        [TitleGroup("地图生成相关"), ShowInInspector, LabelText("地块工厂")]
        private BlockFactory factory;
        [TitleGroup("地图生成相关"), ShowInInspector, LabelText("混合区域大小")]
        public int MixingAreaSize = 3;

        [TitleGroup("地图刷新相关", order: 1)]
        public Dictionary<Vector2Int, BlockCtrl> Map;
        [TitleGroup("地图刷新相关"), ShowInInspector, LabelText("全图植被覆盖率")]
        public float VegetationCoverage;

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
                        QuickRandom.simple.GetFloat(randomStartRange),
                        () => x.ticker.Interval = x.infectionData.Time / _updateSpeed
                     )
                );
            }
        }

        private void Start() {
            Init();
            InitMap();
            MapBlockForEach_Y((x) => x.Init(randomStartRange));
            Time.timeScale = 10;
            SkyboxDayNightCycle​.Instance.TimeOfDay = 50;
        }
        private void Update() {
            //SkyboxDayNightCycle​.Instance.TimeOfDay += (Time.deltaTime / 1000) * 100f;
            //SkyboxController.Instance.CloudsRotation += (Time.deltaTime / 1000) * 100f;
        }
        public void Init() {
            BlockConf = DataManager.Instance.Tables.DTBlockConfig.DataMap;


            Random = new QuickRandom(Seed);
            Random.Noise.SetNoiseType(QuickNoise.NoiseType.Cellular);
            Random.Noise.SetCellularReturnType(QuickNoise.CellularReturnType.CellValue);
            Random.Noise.SetFrequency(DensityParameter);
            factory = BlockFactory.Instance;
        }

        public void InitMap(MapData data = null) {
            Map = new Dictionary<Vector2Int, BlockCtrl>();

            int desertCol = (int)(MapSize.x * BlockConf[EBlockType.Desert].GenerateRate);
            int gobiCol = (int)(MapSize.x * (BlockConf[EBlockType.Desert].GenerateRate + BlockConf[EBlockType.Gobi].GenerateRate));
            int oasisCol = MapSize.x - desertCol - gobiCol;

            int px_2 = MapSize.x;
            for (int x = 0; x < px_2; x++) {
                int t = x;
                for (int y = 0; y < MapSize.y; y++) {
                    EBlockType blockType;
                    if (y < desertCol - MixingAreaSize)
                        blockType = EBlockType.Desert;

                    else if (y < desertCol)
                        if (Random.GetBool())
                            blockType = EBlockType.Desert;
                        else
                            blockType = EBlockType.Gobi;

                    else if (y < desertCol + gobiCol - MixingAreaSize)
                        blockType = EBlockType.Gobi;

                    else if (y < desertCol + gobiCol)
                        if (Random.GetBool())
                            blockType = EBlockType.Gobi;
                        else
                            blockType = EBlockType.Oasis;
                    else
                        blockType = EBlockType.Oasis;
                    Vector2Int pos = new Vector2Int(x, y);
                    BlockCtrl newBlock = factory.CreateHexBlock(pos, blockType);
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
        public List<BlockCtrl> GetBlocks(List<Vector2Int> poses) {
            List<BlockCtrl> output = new List<BlockCtrl>();
            foreach (Vector2Int pos in poses) {
                BlockCtrl bc = GetBlock(pos);
                if (bc != null) output.Add(bc);
            }
            return output;
        }

        public EBlockType GetBlockTypeByVC(float vc) {
            if (vc < BlockConf[EBlockType.Desert].GreennessRange.y) return EBlockType.Desert;
            if (vc < BlockConf[EBlockType.Gobi].GreennessRange.y) return EBlockType.Gobi;
            return EBlockType.Oasis;
        }
        public delegate void MapBlockForeachCallback(BlockCtrl theBlock);
        public void MapBlockForEach_X(MapBlockForeachCallback callback) {
            int px_1 = 0;
            int px_2 = px_1 + MapSize.x;
            for (int y = 0; y < MapSize.y; y++) {
                for (int x = px_1; x < px_2; x++) {
                    callback(Map[new Vector2Int(x, y)]);
                }
                if (y % 2 != 0) {
                    px_1--;
                    px_2--;
                }
            }
        }
        public void MapBlockForEach_Y(MapBlockForeachCallback callback) {
            int px_2 = MapSize.x;
            for (int x = 0; x < px_2; x++) {
                int t = x;
                for (int y = 0; y < MapSize.y; y++) {
                    callback(Map[new Vector2Int(x, y)]);
                    if (y % 2 != 0) x--;
                }
                x = t;
            }
        }
        #endregion
    }
}
