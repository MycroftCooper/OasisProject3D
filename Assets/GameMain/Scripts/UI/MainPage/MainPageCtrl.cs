using System;
using cfg;
using FairyGUI;
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
            _mainPage.BuildingList.BuildingTabAnyBtn.onClick.Add(OnBuildingTypeBtnClicked);
            _mainPage.BuildingList.BuildingTabFunctionBtn.onClick.Add(OnBuildingTypeBtnClicked);
            _mainPage.BuildingList.BuildingTabEcoBtn.onClick.Add(OnBuildingTypeBtnClicked);
            _mainPage.BuildingList.BuildingTabProductBtn.onClick.Add(OnBuildingTypeBtnClicked);
            _mainPage.BuildingList.BuildingTabStorageBtn.onClick.Add(OnBuildingTypeBtnClicked);
            _mainPage.BuildingList.BuildingCaseList.onClickItem.Add(OnBuildingBtnClicked);

            _isBuildingListOpen = false;
            
            DispatchMessage(new Message{Command =  Message.CommonCommand.Show});
        }

        #region 建筑相关按钮响应函数
        private bool _isBuildingListOpen;
        private EBuildingType _nowSelectType;
        
        private void OnBuildBtnClicked() {
            _isBuildingListOpen = !_isBuildingListOpen;
            if (!_isBuildingListOpen) {
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

        public void OnBuildingBtnClicked(EventContext context) {
            string buildingKey = ((BuildingIconCase)context.sender).BuildingIcon.name;
            QLog.Error(buildingKey);
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