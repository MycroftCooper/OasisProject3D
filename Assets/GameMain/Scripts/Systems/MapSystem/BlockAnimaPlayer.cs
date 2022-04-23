using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using MycroftToolkit.QuickCode;
using DG.Tweening;

namespace OasisProject3D.MapSystem {
    public class BlockAnimaPlayer : MonoSingleton<BlockAnimaPlayer> {
        public float PlayTime_Selected = 0.5f;
        public float PlayDistance_Selected = 1f;
        public float PlayTime_TypeChange = 1f;

        public delegate void AnimaCallback();

        public void OnSelected(BlockCtrl block, AnimaCallback callback) {
            block.transform.DOMoveY(block.WorldPos.y + PlayDistance_Selected, PlayTime_Selected).OnComplete(() => { callback?.Invoke(); });
        }
        public void OffSelected(BlockCtrl block, AnimaCallback callback) {
            block.transform.DOMoveY(block.WorldPos.y, PlayTime_Selected).OnComplete(() => { callback?.Invoke(); });
        }
        public void OnTypeChange(BlockCtrl block, AnimaCallback callback) {
            bool isCall = false;
            block.transform.DORotate(Vector3.right * 360, PlayTime_TypeChange, RotateMode.FastBeyond360).OnUpdate(() => {
                if (block.transform.rotation.eulerAngles.x > 180 && !isCall) {
                    callback?.Invoke();
                    isCall = true;
                }
            });
        }
    }
}
