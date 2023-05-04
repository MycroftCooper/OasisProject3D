using System.Collections.Generic;
using System.Linq;
using cfg;
using cfg.BuildingSystem;
using OasisProject3D.UI.GameMainUIPackage;
using QuickGameFramework.Runtime;
using UnityEngine;

namespace OasisProject3D.BuildingSystem {
    public class BuildingManager : IModule {
        #region 模块API相关

        public int Priority { get; set; }
        public bool IsFrameworkModule => false;
        public bool IsManualUpdate => true;
        public void OnModuleCreate(params object[] createParam) {
            Factory = new BuildingFactory();
        }

        public void OnModuleUpdate(float intervalSeconds) {
            UpdateBuildings();
        }

        public void OnModuleFixedUpdate(float intervalSeconds) {
            throw new System.NotImplementedException();
        }

        public void OnModuleDestroy() {
            throw new System.NotImplementedException();
        }

        #endregion
        
        public DTBuildingConfig BuildingCfg => GameEntry.DataTableMgr.Tables.DTBuildingConfig;
        public BuildingFactory Factory { get; private set; }

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
                    output.Add(dataRow.Key);
                    continue;
                }
                if (dataRow.BuildingType.HasFlag(buildingType)) {
                    output.Add(dataRow.Key);
                }
            }
            return output;
        }

        public int Key2ID(string key) {
            if (BuildingCfg.DataMap.TryGetValue(key, out var result)) {
                return result.Id;
            }
            QLog.Error($"BuildingManager>Key2ID>Key:{key}不存在，请检查配表!");
            return -1;
        }
        
        public string ID2Key(int id) {
            var result = BuildingCfg.DataMap.Values.FirstOrDefault(_ => _.Id == id);
            if (result != default) {
                return result.Key;
            }
            QLog.Error($"BuildingManager>Key2ID>ID:{id}不存在，请检查配表!");
            return default;
        }

        public string GetBuildingName(string buildingKey) {
            return BuildingCfg[buildingKey].Name;
        }
        #endregion
    }
}
