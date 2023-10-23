using System;
using System.Collections.Generic;
using System.Linq;
using YooAsset;

namespace QuickGameFramework.Runtime {
    public class AssetLoadProgress {
        public float Progress {
            get {
                if (Handles == null || Handles.Count == 0) {
                    return 0;
                }

                var totalProgress = Handles.Sum(assetOperationHandle => assetOperationHandle.Progress) + 
                                    SubHandles.Sum(assetOperationHandle => assetOperationHandle.Progress);
                return totalProgress / (Handles.Count + SubHandles.Count);
            }
        }
        public bool IsComplete => _completeHandleNum == Handles.Count;
        public Action Completed;
        
        public List<AssetHandle> Handles { get; }
        public List<SubAssetsHandle> SubHandles { get; }
        private int _completeHandleNum;

        public AssetLoadProgress() {
            Handles = new List<AssetHandle>();
            SubHandles = new List<SubAssetsHandle>();
            _completeHandleNum = 0;
        }

        public static AssetLoadProgress operator +(AssetLoadProgress a, AssetLoadProgress b) {
            if (b.Handles.Count != 0) {
                a.AddHandles(b.Handles);
            }
            if (b.SubHandles.Count != 0) {
                a.AddSubHandles(b.SubHandles);
            }
            
            b.Destroy();
            return a;
        }
        
        public static AssetLoadProgress operator +(AssetLoadProgress a, AssetHandle b) {
            a.AddHandle(b);
            return a;
        }
        
        public static AssetLoadProgress operator +(AssetLoadProgress a, AssetHandle[] b) {
            foreach (var handle in b) {
                a.AddHandle(handle);
            }
            return a;
        }
        
        public static AssetLoadProgress operator +(AssetLoadProgress a, SubAssetsHandle b) {
            a.AddSubHandle(b);
            return a;
        }
        
        public static AssetLoadProgress operator +(AssetLoadProgress a, SubAssetsHandle[] b) {
            foreach (var subHandle in b) {
                a.AddSubHandle(subHandle);
            }
            return a;
        }

        public void AddHandles(List<AssetHandle> handles) {
            if (handles == null || handles.Count == 0) {
                QLog.Error("QuickGameFramework>Asset> handle为空，加入加载队列失败！");
                return;
            }
            handles.ForEach(AddHandle);
        }

        public void AddHandle(AssetHandle handle) {
            if (handle == null) {
                QLog.Error("QuickGameFramework>Asset> handle为空，加入加载队列失败！");
                return;
            }
            if (Handles.Contains(handle)) {
                QLog.Error("QuickGameFramework>Asset> 该handle已存在，加入加载队列失败！");
                return;
            }
            Handles.Add(handle);
            handle.Completed += OnHandleComplete;
        }
        
        public void AddSubHandle(SubAssetsHandle handle) {
            if (handle == null) {
                QLog.Error("QuickGameFramework>Asset> handle为空，加入加载队列失败！");
                return;
            }
            if (SubHandles.Contains(handle)) {
                QLog.Error("QuickGameFramework>Asset> 该handle已存在，加入加载队列失败！");
                return;
            }
            SubHandles.Add(handle);
            handle.Completed += OnHandleComplete;
        }

        public void AddSubHandles(List<SubAssetsHandle> handles) {
            if (handles == null || handles.Count == 0) {
                QLog.Error("QuickGameFramework>Asset> handle为空，加入加载队列失败！");
                return;
            }
            handles.ForEach(AddSubHandle);
        }

        public void OnHandleComplete(HandleBase handle) {
            _completeHandleNum += 1;
            if (IsComplete) {
                Completed?.Invoke();
            }
        }

        public void Destroy() {
            Handles.ForEach(h=>h.Completed -= OnHandleComplete);
            Handles.Clear();
            Completed = null;
        }
    }
}