using System.Collections.Generic;
using cfg;
using UnityEngine;

namespace OasisProject3D.UI.GameMainUIPackage {
    internal enum MainPageUICommand { UpdateVegetationCoverage, UpdateResData, UpdateBuildingList }
    
    internal struct MainPageUIData {
        public float GreenRate;
        public Dictionary<EResType, float> ResData;
        public EBuildingType SelectedBuildingType;
        public Dictionary<EBuildingType, List<BuildingUIData>> BuildingUIDataList;
    }

    internal struct BuildingUIData {
        public string BuildingID;
        public EBuildingType BuildingType;
        public Sprite BuildingIcon;
        public bool IsUnlock;
        public bool CanBuild;
    }
}