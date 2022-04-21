using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using MycroftToolkit.QuickCode;
using DG.Tweening;

namespace OasisProject3D.MapSystem {
    public static class BlockAnimaPlayer {
        public delegate void AnimaCallback();

        public static void OnSelected(GameObject block) {

        }
        public static void OffSelected(GameObject block) {

        }
        public static void OnTypeChange(GameObject block, AnimaCallback callback) {
            block.transform.DORotate(Vector3.right * 180, 0.5f, RotateMode.WorldAxisAdd).OnComplete(() => {
                callback();
                block.transform.DORotate(Vector3.right * 180, 0.5f, RotateMode.WorldAxisAdd);
            });
        }
    }
}
