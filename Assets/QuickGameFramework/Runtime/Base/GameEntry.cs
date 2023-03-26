using System;
using QuickGameFramework.Procedure;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuickGameFramework.Runtime {
    public class GameEntry : MonoSingleton<GameEntry> {
        public static ModuleManager ModuleMgr;
        public static ProcedureManager ProcedureMgr;
        public static CoroutineManager CoroutineMgr;
        public static DataTableManager DataTableMgr;
        public static AssetManager AssetMgr;
        public static UIManager UIMgr;
        public static ConfigManager ConfigMgr;
        public static InputManager InputMgr;

        private void Awake() {
            IsGlobal = true;
            
            CoroutineMgr = GetComponent<CoroutineManager>();
            ConfigMgr = new ConfigManager();
            DataTableMgr = new DataTableManager();
            ModuleMgr = GetComponent<ModuleManager>();
            ProcedureMgr = ModuleMgr.CreateModule<ProcedureManager>();
            InputMgr = transform.Find("PlayerInputManager").GetComponent<InputManager>();
            
            AssetMgr = new AssetManager();
            AssetMgr.Init(() => {
                UIMgr = new UIManager();
                ChangeScene("StartScene");
            });
        }

        public static void ChangeScene(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }

        public static void ChangeSceneAsync(string sceneName, Action callback) {
            SceneManager.LoadSceneAsync(sceneName).completed += _ => { callback?.Invoke(); };
        }

        public static void ExitGame() {
            if (Application.isEditor) {
                UnityEditor.EditorApplication.isPlaying = false;
            }
            Application.Quit();
        }
    }
}