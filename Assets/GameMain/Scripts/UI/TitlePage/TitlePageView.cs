using FairyGUI;
using Medium.FGUI;
using QuickGameFramework.Runtime.UI;
using System;
using UnityEngine;

namespace UI
{
    public class TitlePageView : View<TitlePageData>
    {
        private UIPanel _uiPanel;
        private TitlePageUI _dialog;

        protected override void Start()
        {
            base.Start();

            _uiPanel = GetComponent<UIPanel>();
            var content = _uiPanel?.ui;
            _dialog = new(content);
        }

        private void OnDestroy()
        {
            _dialog.Dispose();
        }

        protected override void OnShow(TitlePageData data)
        {
            throw new NotImplementedException();
        }

        protected override void OnHide(TitlePageData data)
        {
            throw new NotImplementedException();
        }

        protected override void OnRefresh(TitlePageData data)
        {
            throw new NotImplementedException();
        }

        protected override void ProcessMessage(ValueType command, TitlePageData data)
        {
            throw new NotImplementedException();
        }

        protected override bool OnBackButtonClicked()
        {
            throw new NotImplementedException();
        }
    }



    public class TitlePageUI : UIObjectWrap<GComponent>
    {
        private GButton _StartBtn;
        private GButton _ContinueBtn;

        public TitlePageUI(GComponent content) : base(content) { }

        protected override void InitComponent()
        {
            base.InitComponent();

            _StartBtn?.onClick.Add(OnClickStartBtn);
            _ContinueBtn?.onClick.Add(OnClickContinueBtn);
        }

        private void OnClickStartBtn()
        {
            Debug.Log("开始游戏点击成功");
        }

        private void OnClickContinueBtn()
        {
            Debug.Log("继续游戏点击成功");
        }
    }
}

