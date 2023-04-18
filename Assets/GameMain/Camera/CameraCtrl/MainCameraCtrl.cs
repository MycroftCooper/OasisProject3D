using MycroftToolkit.DiscreteGridToolkit;
using QuickGameFramework.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

namespace OasisProject3D.CameraCtrl {
    public class MainCameraCtrl : MonoBehaviour {
        private InputActionMap _cameraInputActionMap;
        private InputAction _moveAction;
        private InputAction _rotateAction;
        private InputAction _zoomAction;
        
        private Transform _cameraTransform;

        #region 面板设置相关
        [BoxGroup("平移")]
        [SerializeField]
        private float maxSpeed = 20f;
        private float _speed;
        [BoxGroup("平移")]
        [SerializeField]
        private float acceleration = 10f;
        [BoxGroup("平移")]
        [SerializeField]
        private float damping = 15f;
        
        [BoxGroup("边缘移动")]
        [SerializeField]
        private bool useEdgeMove;
        [BoxGroup("边缘移动")]
        [SerializeField]
        [Range(0f, 0.1f)]
        private float edgeTolerance = 0.05f;
        
        [BoxGroup("旋转")]
        [SerializeField]
        private Vector2 rotationSpeed = new (10f,5f);
        [BoxGroup("旋转")]
        [SerializeField] 
        private Vector2 verticalAngleLimit = new (-45, 40f);
        
        [BoxGroup("缩放")]
        [SerializeField]
        private float zoomSpeed = 40f;
        [BoxGroup("缩放")]
        [SerializeField]
        private float minHeight = 15f;
        [BoxGroup("缩放")]
        [SerializeField]
        private float maxHeight = 100f;
        [BoxGroup("缩放")]
        [SerializeField]
        private float useRayHeight = 30f;
        #endregion
        
        private void Awake() {
            GameEntry.InputMgr.mainCameraCtrl = this;
            _cameraInputActionMap = GameEntry.InputMgr.playerInput.actions.FindActionMap("CameraCtrl");
            _moveAction = _cameraInputActionMap.FindAction("Move");
            _rotateAction = _cameraInputActionMap.FindAction("Rotate");
            _zoomAction = _cameraInputActionMap.FindAction("Zoom");
            _cameraTransform = GetComponentInChildren<Camera>().transform;
        }

        private void OnEnable() {
            _cameraTransform.LookAt(transform);
            _lastPosition = transform.position;
            _cameraInputActionMap.Enable();
        }

        private void OnDisable() {
            _cameraInputActionMap.Disable();
        }

        private void Update() {
            //inputs
            UpdateKeyboardMoveAction();
            UpdateMouseAtScreenEdge();
            // UpdateMouseMoveAction();

            //move base and camera objects
            UpdateVelocity();
            UpdateMovement();

            RotateCamera(_rotateAction.ReadValue<Vector2>());
            ZoomCamera(_zoomAction.ReadValue<Vector2>());
        }
        
        private Vector3 _horizontalVelocity;
        private Vector3 _lastPosition;
        private void UpdateVelocity() {
            var position = transform.position;
            _horizontalVelocity = (position - _lastPosition) / Time.deltaTime;
            _horizontalVelocity.y = 0f;
            _lastPosition = position;
        }

        #region 平移相关
        private Vector3 _targetPosition;
        private Vector3 _startDrag;
        private void UpdateKeyboardMoveAction() {
            Vector3 inputValue = _moveAction.ReadValue<Vector2>().x * GetCameraRight()
                        + _moveAction.ReadValue<Vector2>().y * GetCameraForward();

            inputValue = inputValue.normalized;

            if (inputValue.sqrMagnitude > 0.1f)
                _targetPosition += inputValue;
        }

        private void UpdateMouseAtScreenEdge() {
            if (!useEdgeMove) {
                return;
            }
            //mouse position is in pixels
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 moveDirection = Vector3.zero;

            //horizontal scrolling
            if (mousePosition.x < edgeTolerance * Screen.width)
                moveDirection += -GetCameraRight();
            else if (mousePosition.x > (1f - edgeTolerance) * Screen.width)
                moveDirection += GetCameraRight();

            //vertical scrolling
            if (mousePosition.y < edgeTolerance * Screen.height)
                moveDirection += -GetCameraForward();
            else if (mousePosition.y > (1f - edgeTolerance) * Screen.height)
                moveDirection += GetCameraForward();

            _targetPosition += moveDirection;
        }

        private void UpdateMovement() {
            if (_targetPosition.sqrMagnitude > 0.1f) {
                //create a ramp up or acceleration
                _speed = Mathf.Lerp(_speed, maxSpeed, Time.deltaTime * acceleration);
                transform.position += _targetPosition * (_speed * Time.deltaTime);
            } else {
                //create smooth slow down
                _horizontalVelocity = Vector3.Lerp(_horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
                transform.position += _horizontalVelocity * Time.deltaTime;
            }

            //reset for next frame
            _targetPosition = Vector3.zero;
        }
        
        //gets the horizontal forward vector of the camera
        private Vector3 GetCameraForward() {
            Vector3 forward = _cameraTransform.forward;
            forward.y = 0f;
            return forward;
        }

        //gets the horizontal right vector of the camera
        private Vector3 GetCameraRight() {
            Vector3 right = _cameraTransform.right;
            right.y = 0f;
            return right;
        }
        #endregion

        #region 旋转相关
        private void RotateCamera(Vector2 inputValue) {
            var trans = transform;
            // 水平旋转
            float horizontalAngleY = inputValue.x * rotationSpeed.x * Time.deltaTime;
            // 在世界空间中执行水平旋转
            trans.Rotate(Vector3.up, horizontalAngleY, Space.World);
            
            // 垂直旋转
            float verticalAngleX = -inputValue.y * rotationSpeed.y * Time.deltaTime;
            // 在相机的局部空间中执行垂直旋转
            trans.Rotate(Vector3.right, verticalAngleX, Space.Self);

            // 修复后的相机角度限制
            var rotation = trans.rotation.eulerAngles;
            float finalX = rotation.x > 180f ? rotation.x - 360f : rotation.x;
            finalX = Mathf.Clamp(finalX, verticalAngleLimit.x, verticalAngleLimit.y);
            // 重新计算旋转四元数
            transform.rotation = Quaternion.Euler(finalX, rotation.y, rotation.z);
        }
        #endregion

        #region 缩放相关
        private void ZoomCamera(Vector2 inputValue) {
            if (inputValue.y == 0) {
                return;
            }
            
            // 获取鼠标滚轮值
            float scrollValue = -inputValue.y;
            
            var trans = transform;
            var pos = trans.position;
            Vector3 direction = Vector3.up * scrollValue;
            // 计算新的相机位置
            Vector3 newPosition = pos + direction * (zoomSpeed * Time.deltaTime);

            if (newPosition.y >= maxHeight) {
                return;
            }

            if (newPosition.y < useRayHeight) {
                Ray cameraRay = new Ray(_cameraTransform.position, direction);
                var layerMask = LayerMask.GetMask("Block");
                if (Physics.Raycast(cameraRay, out var hit, maxHeight, layerMask, QueryTriggerInteraction.Collide) &&
                    hit.distance < minHeight && scrollValue > 0) {
                    return;
                }
            }

            if (newPosition.y < 5f) {
                return;
            }
            
            trans.position = newPosition;
        }
        #endregion

        #region API

        public void LockViewOn(Transform target) {
            
        }

        public void UnlockView() {
            
        }
        #endregion
    }
}
