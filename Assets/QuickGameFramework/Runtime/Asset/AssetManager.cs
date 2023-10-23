using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MycroftToolkit.QuickCode;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooAsset;
using Object = UnityEngine.Object;

namespace QuickGameFramework.Runtime {
	public class AssetManager {
		private ProjectAssetSetting _projectAssetSetting;
		private Dictionary<string, ResourcePackage> _packages;

		public void Init(Action callBack = null) {
			_projectAssetSetting = GameEntry.ConfigMgr.ProjectAssetSetting;
			_packages = new Dictionary<string, ResourcePackage>();
			YooAssets.Initialize();
			GameEntry.CoroutineMgr.StartCoroutine(InitPackage(callBack));
		}

		#region 资源加载API
		public AssetHandle LoadAssetAsync<T>(string path, Action<T> callback ,string packageName = null) where T : Object {
			if (!GetAssetPackage(packageName, out ResourcePackage package)) {
				QLog.Error($"QuickGameFramework>Asset>资源<{path}>异步加载失败!");
				return null;
			}
			var handle = package.LoadAssetAsync<T>(path);
			if (callback != null) {
				handle.Completed += _=> callback((T)handle.AssetObject);
			}
			handle.Completed += LogLoadSuccess;
			return handle;
		}

		public SubAssetsHandle LoadSubAssetsAsync<T>(string path, Action<T[]> callback, string packageName = null) where T : Object {
			if (!GetAssetPackage(packageName, out ResourcePackage package)) {
				QLog.Error($"QuickGameFramework>Asset>资源<{path}>异步加载失败!");
				return null;
			}
			var handle = package.LoadSubAssetsAsync<T>(path);
			if (callback != null) {
				handle.Completed += _ => callback(_.GetSubAssetObjects<T>());
			}

			return handle;
		}

		public SubAssetsHandle[] LoadSubAssetsAsyncByTag<T>(string tag, Action<T[]> callback, string packageName = null) where T : Object {
			if (!GetAssetPackage(packageName, out ResourcePackage package)) {
				QLog.Error($"QuickGameFramework>Asset>Tag:<{tag}>相关资源异步加载失败!");
				return null;
			}
			
			AssetInfo[] infos = package.GetAssetInfos(tag);
			if (infos == null || infos.Length == 0) {
				QLog.Error($"QuickGameFramework>Asset>Tag:<{tag}>相关资源异步加载失败!tag不存在或该tag下无资源!");
				return null;
			}
			
			var output = new List<SubAssetsHandle>();
			foreach (var info in infos) {
				var path = info.Address;
				var handle = package.LoadSubAssetsAsync<T>(path);
				if (callback != null) {
					handle.Completed += _=> callback(_.GetSubAssetObjects<T>());
				}
				handle.Completed += LogLoadSuccess;
				output.Add(handle);
			}
			return output.ToArray();
		}

		public AssetHandle[] LoadAssetsAsyncByTag<T>(string tag, Action<T, string> callback, string packageName = null)
			where T : Object {
			if (!GetAssetPackage(packageName, out ResourcePackage package)) {
				QLog.Error($"QuickGameFramework>Asset>Tag:<{tag}>相关资源异步加载失败!");
				return null;
			}

			AssetInfo[] infos = package.GetAssetInfos(tag);
			if (infos == null || infos.Length == 0) {
				QLog.Error($"QuickGameFramework>Asset>Tag:<{tag}>相关资源异步加载失败!tag不存在或该tag下无资源!");
				return null;
			}

			var output = new List<AssetHandle>();
			foreach (var info in infos) {
				var path = info.Address;
				var handle = package.LoadAssetAsync<T>(path);
				if (callback != null) {
					handle.Completed += _=> callback((T)_.AssetObject, handle.GetAssetInfo().AssetPath);
				}
				handle.Completed += LogLoadSuccess;
				output.Add(handle);
			}
			return output.ToArray();
		}

		public AssetHandle LoadAssetSync<T>(out T asset ,string path, string packageName = null) where T : Object {
			if (!GetAssetPackage(packageName, out ResourcePackage package)) {
				QLog.Error($"QuickGameFramework>Asset>资源<{path}>同步加载失败!");
				asset = null;
				return null;
			}
			var handle = package.LoadAssetSync<T>(path);
			asset =(T) package.LoadAssetSync<T>(path).AssetObject;
			handle.Completed += LogLoadSuccess;

			return handle;
		}
		
		public AssetHandle[] LoadAssetsSyncByTag<T>(string tag, Action<T> callback, string packageName = null)
			where T : Object {
			if (!GetAssetPackage(packageName, out ResourcePackage package)) {
				QLog.Error($"QuickGameFramework>Asset>Tag:<{tag}>相关资源同步加载失败!");
				return null;
			}

			AssetInfo[] infos = package.GetAssetInfos(tag);
			if (infos == null || infos.Length == 0) {
				QLog.Error($"QuickGameFramework>Asset>Tag:<{tag}>相关资源同步加载失败!tag不存在或该tag下无资源!");
				return null;
			}

			var output = new List<AssetHandle>();
			foreach (var info in infos) {
				var path = info.Address;
				var handle = package.LoadAssetSync<T>(path);
				if (callback != null) {
					handle.Completed += _=> callback((T)handle.AssetObject);
				}
				handle.Completed += LogLoadSuccess;
				output.Add(handle);
			}
			return output.ToArray();
		}

		public SceneHandle LoadSceneAsync(string path, string packageName = null, LoadSceneMode sceneMode = LoadSceneMode.Single, bool activateOnLoad = true) {
			if (!GetAssetPackage(packageName, out ResourcePackage package)) {
				QLog.Error($"QuickGameFramework>Asset>场景<{path}>！异步加载失败!");
				return null;
			}
			SceneHandle handle = package.LoadSceneAsync(path, sceneMode, activateOnLoad);
			handle.Completed += _ => {
				QLog.Log($"QuickGameFramework>Asset>场景<{path}>！异步加载成功!");
			};
			return handle;
		}

		public AssetHandle LoadAndInitPrefabAsync(string path, (Transform parent, Vector3 pos, Quaternion rotation) gameObjectInfo ,Action<GameObject> callback = null ,string packageName = null) {
			AssetHandle handle = LoadAssetAsync<GameObject>(path, null, packageName);
			if (handle == null) {
				QLog.Error($"QuickGameFramework>Asset>预制体<{path}>！异步加载失败!");
				return null;
			}
			handle.Completed += (x) => {
				if (handle.AssetObject == null) return;
				var target = x.InstantiateSync(gameObjectInfo.pos, gameObjectInfo.rotation, gameObjectInfo.parent);
				callback?.Invoke(target);
				QLog.Log($"QuickGameFramework>Asset>预制体<{path}>！异步加载并实例化成功!");
			};
			handle.Completed += LogLoadSuccess;
			return handle;
		}
		
		public RawFileHandle[] LoadRawFileAsyncByTag(string tag, Action<byte[], string, string> callback, string packageName = null) {
			if (!GetAssetPackage(packageName, out ResourcePackage package)) {
				QLog.Error($"QuickGameFramework>Asset>Tag:<{tag}>相关资源异步加载失败!");
				return null;
			}

			AssetInfo[] infos = package.GetAssetInfos(tag);
			if (infos == null || infos.Length == 0) {
				QLog.Error($"QuickGameFramework>Asset>Tag:<{tag}>相关资源异步加载失败!tag不存在或该tag下无资源!");
				return null;
			}

			var output = new List<RawFileHandle>();
			foreach (var info in infos) {
				var path = info.Address;
				var handle = package.LoadRawFileAsync(path);
				if (callback != null) {
					handle.Completed += _=> callback(handle.GetRawFileData(), handle.GetRawFileText(), handle.GetRawFilePath());
				}
				handle.Completed += LogLoadSuccess;
				output.Add(handle);
			}
			return output.ToArray();
		}

		#endregion
		
		public bool ReleaseAsset(AssetHandle handle) {
			if (handle == null) {
				QLog.Error($"QuickGameFramework>Asset>资源释放失败，句柄为空!");
				return false;
			}
			handle.Release();
			return true;
		}

		public void UnloadAssets(string packageName = null, bool isForce = false) {
			if (string.IsNullOrEmpty(packageName)) {
				_packages.Values.ForEach((x) => {
					if (isForce) {
						x.ForceUnloadAllAssets();
						QLog.Log($"QuickGameFramework>Asset>资源包{packageName}所有资源强制卸载成功");
					}
					else {
						x.UnloadUnusedAssets();
						QLog.Log($"QuickGameFramework>Asset>资源包{packageName}未使用的资源卸载成功");
					}
				});
				UpdatePackageDict();
				return;
			}

			if (!GetAssetPackage(packageName, out ResourcePackage package)) {
				QLog.Error($"QuickGameFramework>Asset>资源包{packageName}卸载资源失败，可能未加载或不存在!");
				return;
			}

			if (isForce) {
				package.ForceUnloadAllAssets();
				QLog.Log($"QuickGameFramework>Asset>资源包{packageName}所有资源强制卸载成功");
			}
			else {
				package.UnloadUnusedAssets();
				QLog.Log($"QuickGameFramework>Asset>资源包{packageName}未使用的资源卸载成功");
			}

			UpdatePackageDict();
		}

		public static void LogLoadSuccess(HandleBase handle) {
			AssetInfo targetInfo = handle.GetAssetInfo();
			string logStr = $"\n寻址:<{targetInfo.Address}>\n类型:<{targetInfo.AssetType}>\n路径:<{targetInfo.AssetPath}>";
			if (handle.Status == EOperationStatus.Succeed) {
				QLog.Log($"QuickGameFramework>Asset>资源加载成功!{logStr}");
			} else {
				QLog.Error($"QuickGameFramework>Asset>资源加载失败!{logStr}");
			}
		}
		
		private void UpdatePackageDict() {
			List<string> needRemovePackages = _packages.Keys.Where(
				packageName => !YooAssets.HasPackage(packageName)
				).ToList();
			foreach (var packageName in needRemovePackages) {
				_packages.Remove(packageName);
			}
		}

		private bool GetAssetPackage(string packageName, out ResourcePackage package) {
			if (string.IsNullOrEmpty(packageName)) {
				packageName = _projectAssetSetting.defaultPackageName;
			}
			if (_packages.TryGetValue(packageName, out var packageT)) {
				package = packageT;
				return true;
			}
			package = YooAssets.TryGetPackage(packageName);
			if (package != null) {
				_packages.TryAdd(packageName, package);
				return true;
			}
			QLog.Warning($"QuickGameFramework>Asset>资源包<{packageName}>不存在!");
			return false;
		}

		private IEnumerator InitPackage(Action callBack) {
			var defaultPackageName = _projectAssetSetting.defaultPackageName;
			var playMode = _projectAssetSetting.playMode;
			var editorPlayMode = _projectAssetSetting.editorPlayMode;

			// 创建默认的资源包
			var package = YooAssets.TryGetPackage(defaultPackageName);
			if (package == null) {
				package = YooAssets.CreatePackage(defaultPackageName);
				YooAssets.SetDefaultPackage(package);
			}

			_packages.Add(_projectAssetSetting.defaultPackageName, package);

#if UNITY_EDITOR
			var initializationOperation = GetInitOperation(editorPlayMode, package);
#else
			var initializationOperation = GetInitOperation(playMode, package);
#endif

			yield return initializationOperation;

			if (initializationOperation != null && package.InitializeStatus != EOperationStatus.Succeed) {
				QLog.Error($"QuickGameFramework>Asset>初始资源包<{defaultPackageName}>以<{playMode}模式>加载失败!\n" +
				           $"{initializationOperation.Error}");
			} else {
				QLog.Log($"QuickGameFramework>Asset>初始资源包<{defaultPackageName}>以<{playMode}模式>加载成功!");
			}

			callBack?.Invoke();
		}

		private InitializationOperation GetInitOperation(EPlayMode playMode, ResourcePackage package) {
			InitializationOperation initializationOperation;
			switch (playMode) {
				// 编辑器下的模拟模式
				case EPlayMode.EditorSimulateMode: {
					var createParameters = new EditorSimulateModeParameters {
						SimulateManifestFilePath =
							EditorSimulateModeHelper.SimulateBuild(EDefaultBuildPipeline.ScriptableBuildPipeline,
								package.PackageName)
					};
					initializationOperation = package.InitializeAsync(createParameters);
					return initializationOperation;
				}

				// 单机运行模式
				case EPlayMode.OfflinePlayMode: {
					var createParameters = new OfflinePlayModeParameters();
					// createParameters.DecryptionServices 可提供资源包加密类
					initializationOperation = package.InitializeAsync(createParameters);
					return initializationOperation;
				}

				// 联机运行模式
				case EPlayMode.HostPlayMode: {
					var createParameters = new HostPlayModeParameters {
						// DecryptionServices = 可提供资源包加密类
						// QueryServices = new GameQueryServices(); 内置文件查询服务类
						// DefaultHostServer = GetHostServerURL(true),
						// FallbackHostServer = GetHostServerURL(false)
					};
					initializationOperation = package.InitializeAsync(createParameters);
					return initializationOperation;
				}
				default:
					throw new ArgumentOutOfRangeException(nameof(playMode), playMode, null);
			}
		}
		
		private string GetHostServerURL(bool isBackup) {
			var hostServerIP = isBackup? _projectAssetSetting.backupHostServerIP : _projectAssetSetting.hostServerIP;
			var gameVersion = _projectAssetSetting.gameVersion;
#if UNITY_EDITOR
			return UnityEditor.EditorUserBuildSettings.activeBuildTarget switch {
				UnityEditor.BuildTarget.Android => $"{hostServerIP}/CDN/Android/{gameVersion}",
				UnityEditor.BuildTarget.iOS => $"{hostServerIP}/CDN/IPhone/{gameVersion}",
				UnityEditor.BuildTarget.WebGL => $"{hostServerIP}/CDN/WebGL/{gameVersion}",
				_ => $"{hostServerIP}/CDN/PC/{gameVersion}"
			};
#else
			return Application.platform switch {
				RuntimePlatform.Android => $"{hostServerIP}/CDN/Android/{gameVersion}",
				RuntimePlatform.IPhonePlayer => $"{hostServerIP}/CDN/IPhone/{gameVersion}",
				RuntimePlatform.WebGLPlayer => $"{hostServerIP}/CDN/WebGL/{gameVersion}",
				_ => $"{hostServerIP}/CDN/PC/{gameVersion}"
			};
#endif
		}
	}
}