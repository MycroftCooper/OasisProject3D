using OasisProject3D.MapSystem;
using QuickGameFramework.Procedure;
using QuickGameFramework.Runtime;

public class EnterGameProcedure : Procedure {
    
    protected override void OnEnter(params object[] parameters) {
        AssetLoadProgress assetLoadProgress = new AssetLoadProgress();
        assetLoadProgress.AddHandles(BlockFactory.Instance.PreLoadAsset());
        assetLoadProgress.Completed += () => GameEntry.ModuleMgr.CreateModule<MapManager>();
    }

    protected override void OnUpdate(float intervalSeconds) {

    }

    protected override void OnExit() {

    }

    protected override void OnDestroy() {

    }
}
