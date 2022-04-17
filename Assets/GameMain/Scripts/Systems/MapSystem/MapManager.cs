﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

using MycroftToolkit.QuickCode;
using MycroftToolkit.MathTool;
using Sirenix.Serialization;

namespace OasisProject3D.MapSystem {
    public enum EBlockType {
        Desert,
        Gobi,
        Oasis,
    }
    public class MapManager : MonoSingleton<MapManager> {
        #region 地图配置相关
        [TitleGroup("地图生成相关", order: 0)]
        [TitleGroup("地图生成相关"), ShowInInspector, LabelText("地图大小")]
        public static Vector2Int MapSize = new Vector2Int(5, 5);
        [TitleGroup("地图生成相关"), ShowInInspector, LabelText("地块生成比例")]
        public static Dictionary<EBlockType, float> BlockTypeRange_Generate = new Dictionary<EBlockType, float> {
            {EBlockType.Desert, 0.2f},
            {EBlockType.Gobi, 0.3f},
            {EBlockType.Oasis, 0.5f},
        };
        [TitleGroup("地图生成相关"), ShowInInspector, LabelText("地块工厂")]
        private BlockFactory factory;
        public int MixingAreaSize = 3;
        [TitleGroup("地图刷新相关", order: 1)]
        public Dictionary<Vector2Int, BlockCtrl> Map;
        [TitleGroup("地图刷新相关"), ShowInInspector, LabelText("全图植被覆盖率")]
        public float VegetationCoverage;
        [TitleGroup("地图刷新相关"), ShowInInspector, LabelText("地块转化值")]
        public static Dictionary<EBlockType, Vector2> BlockTypeRange_VC = new Dictionary<EBlockType, Vector2> {
            {EBlockType.Desert, new Vector2(0f, 0.3f)},
            {EBlockType.Gobi, new Vector2(0.3f, 0.8f)},
            {EBlockType.Oasis, new Vector2(0.8f, 1f)},
        };
        #endregion

        private void Start() {
            factory = BlockFactory.Instance;
            InitMap();
        }

        public void InitMap(MapData data = null) {
            Map = new Dictionary<Vector2Int, BlockCtrl>();

            int desertCol = (int)(MapSize.x * BlockTypeRange_VC[EBlockType.Desert].y - BlockTypeRange_VC[EBlockType.Desert].x);
            int gobiCol = (int)(MapSize.x * BlockTypeRange_VC[EBlockType.Gobi].y - BlockTypeRange_VC[EBlockType.Gobi].x);
            int oasisCol = MapSize.x - desertCol - gobiCol;

            int index = 0;
            for (int i = 0; i < MapSize.y; i++) {
                for (int j = 0; j < MapSize.x; j++) {
                    EBlockType blockType;
                    if (j < desertCol - MixingAreaSize)
                        blockType = EBlockType.Desert;

                    else if (j < desertCol)
                        if (QuickRandom.SimpleRandom.GetBool())
                            blockType = EBlockType.Desert;
                        else
                            blockType = EBlockType.Gobi;

                    else if (j < desertCol + gobiCol - MixingAreaSize)
                        blockType = EBlockType.Gobi;

                    else if (j < desertCol + gobiCol)
                        if (QuickRandom.SimpleRandom.GetBool())
                            blockType = EBlockType.Gobi;
                        else
                            blockType = EBlockType.Oasis;

                    else
                        blockType = EBlockType.Oasis;
                    Vector2Int pos = new Vector2Int(i, j);
                    BlockCtrl newBlock = factory.CreateHexBlock(pos, blockType);
                    Map.Add(pos, newBlock);
                    index++;
                }
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

        public static EBlockType GetBlockTypeByVC(float vc) {
            if (vc < BlockTypeRange_VC[EBlockType.Desert].y) return EBlockType.Desert;
            if (vc < BlockTypeRange_VC[EBlockType.Gobi].y) return EBlockType.Desert;
            return EBlockType.Oasis;
        }
    }
}
