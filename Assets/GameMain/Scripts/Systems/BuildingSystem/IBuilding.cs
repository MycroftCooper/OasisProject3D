using System.Collections;
using System.Collections.Generic;
using cfg;
using cfg.BuildingSystem;
using UnityEngine;

namespace OasisProject3D.BuildingSystem {
    public class Building_Base : MonoBehaviour {
        public EBuildingType buildingType;
        public BuildingConfig baseConf;
        public Vector2Int blockPos;
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

    public interface IBuilding_Functional {

    }
}
