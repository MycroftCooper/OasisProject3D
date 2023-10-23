using System;
using System.Linq;
using FairyGUI;
using OasisProject3D.BuildingSystem;
using QuickGameFramework.Runtime;
using QuickGameFramework.Runtime.UI;

namespace OasisProject3D.UI.GameMainUIPackage {
    internal class BuildingOrBlockDescTabView : View<BuildingOrBlockTabUIData> {
        private BuildingOrBlockDescTab _tab;
        private BuildingCtrl _bindingBuilding;
        
        protected override void Start() {
            base.Start();
            _tab = (BuildingOrBlockDescTab)UIPanel.ui;
        }

        protected void Update() {
            if (_bindingBuilding == null) {
                return;
            }

            if (_tab.CostInfoBox.enabled && _tab.CostInfoBox.BuildInfoCtrl.selectedPage.Contains("Progress")) {
                RefreshBuildingProgress(_bindingBuilding);
            }
            
        }

        protected override void OnShow(BuildingOrBlockTabUIData data) {
            _tab.IsOpen.selectedPage = "Open";
            OnRefresh(data);
        }

        protected override void OnRefresh(BuildingOrBlockTabUIData data) {
            if (data.IsBlock) {
                
                return;
            }

            BindBuilding(data.BuildingData.Ctrl);
            RefreshBuildingBasicInfo(data.BuildingData);
            if (_tab.CostInfoBox.displayObject.stage != null ) {
                if (_tab.CostInfoBox.BuildInfoCtrl.selectedPage.Contains("Cost")) {
                    RefreshBuildingCostInfo(data.BuildingData);
                } else {
                    RefreshBuildingProgress(data.BuildingData.Ctrl);
                }
            }
            
            
        }

        protected override void OnHide(BuildingOrBlockTabUIData data) {
            _tab.IsOpen.selectedPage = "Hide";
            UnbindBuilding();
        }

        protected override void ProcessMessage(ValueType command, BuildingOrBlockTabUIData data) {
            BuildingOrBlockTabUICmd cmd = (BuildingOrBlockTabUICmd) command;
            switch (cmd) {
                case BuildingOrBlockTabUICmd.ChangeBuilding:
                case BuildingOrBlockTabUICmd.UpgradeBuilding:
                case BuildingOrBlockTabUICmd.RebuildBuilding:
                case BuildingOrBlockTabUICmd.MoveBuilding:
                case BuildingOrBlockTabUICmd.SwitchBuildingOpenOrClose:
                    OnRefresh(data);
                    break;
                case BuildingOrBlockTabUICmd.DeleteBuilding:
                    OnHide(data);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override bool OnBackButtonClicked() {
            throw new NotImplementedException();
        }

        private void RefreshBuildingBasicInfo(BuildingDescTabUIData data) {
            BuildingBasicInfo basicInfoBox = _tab.BasicInfoBox;
            basicInfoBox.Name.text = data.BuildingName;
            basicInfoBox.Desc.text = data.BuildingDesc;

            _tab.IconCase.IconHexLoader.BuildingIcon.image.texture =  new NTexture(data.BuildingIcon);
            var stateCtrl = _tab.BuildingStateCtrl;
            if (data.BuildingState == default) {
                stateCtrl.SetSelectedPage("ToBuild");
                return;
            }

            string buildingStateStr = data.Ctrl.FsmCtrl.GetCurrentState().name;
            for (int i = 1;i<stateCtrl.pageCount;i++) {
                string pageName = stateCtrl.GetPageName(i);
                if (pageName != buildingStateStr) continue;
                stateCtrl.SetSelectedIndex(i);
                return;
            }
            
            QLog.Error($"UI中未适配此建筑状态:{buildingStateStr}");
        }

        private void RefreshBuildingCostInfo(BuildingDescTabUIData data) {
            GList viewResList = _tab.CostInfoBox.ResList;
            viewResList.RemoveChildrenToPool();
            foreach (var (icon, progress) in data.CostIconDict) {
                ResIconProgressBar resIconBar = (ResIconProgressBar)viewResList.AddItemFromPool();
                resIconBar.ResIcon.texture = new NTexture(icon);
                resIconBar.asProgress.value = progress.x;
                resIconBar.asProgress.max = progress.y;
            }
        }

        private void RefreshBuildingProgress(BuildingCtrl ctrl) {
            string buildingStateStr = ctrl.FsmCtrl.GetCurrentState().name;
            if (buildingStateStr is not ("Building" or "Upgrade" or "Rebuild")) {
                DispatchMessage(new Message {
                    Command = Message.CommonCommand.Show,
                    ExtraParams = new BuildingDescTabUIData { Ctrl = ctrl }   
                });
                return;
            }
            
            string currentPage = _tab.CostInfoBox.BuildInfoCtrl.selectedPage;
            
            if (currentPage.Contains("Upgrade")) {
                
            } else {
                _tab.CostInfoBox.BuildProgressBar.value = ctrl.ConstructProgress * 100;
                _tab.CostInfoBox.BuildProgressBar.LeftTime.text = ((int)ctrl.LeftConstructScs).ToString();
            }
        }

        #region 建筑回调相关

        private void BindBuilding(BuildingCtrl ctrl) {
            if (ctrl == _bindingBuilding) {
                return;
            }
            if (_bindingBuilding != null) {
                UnbindBuilding();
            }
            if (ctrl == null) {
                return;
            }

            _bindingBuilding = ctrl;
        }

        private void UnbindBuilding() {
            if (_bindingBuilding == null) {
                return;
            }
            
            _bindingBuilding = null;
        }
        


        #endregion
    }
}