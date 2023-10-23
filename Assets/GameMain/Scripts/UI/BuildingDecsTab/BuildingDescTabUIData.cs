using System.Collections.Generic;
using OasisProject3D.BlockSystem;
using OasisProject3D.BuildingSystem;
using UnityEngine;

namespace OasisProject3D.UI.GameMainUIPackage {
    internal enum BuildingOrBlockTabUICmd {
        ChangeBuilding,
        DeleteBuilding,
        UpgradeBuilding,
        RebuildBuilding,
        MoveBuilding,
        SwitchBuildingOpenOrClose,
    }
    
    internal struct BuildingOrBlockTabUIData {
        public bool NeedHide;
        public bool IsBlock;
        public BuildingDescTabUIData BuildingData;
        public BlockDescTabUIData BlockData;
    }

    internal struct BuildingDescTabUIData {
        public string BuildingID;
        public string BuildingName;
        public string BuildingDesc;
        public Sprite BuildingIcon;
        public string BuildingState;
        public BuildingCtrl Ctrl;
        public Dictionary<Sprite, Vector2Int> CostIconDict;
        public Dictionary<Sprite, float> ProduceResIconDict;
        public Dictionary<Sprite, float> ConsumeResIconDict;
    }

    internal struct BlockDescTabUIData {
        public string BlockID;
        public BlockCtrl Ctrl;
    }
}