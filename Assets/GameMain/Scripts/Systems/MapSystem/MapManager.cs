using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MycroftToolkit.QuickCode;


namespace OasisProject3D.MapSystem {
    public class MapManager : MonoSingleton<MapManager> {
        private MapData mapData;

        public void InitMap() {
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
    }
}
