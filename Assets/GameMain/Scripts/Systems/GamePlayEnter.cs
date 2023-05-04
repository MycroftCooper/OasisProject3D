using OasisProject3D.BlockSystem;
using OasisProject3D.BuildingSystem;
using OasisProject3D.MapSystem;
using OasisProject3D.ResourceSystem;
using QuickGameFramework.Runtime;

namespace OasisProject3D {
    public static class GamePlayEnter {
        public static MapManager MapMgr => GameEntry.GamePlayModuleMgr.GetModule<MapManager>();
        public static BlockManager BlockMgr => GameEntry.GamePlayModuleMgr.GetModule<BlockManager>();
        public static BuildingManager BuildingMgr => GameEntry.GamePlayModuleMgr.GetModule<BuildingManager>();
        public static ResourceManager ResMgr => GameEntry.GamePlayModuleMgr.GetModule<ResourceManager>();
    }
}