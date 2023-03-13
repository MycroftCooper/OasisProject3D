using UnityEditor;
using UnityEngine;

public class QuickRunGame : Editor {
    private const string StartScenePath = "Assets/GameMain/Scenes/EntryScene.unity";

    [MenuItem("QuickDebug/Run Game _%g")]
    public static void RunGame() {
        if (Application.isPlaying) {
            return;
        }
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene(StartScenePath);
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }
}