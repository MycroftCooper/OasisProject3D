using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

using MycroftToolkit.QuickCode;


namespace OasisProject3D.MapSystem {
    public enum EBlockType {
        Desert,
        Gobi,
        Oasis,
    }
    public class MapManager : MonoSingleton<MapManager> {
        #region 地图配置相关
        [ShowInInspector, Title("地图大小")]
        public static Vector2Int MapSize;
        [ShowInInspector, Title("地块大小")]
        public static float BlockSize;
        [ShowInInspector, Title("地块间距")]
        public static float BlockDistance { get => BlockSize * 2 * Mathf.Cos(30 * Mathf.Deg2Rad); }

        [ShowInInspector, Title("地块转化值")]
        public static Dictionary<EBlockType, Vector2> BlockTypeRange_VC = new Dictionary<EBlockType, Vector2> {
            {EBlockType.Desert, new Vector2(0f, 0.3f)},
            {EBlockType.Gobi, new Vector2(0.3f, 0.8f)},
            {EBlockType.Oasis, new Vector2(0.8f, 1f)},
        };
        [ShowInInspector, Title("地块生成比例")]
        public static Dictionary<EBlockType, float> BlockTypeRange_Generate = new Dictionary<EBlockType, float> {
            {EBlockType.Desert, 0.2f},
            {EBlockType.Gobi, 0.3f},
            {EBlockType.Oasis, 0.5f},
        };
        #endregion

        public Dictionary<Vector3, BlockCtrl> Map;
        public float VegetationCoverage;


        public void InitMap(MapData data = null) {
            //Map = new HexBlockLogic[MapSize.y][];

            //int desertCol = (int)(MapSize.x * DesertRate);
            //int gobiCol = (int)(MapSize.x * GobiRate);
            //int oasisCol = MapSize.x - desertCol - gobiCol;

            //int index = 0;
            //for (int i = 0; i < MapSize.y; i++) {
            //    Map[i] = new HexBlockLogic[MapSize.x];
            //    for (int j = 0; j < MapSize.x; j++) {
            //        Vector2Int position = new Vector2Int(i, j);
            //        HexBlockLogic landBlock;
            //        //if (j < desertCol - MixingAreaSize)
            //        //    landBlock = new LandBlock(index, position, BlockType.Desert);
            //        //else if (j < desertCol)
            //        //    if (Random.Range(0f, 1f) < 0.5)
            //        //        landBlock = new LandBlock(index, position, BlockType.Desert);
            //        //    else
            //        //        landBlock = new LandBlock(index, position, BlockType.Gobi);

            //        //else if (j < desertCol + gobiCol - MixingAreaSize)
            //        //    landBlock = new LandBlock(index, position, BlockType.Gobi);
            //        //else if (j < desertCol + gobiCol)
            //        //    if (Random.Range(0f, 1f) < 0.5)
            //        //        landBlock = new LandBlock(index, position, BlockType.Gobi);
            //        //    else
            //        //        landBlock = new LandBlock(index, position, BlockType.Oasis);
            //        //else
            //        //    landBlock = new LandBlock(index, position, BlockType.Oasis);
            //        //Map[i][j] = landBlock;
            //        index++;
            //    }
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
