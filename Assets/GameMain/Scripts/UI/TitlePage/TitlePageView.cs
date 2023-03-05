using FairyGUI;
using QuickGameFramework.Runtime.UI;
using System;

namespace UI {
    public class TitlePageView : View<TitlePageData> {
        private UIPanel _uiPanel;

        protected override void Start() {
            base.Start();

            _uiPanel = GetComponent<UIPanel>();
        }

        protected override void OnShow(TitlePageData data) {
            throw new NotImplementedException();
        }

        protected override void OnHide(TitlePageData data) {
            throw new NotImplementedException();
        }

        protected override void OnRefresh(TitlePageData data) {
            throw new NotImplementedException();
        }

        protected override void ProcessMessage(ValueType command, TitlePageData data) {
            throw new NotImplementedException();
        }

        protected override bool OnBackButtonClicked() {
            throw new NotImplementedException();
        }
    }
}

