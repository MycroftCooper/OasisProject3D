using System.Collections.Generic;
using cfg;
using cfg.BuildingSystem;
using QuickGameFramework.Runtime;

namespace OasisProject3D.BuildingSystem {
    public class BuildingManager : Singleton<BuildingManager> {
        public DTBuildingConfig BuildingCfg => GameEntry.DataTableMgr.Tables.DTBuildingConfig;

        public void ConstructNewBuilding(string buildingKey) {
            
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
        #endregion
    }
}
