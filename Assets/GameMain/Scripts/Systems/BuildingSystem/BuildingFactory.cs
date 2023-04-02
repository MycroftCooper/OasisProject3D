using System.Collections.Generic;
using QuickGameFramework.Runtime;
using UnityEngine;

namespace OasisProject3D.BuildingSystem {
    public class BuildingFactory : Singleton<BuildingFactory>, IEntityFactory<BuildingCtrl> {
        private AssetManager AssetMgr => GameEntry.AssetMgr;
        private Dictionary<string, GameObject> _prefabs;
        private Dictionary<string, Material> _materials;
        
        public AssetLoadProgress PreLoadAsset() {
            var output = new AssetLoadProgress();
            _prefabs = new Dictionary<string, GameObject>();
            output += AssetMgr.LoadAssetAsync<GameObject>("Building_centerTower_prefab", target => { _prefabs.Add(target.name, target);});
            output += AssetMgr.LoadAssetAsync<GameObject>("Building_firePowerStation_prefab", target => { _prefabs.Add(target.name, target);});

            _materials = new Dictionary<string, Material>();
            output += AssetMgr.LoadAssetAsync<Material>("Building_transColor_material", target => { _materials.Add(target.name, target);});
            output += AssetMgr.LoadAssetAsync<Material>("Building_construct_material", target => { _materials.Add(target.name, target);});
            return output;
        }

        public void Init() {
            throw new System.NotImplementedException();
        }

        public BuildingCtrl CreateEntity(string entityID, object data = null) {
            throw new System.NotImplementedException();
        }

        public void RecycleEntity(Entity entity) {
            throw new System.NotImplementedException();
        }

        public Material GetMaterial(string key) {
            if (!_materials.TryGetValue(key, out var output)) {
                QLog.Error($"BuildingFactory>Error> 材质{key}不存在，可能是没加载，也可能真的不存在!");
            }
            return output;
        }
    }
}
