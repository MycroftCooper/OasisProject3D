using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using MycroftToolkit.QuickCode;
using DG.Tweening;

namespace OasisProject3D.MapSystem {
    public class BlockAnimaPlayer : MonoSingleton<BlockAnimaPlayer> {
        public float playTime_Selected = 0.5f;
        public float playDistance_Selected = 1f;
        public float playTime_TypeChange = 1f;

        public delegate void AnimaCallback();

        public void OnSelected(BlockCtrl block, AnimaCallback callback) {
            block.transform.DOMoveY(block.worldPos.y + playDistance_Selected, playTime_Selected).OnComplete(() => { callback?.Invoke(); });
        }
        public void OffSelected(BlockCtrl block, AnimaCallback callback) {
            block.transform.DOMoveY(block.worldPos.y, playTime_Selected).OnComplete(() => { callback?.Invoke(); });
        }
        public void OnTypeChange(BlockCtrl block, AnimaCallback callback) {
            bool isCall = false;
            block.transform.DORotate(Vector3.right * 360, playTime_TypeChange, RotateMode.FastBeyond360).OnUpdate(() => {
                if (block.transform.rotation.eulerAngles.x > 180 && !isCall) {
                    callback?.Invoke();
                    isCall = true;
                }
            });
        }
    }
}
