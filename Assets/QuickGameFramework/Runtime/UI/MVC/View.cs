using System;
using FairyGUI;
using MycroftToolkit.QuickCode;
using UnityEngine;

namespace QuickGameFramework.Runtime.UI {
    public abstract class View<T> : MonoBehaviour {
        protected Controller Controller;
        protected bool IsShowing => Controller.IsShowing;

        protected abstract void OnShow(T data);
        protected abstract void OnHide(T data);
        protected virtual void OnRefresh(T data) { }
        protected abstract void ProcessMessage(ValueType command, T data);
        protected abstract bool OnBackButtonClicked();

        public string uiID;
        public bool useFgui;
        public string fguiBinderName;
        protected UIPanel UIPanel;
        protected virtual void Awake() {
            if (!useFgui) return;
            UIPanel = GetComponent<UIPanel>();
            if(string.IsNullOrEmpty(fguiBinderName)) return;
            Type.GetType(fguiBinderName).InvokeStaticMethod("BindAll");
        }

        protected virtual void Start() {
            Controller = transform.GetComponent<Controller>();
            GameEntry.UIMgr.AddUIInstance(uiID, Controller);
        }

        protected void OnDestroy() {
            GameEntry.UIMgr.RemoveUIInstance(uiID);
        }

        public void HandleCommand(ValueType command, T data) {
            switch (command) {
                case Message.CommonCommand.Show:
                    OnShow(data);
                    break;
                case Message.CommonCommand.Hide:
                    OnHide(data);
                    break;
                case Message.CommonCommand.Refresh:
                    OnRefresh(data);
                    break;
                default:
                    ProcessMessage(command, data);
                    break;
            }
        }

        protected void DispatchMessage(Message message) {
            Controller.DispatchMessage(message);
        }
        
        protected void DispatchMessageDelayed(Message message, float time) {
            Controller.DispatchMessageDelayed(message, time);
        }
    }
}
