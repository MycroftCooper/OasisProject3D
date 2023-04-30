using System;
using cfg;
using QuickGameFramework.Runtime.UI;

namespace OasisProject3D.UI.GameMainUIPackage {
    internal class MainPageModel:Model<MainPageUIData> {
        protected override void OnShow(ValueType extraParams) {
            OnGreenRateRefresh();
            OnResDataRefresh();
        }

        protected override void OnHide(ValueType extraParams) {
            throw new NotImplementedException();
        }

        private void OnGreenRateRefresh() {
            data.GreenRate = GamePlayEnter.MapMgr.GreenRate;
        }
        
        private void OnResDataRefresh() {
            
        }

        protected override void ProcessMessage(Message message) {
            MainPageUICommand cmd = (MainPageUICommand)message.Command;
            switch (cmd) {
                case MainPageUICommand.UpdateGreenRate:
                    OnGreenRateRefresh();
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