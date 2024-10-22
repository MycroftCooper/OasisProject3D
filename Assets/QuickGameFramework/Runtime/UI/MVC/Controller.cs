using System;
using FairyGUI;
using MycroftToolkit.QuickCode;
using UnityEngine;

namespace QuickGameFramework.Runtime.UI {
    public class Controller : MonoBehaviour {
        private MonoBehaviour _model;
        private MonoBehaviour _view;
        protected UIPanel UIPanel;

        public bool IsShowing { get; private set; }

        private void Awake() {
            foreach (var component in transform.GetComponents<MonoBehaviour>()) {
                var componentName = component.GetType().ToString();
                if (componentName.Contains("Model")) {
                    _model = component;
                } else if (componentName.Contains("View")) {
                    _view = component;
                }
            }
            UIPanel = GetComponent<UIPanel>();
        }

        public void DispatchMessage(Message message) {
            IsShowing = message.Command switch {
                Message.CommonCommand.Show => true,
                Message.CommonCommand.Hide => false,
                _ => IsShowing
            };

            var modelType = _model.GetType();
            var handleMessage = modelType.GetMethod("HandleMessage");
            if (handleMessage == null) {
                return;
            }
            var data = handleMessage.Invoke(_model, new object[] {message});
            
            var viewType = _view.GetType();
            var handleCommand = viewType.GetMethod("HandleCommand");
            if (handleCommand == null) {
                return;
            }
            handleCommand.Invoke(_view, new[] {message.Command, data});
        }

        public void DispatchMessageDelayed(Message message, float time) {
            Timer.Register(time, () => { DispatchMessage(message); }, null, false,true);
        }
        
        public void Show(ValueType extraParams) {
            DispatchMessage(new Message {
                Command = Message.CommonCommand.Show,
                ExtraParams = extraParams
            });
        }
        
        public void Hide(ValueType extraParams) {
            DispatchMessage(new Message {
                Command = Message.CommonCommand.Hide,
                ExtraParams = extraParams
            });
        }
    }
}
