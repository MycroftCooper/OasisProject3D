using FairyGUI;

namespace OasisProject3D.UI
{
    public abstract class UIComponent : GComponent
    {
        #region
        protected abstract void Init();

        public override void Dispose()
        {
            base.Dispose();
            /// TODO
            UnRegisterEvent();
            OnDispose();
        }

        protected abstract void Update();

        protected abstract void RegisterEvent();

        protected virtual void UnRegisterEvent()
        {
            /// TODO
        }

        protected virtual void OnDispose()
        {
            /// TODO
        }
        #endregion
    }
}
