using System;
using System.Collections;
using System.Collections.Generic;
using cfg;
using QuickGameFramework.Runtime.UI;
using UnityEngine;

namespace OasisProject3D.UI.GameMainUIPackage {
    internal class MainPageModel:Model<MainPageUIData> {
        protected override void OnShow(ValueType extraParams) {
            OnVegetationCoverageRefresh();
            OnResDataRefresh();
        }

        protected override void OnHide(ValueType extraParams) {
            throw new NotImplementedException();
        }

        private void OnVegetationCoverageRefresh() {
            
        }
        
        private void OnResDataRefresh() {
            
        }

        protected override void ProcessMessage(Message message) {
            MainPageUICommand cmd = (MainPageUICommand)message.Command;
            switch (cmd) {
                case MainPageUICommand.UpdateVegetationCoverage:
                    OnVegetationCoverageRefresh();
                    break;
                case MainPageUICommand.UpdateResData:
                    OnResDataRefresh();
                    break;
                case MainPageUICommand.UpdateBuildingList:
                    data.SelectedBuildingType = (EBuildingType)message.ExtraParams;
                    break;
            }
        }
    }
}