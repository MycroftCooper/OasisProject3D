using OasisProject3D.MapSystem;
using QuickGameFramework.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OasisProject3D.BuildingSystem {
    public class BuildingMoveHelper {
        private readonly Transform _targetTransform;
        private BuildingCtrl _targetBuilding;

        private InputActionMap _buildingInputActionMap;

        private readonly Vector3 _rotateAngle = new (0, 60, 0);

        public BuildingMoveHelper(BuildingCtrl targetBuilding) {
            _targetBuilding = targetBuilding;
            _targetTransform = _targetBuilding.transform;

            BindPlayerInput();
        }

        private void BindPlayerInput() {
            PlayerInput playerInput = GameEntry.InputMgr.playerInput;
            _buildingInputActionMap = playerInput.actions.FindActionMap("BuildingMoveCtrl");
            _buildingInputActionMap.Enable();
            
            _buildingInputActionMap.FindAction("Move").performed += OnBuildingMoveHandler;
            _buildingInputActionMap.FindAction("TurnLeft").performed += OnBuildingTurnLeftHandler;
            _buildingInputActionMap.FindAction("TurnRight").performed += OnBuildingTurnRightHandler;
            _buildingInputActionMap.FindAction("Confirm").performed += OnMoveConfirmHandler;
            _buildingInputActionMap.FindAction("Cancel").performed += OnMoveCancelHandler;
        }
        
        private void UnbindPlayerInput() {
            _buildingInputActionMap.FindAction("Move").performed -= OnBuildingMoveHandler;
            _buildingInputActionMap.FindAction("TurnLeft").performed -= OnBuildingTurnLeftHandler;
            _buildingInputActionMap.FindAction("TurnRight").performed -= OnBuildingTurnRightHandler;
            _buildingInputActionMap.FindAction("Confirm").performed -= OnMoveConfirmHandler;
            _buildingInputActionMap.FindAction("Cancel").performed -= OnMoveCancelHandler;
            _buildingInputActionMap.Disable();
        }
        
        private void OnBuildingMoveHandler(InputAction.CallbackContext obj) {
            Camera camera = Camera.main;
            if (camera == null) {
                QLog.Error("BuildingMoveHelper>Error>主摄像机为空！");
                return;
            }
            
            Vector3 mousePosition = obj.ReadValue<Vector2>();
            Ray mouseRay = camera.ScreenPointToRay(mousePosition);
            Plane xzPlane = new Plane(Vector3.up, Vector3.zero);
            if (!xzPlane.Raycast(mouseRay, out var enter)) {
                return;
            }
            Vector3 worldPoint = mouseRay.GetPoint(enter);
            Vector2Int logicPos = GameEntry.ModuleMgr.GetModule<MapManager>().WorldPos2LogicPos(worldPoint);
            var targetBlock = GameEntry.ModuleMgr.GetModule<MapManager>().GetBlock(worldPoint);
            if (targetBlock == null) {
                QLog.Error($"BuildingMoveHelper>Error>目标位置{worldPoint}={logicPos}没有地块！");
                return;
            }
            _targetTransform.position = targetBlock.BuildingPos;
        }

        private void OnBuildingTurnLeftHandler(InputAction.CallbackContext obj) {
            var oldRotation = _targetTransform.localRotation.eulerAngles;
            _targetTransform.localRotation = Quaternion.Euler(oldRotation + _rotateAngle);
        }
        
        private void OnBuildingTurnRightHandler(InputAction.CallbackContext obj) {
            var oldRotation = _targetTransform.localRotation.eulerAngles;
            _targetTransform.localRotation = Quaternion.Euler(oldRotation - _rotateAngle);
        }

        private void OnMoveConfirmHandler(InputAction.CallbackContext obj) {

            UnbindPlayerInput();
        }

        private void OnMoveCancelHandler(InputAction.CallbackContext obj) {
            
            UnbindPlayerInput();
        }

    }
}