using UnityEditor;
using UnityEngine;

public class QuickRestart {
    [MenuItem("Tools/快速重启")]
    public static void RestartUnity() {
        EditorApplication.OpenProject(Application.dataPath.Replace("Assets", string.Empty));
    }
}
