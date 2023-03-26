using OasisProject3D.CameraCtrl;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    public PlayerInput playerInput;
    public MainCameraCtrl mainCameraCtrl;
    
    void Start() {
        playerInput = GetComponent<PlayerInput>();
    }
}
