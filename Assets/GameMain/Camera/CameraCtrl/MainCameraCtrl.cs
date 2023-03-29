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
        [BoxGroup("水平移动")]
        [SerializeField]
        private float maxSpeed = 5f;
        private float _speed;
        [BoxGroup("水平移动")]
        [SerializeField]
        private float acceleration = 10f;
        [BoxGroup("水平移动")]
        [SerializeField]
        private float damping = 15f;

        [BoxGroup("垂直移动")]
        [SerializeField]
        private float stepSize = 2f;
        [BoxGroup("垂直移动")]
        [SerializeField]
        private float zoomDampening = 7.5f;
        [BoxGroup("垂直移动")]
        [SerializeField]
        private float minHeight = 5f;
        [BoxGroup("垂直移动")]
        [SerializeField]
        private float maxHeight = 50f;
        [BoxGroup("垂直移动")]
        [SerializeField]
        private float zoomSpeed = 2f;

        [BoxGroup("边缘移动")]
        [SerializeField]
        [Range(0f, 0.1f)]
        private float edgeTolerance = 0.05f;

        [BoxGroup("旋转")]
        [SerializeField]
        private float maxRotationSpeed = 1f;
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
            _zoomHeight = _cameraTransform.localPosition.y;
            _cameraTransform.LookAt(transform);
            _lastPosition = transform.position;
            
            _rotateAction.performed += RotateCamera;
            _zoomAction.performed += ZoomCamera;
            _cameraInputActionMap.Enable();
        }

        private void OnDisable() {
            _rotateAction.performed -= RotateCamera;
            _zoomAction.performed -= ZoomCamera;
            _cameraInputActionMap.Disable();
        }

        private void Update() {
            //inputs
            UpdateKeyboardMoveAction();
            UpdateMouseAtScreenEdge();
            UpdateMouseMoveAction();

            //move base and camera objects
            UpdateVelocity();
            UpdateMovement();
            UpdateZoom();
            
            
            Ray mouseRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Debug.DrawRay(mouseRay.origin, mouseRay.direction * 1000);
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

        private void UpdateMouseMoveAction() {
            if (!Mouse.current.middleButton.isPressed)
                return;

            //create plane to raycast to
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            if (Camera.main == null) {
                QLog.Error("InputSystem>CameraCtrl>主相机缺失！");
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!plane.Raycast(ray, out float distance)) return;
            if (Mouse.current.middleButton.wasPressedThisFrame)
                _startDrag = ray.GetPoint(distance);
            else
                _targetPosition += _startDrag - ray.GetPoint(distance);
        }

        private void UpdateMouseAtScreenEdge() {
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
        private void RotateCamera(InputAction.CallbackContext obj) {
            if (Mouse.current.rightButton.isPressed || Keyboard.current.qKey.isPressed || Keyboard.current.eKey.isPressed) {
                float inputValue = obj.ReadValue<Vector2>().x;
                transform.rotation = Quaternion.Euler(0f, inputValue * maxRotationSpeed + transform.rotation.eulerAngles.y, 0f);
            } else if (Keyboard.current.qKey.wasPressedThisFrame || Keyboard.current.eKey.isPressed) {

            }
        }
        #endregion

        #region 缩放相关
        private float _zoomHeight;
        private void UpdateZoom() {
            //set zoom target
            var localPosition = _cameraTransform.localPosition;
            Vector3 zoomTarget = new Vector3(localPosition.x, _zoomHeight, localPosition.z);
            //add vector for forward/backward zoom
            zoomTarget -= zoomSpeed * (_zoomHeight - localPosition.y) * Vector3.forward;

            localPosition = Vector3.Lerp(localPosition, zoomTarget, Time.deltaTime * zoomDampening);
            _cameraTransform.localPosition = localPosition;
            _cameraTransform.LookAt(this.transform);
        }

        private void ZoomCamera(InputAction.CallbackContext obj) {
            float inputValue = -obj.ReadValue<Vector2>().y / 100f;

            if (!(Mathf.Abs(inputValue) > 0.1f)) return;
            _zoomHeight = _cameraTransform.localPosition.y + inputValue * stepSize;

            if (_zoomHeight < minHeight)
                _zoomHeight = minHeight;
            else if (_zoomHeight > maxHeight)
                _zoomHeight = maxHeight;
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
