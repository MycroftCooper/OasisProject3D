using UnityEngine;
using DG.Tweening;
using QuickGameFramework.Runtime;

namespace OasisProject3D.MapSystem {
    public class BlockAnimaPlayer : MonoSingleton<BlockAnimaPlayer> {
        public float playTimeSelected = 0.5f;
        public float playDistanceSelected = 1f;
        public float playTimeTypeChange = 1f;

        public delegate void AnimaCallback();

        public void OnSelected(BlockCtrl block, AnimaCallback callback) {
            block.transform.DOMoveY(block.worldPos.y + playDistanceSelected, playTimeSelected).OnComplete(() => { callback?.Invoke(); });
        }
        public void OffSelected(BlockCtrl block, AnimaCallback callback) {
            block.transform.DOMoveY(block.worldPos.y, playTimeSelected).OnComplete(() => { callback?.Invoke(); });
        }
        public void OnTypeChange(BlockCtrl block, AnimaCallback callback) {
            bool isCall = false;
            block.transform.DORotate(Vector3.right * 360, playTimeTypeChange, RotateMode.FastBeyond360).OnUpdate(() => {
                if (!(block.transform.rotation.eulerAngles.x > 180) || isCall) return;
                callback?.Invoke();
                isCall = true;
            });
        }
    }
}
