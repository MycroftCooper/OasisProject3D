using System.Collections.Generic;
using cfg;
using cfg.BuildingSystem;
using OasisProject3D.UI.GameMainUIPackage;
using QuickGameFramework.Runtime;
using UnityEngine;

namespace OasisProject3D.BuildingSystem {
    public class BuildingManager : Singleton<BuildingManager> {
        public DTBuildingConfig BuildingCfg => GameEntry.DataTableMgr.Tables.DTBuildingConfig;
        public BuildingFactory Factory => BuildingFactory.Instance;

        private List<BuildingCtrl> _buildingCtrls;

        public void ConstructNewBuilding(string buildingKey) {
            _buildingCtrls ??= new List<BuildingCtrl>();
            
            var newBuilding = Factory.CreateEntity(buildingKey);
            newBuilding.OnBuildEnd += (_, isSuccess) => {
                if (!isSuccess) {
                    DestroyBuilding(_);
                    GameEntry.UIMgr.GetUIInstance<MainPageCtrl>("MainPage").OpenOrCloseBuildingList(true);
                    return;
                }
                _buildingCtrls.Add(newBuilding);
            };
        }

        public void DestroyBuilding(BuildingCtrl target) {
            _buildingCtrls.Remove(target);
            Object.Destroy(target.gameObject);
        }

        public void UpdateBuildings() {
            _buildingCtrls.ForEach(_=>_.UpdateBuilding());
        }

        #region 查询相关
        public bool CanAffordConstructCosts(string buildingKey) {
            // todo: 判断是否有足够的材料建造建筑
            return true;
        }
        
        public bool IsBuildingUnlock(string buildingKey) {
            // todo: 判断建筑是否解锁
            return true;
        }

        public List<string> GetBuildingKeys(EBuildingType buildingType) {
            List<string> output = new List<string>();
            foreach (var dataRow in BuildingCfg.DataList) {
                if (buildingType == EBuildingType.Any) {
                    output.Add(dataRow.Id);
                    continue;
                }
                if (dataRow.BuildingType.HasFlag(buildingType)) {
                    output.Add(dataRow.Id);
                }
            }
            return output;
        }

        public string GetBuildingName(string buildingKey) {
            return BuildingCfg[buildingKey].Name;
        }
        #endregion
    }
}
