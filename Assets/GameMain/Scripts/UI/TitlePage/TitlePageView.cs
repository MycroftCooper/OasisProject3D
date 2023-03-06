using System;
using System.Collections.Generic;
using FairyGUI;
using OasisProject3D.UI.GameEntryUIPackage;
using QuickGameFramework.Runtime;
using QuickGameFramework.Runtime.UI;

namespace UI {
    public class TitlePageView : View<TitlePageData> {
        private TitlePage _titlePage;

        protected override void OnShow(TitlePageData data) {
            _titlePage = (TitlePage)UIPanel.ui;
            BindBtnClickedHandler();
        }

        protected override void OnHide(TitlePageData data) {
            throw new NotImplementedException();
        }

        protected override void OnRefresh(TitlePageData data) {
            throw new NotImplementedException();
        }

        protected override void ProcessMessage(ValueType command, TitlePageData data) {
            
        }

        #region 按钮

        private List<GButton> _buttons;
        public int defaultBtnTextSize = 60;
        public int selectBtnTextSize = 80;
        private void BindBtnClickedHandler() {
            _buttons = new List<GButton> {
                _titlePage.TitleBtn,  _titlePage.StartBtn, _titlePage.ContinueBtn, _titlePage.SettingBtn, _titlePage.AboutBtn, _titlePage.ExitBtn
            };

            foreach (var btn in _buttons) {
                btn.onClick.Clear();
                btn.onClick.Add(OnTabBtnClicked);
            }

            _titlePage.StartBtn.onClick.Add(OnStartGameBtnClicked);
            
            _titlePage.ExitBtn.onClick.Add(OnExitGameBtnClicked);
        }
        
        private void OnTabBtnClicked() {
            int index = _titlePage.IsSelectBtn.selectedIndex;
            int preIndex = _titlePage.IsSelectBtn.previsousIndex;
            if (preIndex == 0) {
                _buttons.ForEach(_ => _.titleFontSize = defaultBtnTextSize);
            } else {
                _buttons[preIndex].titleFontSize = defaultBtnTextSize;
            }
            
            if (index != 0) {
                _buttons[index].titleFontSize = selectBtnTextSize;
            }
        }

        private void OnStartGameBtnClicked() {
            var loadingPage = GameEntry.UIMgr.CreateFguiComponent(nameof(LoadingPage), LoadingPage.CreateInstance);
            _titlePage.AddChild(loadingPage);
            GameEntry.ProcedureMgr.StartProcedure(typeof(GameLoadingProcedure));
        }

        private void OnExitGameBtnClicked() {
            var confirmWindow = ConfirmWindow.CreateInstance();
            _titlePage.AddChild(confirmWindow);
            confirmWindow.SetPosition(_titlePage.width/2,_titlePage.height/2,0);
            confirmWindow.TitleText.text = "确认离开游戏？";
            confirmWindow.YesBtn.onClick.Set(GameEntry.ExitGame);
            confirmWindow.NoBtn.onClick.Set(() => {
                confirmWindow.Dispose();
                _titlePage.IsSelectBtn.selectedIndex = 0;
                _titlePage.TitleBtn.onClick.Call();
            });
        }
        
        protected override bool OnBackButtonClicked() {
            throw new NotImplementedException();
        }
        #endregion
    }
}

