using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OasisProject3D.UI.GameMainUIPackage {
    internal enum BuildingDescUICommand {
        OpenTab,
        CloseTab,
        RefreshTab,
    }

    internal struct BuildingDescTabUIData {
        public string BuildingID;
        public string BuildingName;
        public string BuildingDesc;
        public Sprite BuildingIcon;
    }
}