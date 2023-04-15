using System;
using System.Collections.Generic;
using cfg;
using cfg.BuildingSystem;
using QuickGameFramework.Runtime;
using UnityEngine.U2D;

namespace OasisProject3D.BuildingSystem {
    public class BuildingManager : Singleton<BuildingManager> {
        public DTBuildingConfig BuildingCfg => GameEntry.DataTableMgr.Tables.DTBuildingConfig;

        #region 查询相关
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
