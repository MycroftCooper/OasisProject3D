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
            _mainPage.BuildingList.BuildingTabAnyBtn.onClick.Add(OnBuildTypeBtnClicked);
            _mainPage.BuildingList.BuildingTabFunctionBtn.onClick.Add(OnBuildTypeBtnClicked);
            _mainPage.BuildingList.BuildingTabEcoBtn.onClick.Add(OnBuildTypeBtnClicked);
            _mainPage.BuildingList.BuildingTabProductBtn.onClick.Add(OnBuildTypeBtnClicked);
            _mainPage.BuildingList.BuildingTabStorageBtn.onClick.Add(OnBuildTypeBtnClicked);
            DispatchMessage(new Message{Command =  Message.CommonCommand.Show});
        }

        #region 建筑相关按钮响应函数
        private bool _isBuildingListOpen;
        private EBuildingType _nowSelectType;
        
        public void OnBuildBtnClicked() {
            
        }

        public void OnBuildTypeBtnClicked(EventContext context) {
            string btnName = ((BuildingTypeBtn)context.sender).name;
            string buildingTypeStr = btnName.Replace("BuildingTab", "").Replace("Btn", "");
            EBuildingType buildingType = Enum.Parse<EBuildingType>(buildingTypeStr);
            if (_nowSelectType == buildingType) {
                return;
            }
            QLog.Error(buildingType);
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