using FairyGUI;
using OasisProject3D.BlockSystem;
using OasisProject3D.BuildingSystem;
using QuickGameFramework.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using Controller = QuickGameFramework.Runtime.UI.Controller;

namespace OasisProject3D.UI.GameMainUIPackage {
    public class BuildingDescTabCtrl : Controller {
        private BuildingDescTab _tab;

        private void Start() {
            _tab = (BuildingDescTab)UIPanel.ui;
            
            var mainGameCtrlMap = GameEntry.InputMgr.playerInput.actions.FindActionMap("MainGameCtrl");
            mainGameCtrlMap.FindAction("Click").performed += OnPlayClickedInGamePlay;
        }
        
        public void OnPlayClickedInGamePlay(InputAction.CallbackContext obj) {
            if (Stage.isTouchOnUI) {
                Debug.Log("0");
                return;
            }
            
            Camera mainCamera = Camera.main;
            if (mainCamera == null) {
                QLog.Error("BuildingMoveHelper>Error>主摄像机为空！");
                return;
            }
            
            Vector3 mousePosition = obj.ReadValue<Vector2>();
            Ray mouseRay = mainCamera.ScreenPointToRay(mousePosition);
            var layerMask = LayerMask.GetMask("Block", "Building");
            if (!Physics.Raycast(mouseRay, out var hit, 5000f, layerMask, QueryTriggerInteraction.Collide)) {
                return;
            }
            
            int targetLayer = hit.transform.gameObject.layer;
            if (targetLayer == LayerMask.NameToLayer("Block")) {
                var targetBlock = hit.transform.GetComponent<BlockCtrl>();
            Debug.Log("1");
            }
        
            if (targetLayer == LayerMask.NameToLayer("Building")) {
                var targetBuilding = hit.transform.GetComponent<BuildingCtrl>();
                Debug.Log("2");
            }
        }
    }
}
