using MycroftToolkit.QuickCode;
using OasisProject3D.MapSystem;
using OasisProject3D.UI.GameEntryUIPackage;
using QuickGameFramework.Procedure;
using QuickGameFramework.Runtime;

public class GameLoadingProcedure : Procedure {
    private bool _isLoadCompleted;
    Timer _timer;
    private ProgressBar1 _progressBar;
    private AssetLoadProgress _assetLoadProgress;
    
    protected override void OnEnter(params object[] parameters) {
        _progressBar = GameEntry.UIMgr.GetUIInstance<LoadingPage>(nameof(LoadingPage)).ProgressBar;
        _assetLoadProgress = BlockFactory.Instance.PreLoadAsset();
        _assetLoadProgress += GameEntry.UIMgr.PreLoadAsset();
        _assetLoadProgress.Completed += () => _isLoadCompleted = true;
        
        _progressBar.value = 0;
        _timer = Timer.Register(3, null);
    }

    protected override void OnUpdate(float intervalSeconds) {
        if (_isLoadCompleted && _timer.isCompleted) {
            Exit();
            return;
        }
        _progressBar.Update(_timer.GetTimeElapsed()/_timer.duration *100);
        // _progressBar.Update(_assetLoadProgress.Progress); 现在加载太快了，但总得演一下吧
    }

    protected override void OnExit() {
        GameEntry.ChangeSceneAsync("MainGameScene", () => {
            GameEntry.ModuleMgr.CreateModule<MapManager>();
            GameEntry.AssetMgr.UnloadAssets();
        });
    }

    protected override void OnDestroy() { }
}
