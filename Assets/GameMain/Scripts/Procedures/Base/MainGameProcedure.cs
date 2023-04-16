using OasisProject3D.MapSystem;
using QuickGameFramework.Procedure;
using QuickGameFramework.Runtime;

namespace OasisProject3D.Procedures {
    public class MainGameProcedure : Procedure {
        protected override void OnEnter(params object[] parameters) {
            GameEntry.GamePlayModuleMgr.CreateModule<MapManager>();
        }

        protected override void OnUpdate(float intervalSeconds) {
            
        }

        protected override void OnExit() {
            throw new System.NotImplementedException();
        }

        protected override void OnDestroy() {
            throw new System.NotImplementedException();
        }
    }
}