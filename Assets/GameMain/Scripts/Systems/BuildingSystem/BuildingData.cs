using cfg;
using cfg.BuildingSystem;
using UnityEngine;

namespace OasisProject3D.BuildingSystem {
    public struct BuildingData {
        public EBuildingType BuildingType;
        public BuildingConfig BaseConf;
        public Vector2Int BlockPos;
        public Vector2 HPRange;
        private float _HP;
        public float HP {
            get => _HP;
            set {
                if (_HP == value) return;
                _HP = Mathf.Clamp(value, HPRange.x, HPRange.y);
            }
        }
    }
}
