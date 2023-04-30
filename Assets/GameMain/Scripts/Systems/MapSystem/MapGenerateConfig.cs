using UnityEngine;

namespace OasisProject3D.MapSystem {
    [CreateAssetMenu(fileName = "MapGenerateConfigPreset", menuName = "OasisProject3D/MapGenerateConfig")]
    public class MapGenerateConfig : ScriptableObject {
        public int seed = 114514; // 随机种子
        public Vector2Int mapSize = new (30, 50); // 地图大小
        public float steepParameter = 5f;// 地形落差
        public float densityParameter = 0.1f; // 地形密度
        
        public int mixingAreaSize = 3;// 地块混合区域大小
    }
}