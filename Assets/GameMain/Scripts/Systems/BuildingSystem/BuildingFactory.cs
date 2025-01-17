﻿using System.Collections.Generic;
using cfg;
using cfg.BuildingSystem;
using QuickGameFramework.Runtime;
using UnityEngine;

namespace OasisProject3D.BuildingSystem {
    public class BuildingFactory : IEntityFactory<BuildingCtrl> {
        public DTBuildingConfig BuildingCfg => GameEntry.DataTableMgr.Tables.DTBuildingConfig;
        private AssetManager AssetMgr => GameEntry.AssetMgr;
        private Dictionary<string, GameObject> _prefabs;
        private Dictionary<string, Material> _materials;
        private Dictionary<string, Sprite> _icons;

        public AssetLoadProgress PreLoadAsset() {
            var output = new AssetLoadProgress();
            _prefabs = new Dictionary<string, GameObject>();
            output += AssetMgr.LoadAssetsAsyncByTag<GameObject>("BuildingPrefab", (prefab,_) => {
                _prefabs.Add(prefab.name.Replace("_prefab", ""), prefab);
            });

            _materials = new Dictionary<string, Material>();
            output += AssetMgr.LoadAssetAsync<Material>("Building_transColor_material", target => { _materials.Add(target.name, target);});
            output += AssetMgr.LoadAssetAsync<Material>("Building_construct_material", target => { _materials.Add(target.name, target);});

            var projectAssetSetting = GameEntry.ConfigMgr.ProjectAssetSetting;
            _icons = new Dictionary<string, Sprite>();
            output += GameEntry.AssetMgr.LoadSubAssetsAsyncByTag<Sprite>("BuildingIcon", icons => {
                foreach (var icon in icons) {
                    _icons.Add(icon.name.Replace("_icon", ""), icon);
                }
            }, projectAssetSetting.uiAssetsPackageName);
            return output;
        }
        
        public void Init() {
            throw new System.NotImplementedException();
        }

        public BuildingCtrl CreateEntity(string entityID, object data = null) {
            if (!_prefabs.TryGetValue(entityID, out var buildingPrefab)) {
                QLog.Error($"BuildingFactory>创建实例失败！key:<{entityID}>的建筑预制体不存在!");
                return null;
            }

            GameObject buildingGo = Object.Instantiate(buildingPrefab);
            var output = buildingGo.GetComponent<BuildingCtrl>();

            output.Init(entityID, this, data ?? new BuildingData{Conf = GetBuildingConfig(entityID)});

            return output;
        }

        public void RecycleEntity(Entity entity) {
            throw new System.NotImplementedException();
        }

        #region 获取资源相关

        public BuildingConfig GetBuildingConfig(string key) {
            BuildingCfg.DataMap.TryGetValue(key, out var cfg);
            if (cfg != null) return cfg;
            QLog.Error($"BuildingFactor> 建筑配置查找失败，配表中不存在:{key}");
            return null;
        }
        
        public Material GetBuildingMaterial(string key) {
            if (!_materials.TryGetValue(key, out var output)) {
                QLog.Error($"BuildingFactory>Error> 材质<{key}>不存在，可能是没加载，也可能真的不存在!");
            }
            return output;
        }
        
        public Sprite GetBuildingIcon(string key){
            if (!_icons.TryGetValue(key, out var output)) {
                QLog.Warning($"BuildingFactory>Error> Icon<{key}>不存在，可能是没加载，也可能真的不存在!");
            }
            return output;
        }

        public List<Sprite> GetBuildingIcon(EBuildingType buildingType) {
            List<string> buildingKeys = GamePlayEnter.BuildingMgr.GetBuildingKeys(buildingType);
            if (buildingKeys.Count == 0) {
                QLog.Error($"BuildingFactory> 未找到<{buildingType}>类型的建筑Key!请检查建筑配表!");
                return null;
            }

            List<Sprite> output = new List<Sprite>();
            foreach (var key in buildingKeys) {
                var icon = GetBuildingIcon(key);
                if (icon != null) {
                    output.Add(icon); 
                }
            }

            return output;
        }

        public string GetBuildingName(string buildingKey) {
            var cfg = GetBuildingConfig(buildingKey);
            if (cfg != null) return cfg.Name;
            QLog.Error($"BuildingFactor> 查找名称失败，配表中不存在:{buildingKey}");
            return default;
        }

        public string GetBuildingDesc(string buildingKey) {
            var cfg = GetBuildingConfig(buildingKey);
            if (cfg != null) return cfg.Describe;
            QLog.Error($"BuildingFactor> 查找名称失败，配表中不存在:{buildingKey}");
            return default;
        }

        public string GetBuildingCoast(string buildingKey) {
            return default;
        }
        #endregion
        
    }
}
