using System;
using cfg;
using FairyGUI;
using OasisProject3D.BuildingSystem;
using OasisProject3D.MapSystem;
using QuickGameFramework.Runtime;
using QuickGameFramework.Runtime.UI;
using UnityEngine;
using Controller = QuickGameFramework.Runtime.UI.Controller;

namespace OasisProject3D.UI.GameMainUIPackage {
    public class MainPageCtrl: Controller  {
        private MainPage _mainPage;
        private void Start() {
            _mainPage = (MainPage)UIPanel.ui;
            
            _mainPage.BuildBtn.onClick.Add(OnBuildBtnClicked);
            var buildingList = _mainPage.BuildingList;
            _isBuildingListOpen = false;
            _buildingCaseList = buildingList.BuildingCaseList;
            buildingList.BuildingTabAnyBtn.onClick.Add(OnBuildingTypeBtnClicked);
            buildingList.BuildingTabFunctionBtn.onClick.Add(OnBuildingTypeBtnClicked);
            buildingList.BuildingTabEcoBtn.onClick.Add(OnBuildingTypeBtnClicked);
            buildingList.BuildingTabProductBtn.onClick.Add(OnBuildingTypeBtnClicked);
            buildingList.BuildingTabStorageBtn.onClick.Add(OnBuildingTypeBtnClicked);
            buildingList.BuildingCaseList.onClickItem.Add(OnBuildingBtnClicked);

            _mainPage.Speed1XBtn.onClick.Add(OnGameSpeedBtnClicked);
            _mainPage.Speed2XBtn.onClick.Add(OnGameSpeedBtnClicked);
            _mainPage.Speed3XBtn.onClick.Add(OnGameSpeedBtnClicked);
            
            _mainPage.SettingPanel.YesBtn.onClick.Add(GameEntry.ExitGame);
            _mainPage.SettingPanel.NoBtn.onClick.Add(()=>_mainPage.SettingPanelCtrl.SetSelectedPage("close"));
            
            DispatchMessage(new Message{Command =  Message.CommonCommand.Show});
        }

        #region 建筑相关按钮响应函数
        private bool _isBuildingListOpen;
        private EBuildingType _nowSelectType;
        private GList _buildingCaseList;
        private BuildingManager BuildingMgr => BuildingManager.Instance;
        
        private void OnBuildBtnClicked() {
            _isBuildingListOpen = !_isBuildingListOpen;
            if (!_isBuildingListOpen) {
                _buildingCaseList.ClearSelection();
                return;
            }
            
            _nowSelectType = EBuildingType.Any;
            DispatchMessage(new Message {
                Command =  MainPageUICommand.UpdateBuildingList,
                ExtraParams = _nowSelectType
            });
        }

        private void OnBuildingTypeBtnClicked(EventContext context) {
            string btnName = ((BuildingTypeBtn)context.sender).name;
            string buildingTypeStr = btnName.Replace("BuildingTab", "").Replace("Btn", "");
            EBuildingType buildingType = Enum.Parse<EBuildingType>(buildingTypeStr);
            if (_nowSelectType == buildingType) {
                return;
            }

            _nowSelectType = buildingType;
            DispatchMessage(new Message {
                Command =  MainPageUICommand.UpdateBuildingList,
                ExtraParams = buildingType
            });
        }

        private void OnBuildingBtnClicked() {
            int selectedIndex = _buildingCaseList.selectedIndex;
            BuildingIconCase targetCase = (BuildingIconCase)_buildingCaseList.GetChildAt(selectedIndex);
            string buildingKey = targetCase.name;
            if (!BuildingMgr.CanAffordConstructCosts(buildingKey)) {
                // todo: 显示<没有足够的材料用以建筑>的提示UI
                return;
            }

            BuildingMgr.ConstructNewBuilding(buildingKey);
            OpenOrCloseBuildingList(false);
        }

        public void OpenOrCloseBuildingList(bool isOpen) {
            DispatchMessage(new Message {
                Command = isOpen ? MainPageUICommand.OpenBuildingList : MainPageUICommand.CloseBuildingList
            });
        }
        #endregion

        #region 资源相关按钮响应函数

        // todo: 资源相关按钮响应函数实现

        #endregion

        #region 游戏速度相关按钮响应函数

        // todo: 游戏速度相关按钮响应函数实现
        private void OnGameSpeedBtnClicked(EventContext context) {
            var btnName = ((GButton)context.sender).name;
            var gamePlayMgr = GameEntry.GamePlayModuleMgr;
            switch (btnName) {
                case "Speed1XBtn":
                    gamePlayMgr.updateSpeed = gamePlayMgr.updateSpeed == 1 ? 0 : 1;
                    break;
                case "Speed2XBtn":
                    gamePlayMgr.updateSpeed = 3;
                    break;
                case "Speed3XBtn":
                    gamePlayMgr.updateSpeed = 6;
                    break;
            }
        }
        
        #endregion

        #region 天气相关按钮响应函数

        // todo: 天气相关按钮响应函数实现

        #endregion
    }
}