using System;
using System.Collections.Generic;
using System.Globalization;
using OasisProject3D.ResourceSystem;
using QuickGameFramework.Runtime.UI;

namespace OasisProject3D.UI.GameMainUIPackage {
    internal class MainPageView : View<MainPageUIData> {
        private MainPage _mainPage;
        
        protected override void OnShow(MainPageUIData uiData) {
            _mainPage = (MainPage)UIPanel.ui;
        }

        protected override void OnHide(MainPageUIData uiData) {
            throw new NotImplementedException();
        }

        #region 页面刷新相关
        protected void OnVegetationCoverageRefresh(MainPageUIData uiData) {
            _mainPage.GreenRateBar.value = uiData.GreenRate;
        }
        
        protected void OnResDataRefresh(MainPageUIData uiData) {
            Dictionary<EResourceType, float> resDict = uiData.ResData;
            foreach (var kv in resDict) {
                EResourceType resType = kv.Key;
                string numText = kv.Value.ToString("F1", CultureInfo.InvariantCulture);
                switch (resType) {
                    case EResourceType.BuildingMaterials:
                        _mainPage.WoodNum.text = numText;
                        break;
                    case EResourceType.Water:
                        _mainPage.WaterNum.text = numText;
                        break;
                    case EResourceType.Electricity:
                        break;
                    case EResourceType.Seedlings:
                        _mainPage.SaplingNum.text = numText;
                        break;
                    case EResourceType.Money:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        
        protected void OnBuildingListRefresh(MainPageUIData uiData) {
            throw new NotImplementedException();
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override bool OnBackButtonClicked() {
            throw new NotImplementedException();
        }
    }
}