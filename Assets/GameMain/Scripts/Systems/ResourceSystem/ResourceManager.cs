using System;
using System.Collections.Generic;
using cfg;
using QuickGameFramework.Runtime;
using UnityEngine;

namespace OasisProject3D.ResourceSystem {
    public class ResourceManager : IModule  {
        #region 模块相关AIP
        public int Priority { get; set; }
        public bool IsFrameworkModule => false;
        public bool IsManualUpdate => true;
        public void OnModuleCreate(params object[] createParam) {
            
        }

        public void OnModuleUpdate(float intervalSeconds) {

        }

        public void OnModuleFixedUpdate(float intervalSeconds) {

        }

        public void OnModuleDestroy() {
            throw new NotImplementedException();
        }
        #endregion

        #region Icon相关
        private Dictionary<EResType, Sprite> _resIconDict;
        public AssetLoadProgress PreLoadAsset() {
            var output = new AssetLoadProgress();
            var projectAssetSetting = GameEntry.ConfigMgr.ProjectAssetSetting;
            _resIconDict = new Dictionary<EResType, Sprite>();
            output += GameEntry.AssetMgr.LoadSubAssetsAsyncByTag<Sprite>("ResIcon", icons => {
                foreach (var icon in icons) {
                    string resTypeStr = icon.name.Replace("_icon", "");
                    EResType resType = Enum.Parse<EResType>(resTypeStr);
                    _resIconDict.Add(resType, icon);
                }
            }, projectAssetSetting.uiAssetsPackageName);
            return output;
        }

        public Sprite GetResIcon(EResType resType) {
            if (_resIconDict.TryGetValue(resType, out var icon)) return icon;
            QLog.Warning($"ResourceManager>ResIcon> 未找到<{resType}>资源的Icon!");
            return null;
        }
        #endregion

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
    }
}