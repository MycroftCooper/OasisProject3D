using System;
using System.Collections.Generic;
using FairyGUI;
using MycroftToolkit.QuickCode;
using UnityEngine;
using UnityEngine.U2D;
using YooAsset;

namespace QuickGameFramework.Runtime {
    public class UIManager {
        private readonly ProjectAssetSetting _projectAssetSetting;
        private readonly Dictionary<string, AssetHandle> _handleDict;

        private SpriteAtlas _iconAtlas;

        public UIManager() {
            _projectAssetSetting = GameEntry.ConfigMgr.ProjectAssetSetting;
            _handleDict = new Dictionary<string, AssetHandle>();
            _uiCtrlDict = new GeneralDictionary<string>();
        }

        #region UI资源预加载相关
        public bool HasPreloadPackage(string uiPackageName) {
            return _handleDict.ContainsKey(uiPackageName);
        }

        public void PreloadPackage(string uiPackageName) {
            if (_handleDict.ContainsKey(uiPackageName)) {
                QLog.Error($"QuickGameFramework>UIMgr> FUI包[{uiPackageName}]加载失败！ 该FUI包已加载，请勿重复加载！");
                return;
            }
            UIPackage.AddPackage(uiPackageName, LoadFunc);
        }

        public void ReleasePreloadPackage(string uiPackageName) {
            if (_handleDict.ContainsKey(uiPackageName)) {
                QLog.Error($"QuickGameFramework>UIMgr> FUI包[{uiPackageName}]释放失败！ 该FUI包未加载！");
                return;
            }
            UIPackage.RemovePackage(uiPackageName);
            _handleDict[uiPackageName].Release();
            _handleDict.Remove(uiPackageName);
        }

        public void ReleaseAllPreloadPackage() {
            _handleDict.ForEach(_ => {
                UIPackage.RemovePackage(_.Key);
                _.Value.Release();
            });
            _handleDict.Clear();
        }

        // FairyGUI对接YooAssets加载UI包的方法
        private object LoadFunc(string name, string extension, Type type, out DestroyMethod method) {
            method = DestroyMethod.None;
            string location = _projectAssetSetting.uiResPath + name;
            var assetPackage = YooAssets.GetPackage(_projectAssetSetting.uiAssetsPackageName);
            var handle = assetPackage.LoadAssetSync(location, type);
            _handleDict.Add(name, handle);
            return handle.AssetObject;
        }
        #endregion

        #region UI组件动态生成相关
        private readonly GeneralDictionary<string> _uiCtrlDict;
        public GComponent CreateFguiComponentSync(string uiID, string pkgName, string componentName) {
            if (HasUIInstance(uiID)) {
                QLog.Error($"QuickGameFramework>UIMgr> UI实例已存在，UIId:{uiID}, componentName:{componentName}的UI创建失败！");
                return default;
            }
            GComponent view = UIPackage.CreateObject(pkgName,componentName).asCom;
            view.displayObject.gameObject.AddComponent<UIAssetLoader>();
            AddUIInstance(uiID, view);
            QLog.Log($"QuickGameFramework>UIMgr> UI实例创建成功！UIId:{uiID}, componentName:{componentName}");
            return view;
        }
        
        public void CreateFguiComponentASync(string uiID, string pkgName, string componentName, UIPackage.CreateObjectCallback callback) {
            if (HasUIInstance(uiID)) {
                QLog.Error($"QuickGameFramework>UIMgr> UI实例已存在，UIId:{uiID}, componentName:{componentName}的UI创建失败！");
                return;
            }
            UIPackage.CreateObjectAsync(pkgName,componentName, (_)=> {
                _.displayObject.gameObject.AddComponent<UIAssetLoader>();
                AddUIInstance(uiID, _);
                QLog.Log($"QuickGameFramework>UIMgr> UI实例创建成功！UIId:{uiID}, componentName:{componentName}");
                callback?.Invoke(_);
            });
        }

        public T CreateFguiComponent<T>(string uiID, Func<T> createInstanceFunc) where T : GComponent {
            if (HasUIInstance(uiID)) {
                QLog.Error($"QuickGameFramework>UIMgr> UI实例已存在，UIId:{uiID}, type:{typeof(T)}的UI创建失败！");
                return default;
            }
            if (createInstanceFunc == null) {
                QLog.Error($"QuickGameFramework>UIMgr> 实例创建方法为空，UIId:{uiID}, type:{typeof(T)}的UI创建失败！");
                return default;
            }
            T output = createInstanceFunc.Invoke();
            AddUIInstance(uiID, output);
            QLog.Log($"QuickGameFramework>UIMgr> UI实例创建成功！UIId:{uiID}, type:{typeof(T)}");
            return output;
        }

        public void DisposeFguiComponent(string uiID, GComponent target) {
            target.Dispose();
            RemoveUIInstance(uiID);
        }
        public void AddUIInstance(string uiID, object uiCtrl) {
            _uiCtrlDict.Add(uiID, uiCtrl);
        }
        public bool HasUIInstance(string uiID) {
            return _uiCtrlDict.ContainsKey(uiID);
        }
        public T GetUIInstance<T>(string uiID) {
            return _uiCtrlDict.Get<T>(uiID);
        }
        public void RemoveUIInstance(string uiID) {
            _uiCtrlDict.Remove(uiID);
        }

        #endregion

        public Sprite GetIcon(string iconID) {
            return _iconAtlas.GetSprite(iconID);
        }
    }
}