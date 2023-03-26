using System;
using MycroftToolkit.QuickCode;
using UnityEngine;

namespace OasisProject3D.BuildingSystem {
    public enum BuildingCmd { Build, Move, Open, Close, Upgrade, Demolish }
    
    public class BuildingCtrl : MonoBehaviour, IEnumCmdReceiver<BuildingCmd> {
        public BuildingData Data { protected set; get; }

        public void ExecuteCmd(BuildingCmd cmd) {
            switch (cmd) {
                case BuildingCmd.Build:
                    BuildCmdHandler();
                    break;
                case BuildingCmd.Move:
                    OnMoveCmdCall();
                    break;
                case BuildingCmd.Open:
                    OnOpenCmdCall();
                    break;
                case BuildingCmd.Close:
                    OnCloseCmdCall();
                    break;
                case BuildingCmd.Upgrade:
                    OnUpgradeCmdCall();
                    break;
                case BuildingCmd.Demolish:
                    OnDemolishCmdCall();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cmd), cmd, null);
            }
        }
        
        #region 初始化建筑相关
        public bool IsInitialised { get; private set; }

        public void Initialize() {
            IsInitialised = true;
        }
        #endregion
        
        #region 开关建筑相关
        public bool IsWorking { get; private set; }
        public Action OnOpenCmdCall;
        public Action OnCloseCmdCall;
        public Action<BuildingCtrl> OnOpen;
        public Action<BuildingCtrl> OnClose;
        private void OnOpenEnterHandler() {
            IsWorking = true;
            OnOpen?.Invoke(this);
        }
        
        public virtual void OnOpenUpdateHandler() {
            if (!IsWorking) {
                return;
            }
            
        }

        public virtual void Close() {
            IsWorking = false;
            OnClose?.Invoke(this);
        }
        #endregion
        
        #region 移动建筑相关
        public bool IsMoving { get; private set; }
        public Action OnMoveCmdCall;
        public Action<BuildingCtrl> OnMoveStart;
        public Action<BuildingCtrl> OnMoveEnd;
        private BuildingMoveHelper _buildingMoveHelper;

        public virtual void OnMoveEnterHandler() {
            IsMoving = true;
            OnMoveStart?.Invoke(this);
            _buildingMoveHelper = new BuildingMoveHelper(this);
        }
        
        public virtual void OnMoveUpdateHandler() {
            if (!IsMoving) {
                return;
            }
        }

        public virtual void OnMoveExitHandler() {
            if (IsMoving) {
                return;
            }
            _buildingMoveHelper = null;
            OnMoveEnd?.Invoke(this);
        }

        public bool CanSetBuildingHere(Vector3 targetPos, Vector3 targetRotation) {
            
            return true;
        }
        #endregion
        
        #region 建造建筑相关
        public bool IsBuilding { get; private set; }
        public bool IsBuilt { get; private set; }
        public Action<BuildingCtrl> OnBuildStart;
        public Action<BuildingCtrl> OnBuildEnd;

        private void BuildCmdHandler() {
            if (IsBuilt) {
                return;
            }
            
        }
        
        public virtual void OnBuildEnterHandler() {
            IsBuilding = true;
            OnBuildStart?.Invoke(this);
        }
        
        public virtual void OnBuildUpdateHandler() {

        }
        
        public virtual void OnBuildExitHandler() {
            IsBuilding = false;
            IsBuilt = true;
            OnBuildEnd?.Invoke(this);
        }
        #endregion
        
        #region 升级建筑相关
        public bool IsUpgrading { get; private set; }
        public Action OnUpgradeCmdCall;
        public Action<BuildingCtrl> OnUpgradeStart;
        public Action<BuildingCtrl> OnUpgradeEnd;

        public virtual void OnUpgradeEnterHandler() {
            IsUpgrading = true;
            OnMoveStart?.Invoke(this);
        }
        
        public virtual void OnUpgradeUpdateHandler() {

        }
        
        public virtual void OnUpgradeExitHandler() {
            IsUpgrading = false;
            OnUpgradeEnd?.Invoke(this);
        }
        #endregion

        #region 拆除建筑相关
        public bool IsDemolished { get; private set; }
        public Action OnDemolishCmdCall;
        public Action<BuildingCtrl> OnDemolishStart;
        public Action<BuildingCtrl> OnDemolishEnd;
        public virtual void Demolish() {
            OnDemolishStart?.Invoke(this);
            
            IsDemolished = true;
            OnDemolishEnd?.Invoke(this);
        }
        #endregion
    }
}
