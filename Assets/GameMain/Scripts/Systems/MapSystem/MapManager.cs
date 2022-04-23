using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

using MycroftToolkit.QuickCode;
using MycroftToolkit.MathTool;
using Sirenix.Serialization;
using MycroftToolkit.DiscreteGridToolkit.Hex;
using System;

namespace OasisProject3D.MapSystem {
    public enum EBlockType {
        Desert,
        Gobi,
        Oasis,
    }
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
        [TitleGroup("地图生成相关"), ShowInInspector, LabelText("地块生成比例")]
        public static Dictionary<EBlockType, float> BlockTypeRange_Generate = new Dictionary<EBlockType, float> {
            {EBlockType.Desert, 0.2f},
            {EBlockType.Gobi, 0.3f},
            {EBlockType.Oasis, 0.5f},
        };
        [TitleGroup("地图生成相关"), ShowInInspector, LabelText("地块工厂")]
        private BlockFactory factory;
        [TitleGroup("地图生成相关"), ShowInInspector, LabelText("混合区域大小")]
        public int MixingAreaSize = 3;

        [TitleGroup("地图刷新相关", order: 1)]
        public Dictionary<Vector2Int, BlockCtrl> Map;
        [TitleGroup("地图刷新相关"), ShowInInspector, LabelText("全图植被覆盖率")]
        public float VegetationCoverage;
        [TitleGroup("地图刷新相关"), ShowInInspector, LabelText("刷新规则")]
        public static Dictionary<EBlockType, BlockInfectionConf> DefaultBlockInfectionConf = new Dictionary<EBlockType, BlockInfectionConf> {
                {EBlockType.Desert, new BlockInfectionConf{
                    VCRange = new Vector2(0f, 0.3f),
                    Infection_Range = 1,
                    Infection_Factor = 1f,
                    Infection_Time = 1f,
                }},
                { EBlockType.Gobi, new BlockInfectionConf(){
                    VCRange = new Vector2(0f, 0.3f),
                    Infection_Range = 1,
                    Infection_Factor = 1f,
                    Infection_Time = 1f,
                }},
                { EBlockType.Oasis, new BlockInfectionConf(){
                    VCRange = new Vector2(0f, 0.3f),
                    Infection_Range = 1,
                    Infection_Factor = 1f,
                    Infection_Time = 1f,
                }},
        };
        #endregion
        public QuickRandom Random;
        private void Start() {
            Init();
            InitMap();
        }
        public void Init() {
            Random = new QuickRandom(Seed);
            Random.Noise.SetNoiseType(QuickNoise.NoiseType.Cellular);
            Random.Noise.SetCellularReturnType(QuickNoise.CellularReturnType.CellValue);
            Random.Noise.SetFrequency(DensityParameter);
            factory = BlockFactory.Instance;
        }

        public void InitMap(MapData data = null) {
            Map = new Dictionary<Vector2Int, BlockCtrl>();

            int desertCol = (int)(MapSize.x * BlockTypeRange_Generate[EBlockType.Desert]);
            int gobiCol = (int)(MapSize.x * (BlockTypeRange_Generate[EBlockType.Desert] + BlockTypeRange_Generate[EBlockType.Gobi]));
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
        public void UpdateMap() {

        }
        public MapData GetMapData() {
            MapData data = new MapData();
            return data;
        }
        public void LoadMapData(MapData data) {

        }

        #region 地图工具相关
        public static EBlockType GetBlockTypeByVC(float vc) {
            if (vc < DefaultBlockInfectionConf[EBlockType.Desert].VCRange.y) return EBlockType.Desert;
            if (vc < DefaultBlockInfectionConf[EBlockType.Gobi].VCRange.y) return EBlockType.Gobi;
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
