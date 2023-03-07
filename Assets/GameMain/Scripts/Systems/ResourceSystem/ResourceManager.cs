using System.Collections;
using System.Collections.Generic;
using QuickGameFramework.Runtime;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace OasisProject3D.ResourceSystem {
    public class ResourceManager : IModule  {
        public int Priority { get; set; }
        public bool IsFrameworkModule => false;

        private Dictionary<EResourceType, float> _resDict;
        private Dictionary<EResourceType, float> _storageSpaceDict;

        public EStorageStatus GetStorageStatus(EResourceType resType) {
            return default;
        }

        public void UpdateRes(ResHandle handle) {
            var profits = handle.GetProfits();
            foreach (var kv in profits) {
                var resType = kv.Key;
                float resNum = _resDict[resType] + kv.Value;
                resNum = Mathf.Clamp(resNum, 0f, _storageSpaceDict[resType]);
                _resDict[resType] = resNum;
            }
        }

        public void OnModuleCreate(params object[] createParam) {
            throw new System.NotImplementedException();
        }

        public void OnModuleUpdate(float intervalSeconds) {
            throw new System.NotImplementedException();
        }

        public void OnModuleDestroy() {
            throw new System.NotImplementedException();
        }
    }
}