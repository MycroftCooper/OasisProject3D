using System;
using cfg;
using FairyGUI;
using OasisProject3D.BuildingSystem;
using QuickGameFramework.Runtime;
using QuickGameFramework.Runtime.UI;
using Controller = QuickGameFramework.Runtime.UI.Controller;

namespace OasisProject3D.UI.GameMainUIPackage {
    public class MainPageCtrl: Controller  {
        private MainPage _mainPage;
        private void Start() {
            _mainPage = (MainPage)UIPanel.ui;
            _mainPage.BuildBtn.onClick.Add(OnBuildBtnClicked);
            var buildingList = _mainPage.BuildingList;
            buildingList.BuildingTabAnyBtn.onClick.Add(OnBuildingTypeBtnClicked);
            buildingList.BuildingTabFunctionBtn.onClick.Add(OnBuildingTypeBtnClicked);
            buildingList.BuildingTabEcoBtn.onClick.Add(OnBuildingTypeBtnClicked);
            buildingList.BuildingTabProductBtn.onClick.Add(OnBuildingTypeBtnClicked);
            buildingList.BuildingTabStorageBtn.onClick.Add(OnBuildingTypeBtnClicked);
            buildingList.BuildingCaseList.onClickItem.Add(OnBuildingBtnClicked);

            _isBuildingListOpen = false;
            _buildingCaseList = buildingList.BuildingCaseList;
            
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

        public void OnBuildingBtnClicked() {
            int selectedIndex = _buildingCaseList.selectedIndex;
            BuildingIconCase targetCase = (BuildingIconCase)_buildingCaseList.GetChildAt(selectedIndex);
            string buildingKey = targetCase.name;
            if (!BuildingMgr.CanAffordConstructCosts(buildingKey)) {
                // todo: 显示<没有足够的材料用以建筑>的提示UI
                return;
            }

            BuildingMgr.ConstructNewBuilding(buildingKey);
        }
        #endregion

        #region 资源相关按钮响应函数

        // todo: 资源相关按钮响应函数实现

        #endregion

        #region 游戏速度相关按钮响应函数

        // todo: 游戏速度相关按钮响应函数实现
        
        #endregion

        #region 天气相关按钮响应函数

        // todo: 天气相关按钮响应函数实现

        #endregion
    }
}