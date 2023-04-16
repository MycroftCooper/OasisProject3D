using System;
using System.Collections.Generic;
using System.Globalization;
using cfg;
using FairyGUI;
using OasisProject3D.BuildingSystem;
using QuickGameFramework.Runtime.UI;
using UnityEngine;

namespace OasisProject3D.UI.GameMainUIPackage {
    internal class MainPageView : View<MainPageUIData> {
        private MainPage _mainPage;

        protected override void Start() {
            base.Start();
            _mainPage = (MainPage)UIPanel.ui;
        }

        protected override void OnShow(MainPageUIData uiData) { }

        protected override void OnHide(MainPageUIData uiData) {
            throw new NotImplementedException();
        }

        #region 页面刷新相关
        protected void OnVegetationCoverageRefresh(MainPageUIData uiData) {
            _mainPage.GreenRateBar.value = uiData.GreenRate;
        }
        
        protected void OnResDataRefresh(MainPageUIData uiData) {
            Dictionary<EResType, float> resDict = uiData.ResData;
            foreach (var kv in resDict) {
                EResType resType = kv.Key;
                string numText = kv.Value.ToString("F1", CultureInfo.InvariantCulture);
                switch (resType) {
                    case EResType.BuildingMaterial:
                        _mainPage.WoodNum.text = numText;
                        break;
                    case EResType.Water:
                        _mainPage.WaterNum.text = numText;
                        break;
                    case EResType.Electricity:
                        break;
                    case EResType.Seedling:
                        _mainPage.SaplingNum.text = numText;
                        break;
                    case EResType.Money:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        
        
        #endregion

        #region 建筑列表相关
        protected void OnBuildingListRefresh(MainPageUIData uiData) {
            GList buildingList = _mainPage.BuildingList.BuildingCaseList;
            buildingList.RemoveChildrenToPool();
            var buildingMgr = BuildingManager.Instance;
            List<string> buildingKeys = buildingMgr.GetBuildingKeys(uiData.SelectedBuildingType);
            foreach (var buildingKey in buildingKeys) {
                Sprite buildingIcon = BuildingFactory.Instance.GetBuildingIcon(buildingKey);
                if (buildingIcon == null) {
                    continue;
                }
                BuildingIconCase buildingIconCase = (BuildingIconCase)buildingList.AddItemFromPool();
                buildingIconCase.BuildingIcon.texture = new NTexture(buildingIcon);
                buildingIconCase.BuildingName.text = buildingMgr.GetBuildingName(buildingKey);
                buildingIconCase.name = buildingKey;
            }
        }
        
        private void OnOpenOrCloseBuildingList(bool isOpen) {
            if (isOpen) {
                _mainPage.BuildingListCtrl.SetSelectedPage("open");
                return;
            }
            _mainPage.BuildingList.BuildingCaseList.ClearSelection();
            _mainPage.BuildingListCtrl.SetSelectedPage("close");
        }
        #endregion
        

        protected override void ProcessMessage(ValueType command, MainPageUIData uiData) {
            MainPageUICommand cmd = (MainPageUICommand)command;
            switch (cmd) {
                case MainPageUICommand.UpdateVegetationCoverage:
                    OnVegetationCoverageRefresh(uiData);
                    break;
                case MainPageUICommand.UpdateResData:
                    OnResDataRefresh(uiData);
                    break;
                case MainPageUICommand.UpdateBuildingList:
                    OnBuildingListRefresh(uiData);
                    break;
                case MainPageUICommand.CloseBuildingList:
                    OnOpenOrCloseBuildingList(false);
                    break;
                case MainPageUICommand.OpenBuildingList:
                    OnOpenOrCloseBuildingList(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override bool OnBackButtonClicked() {
            throw new NotImplementedException();
        }
    }
}