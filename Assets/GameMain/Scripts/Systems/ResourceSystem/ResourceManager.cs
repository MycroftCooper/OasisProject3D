using System.Collections.Generic;
using cfg;
using QuickGameFramework.Runtime;
using UnityEngine;

namespace OasisProject3D.ResourceSystem {
    public class ResourceManager : IModule  {
        public int Priority { get; set; }
        public bool IsFrameworkModule => false;
        public bool IsManualUpdate => true;

        private Dictionary<EResType, float> _resDict;
        private Dictionary<EResType, float> _storageSpaceDict;

        public EStorageStatus GetStorageStatus(EResType resType) {
            return default;
        }

        public void UpdateRes(ResRecorder recorder) {
            var profits = recorder.GetProfits();
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

        }

        public void OnModuleFixedUpdate(float intervalSeconds) {

        }

        public void OnModuleDestroy() {
            throw new System.NotImplementedException();
        }
    }
}