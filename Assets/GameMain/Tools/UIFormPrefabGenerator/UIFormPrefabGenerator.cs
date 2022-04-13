using FairyGUI;
using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Editor.UIFormTools {
    public sealed class UIFormPrefabGenerator : EditorWindow {
        private string dataPath = "Assets/GameMain/Tools/UIFormPrefabGenerator/UIFormPrefabGenerator.settingData";
        private string nameSpace = "";
        private string UIScriptsFormPath = "Assets/GameMain/UI/UIScriptsForm";
        private string UIPrefabPath = "Assets/GameMain/UI/UIForms";

        public UIFormPrefabGenerator() {
            readPath();
            this.titleContent = new GUIContent("UIFormPrefabGenerator");
        }

        [MenuItem("Tools/UITools/Generate UIForm Prefab")]
        private static void oppenSettingWindow() =>
            EditorWindow.GetWindow(typeof(UIFormPrefabGenerator));

        private void OnGUI() {
            GUILayout.BeginVertical();
            //绘制标题
            GUILayout.Space(10);
            GUI.skin.label.fontSize = 24;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label("UIFormPrefabGeneratorSetting");

            GUILayout.Space(10);
            nameSpace = EditorGUILayout.TextField("NameSpace", nameSpace);

            GUILayout.Space(10);
            UIScriptsFormPath = EditorGUILayout.TextField("UIScriptsFormPath", UIScriptsFormPath);

            GUILayout.Space(10);
            UIPrefabPath = EditorGUILayout.TextField("UIPrefabPath", UIPrefabPath);

            if (GUILayout.Button("保存更改")) changePath();

            if (GUILayout.Button("生成UI窗口预制体")) generateUIForms();

            GUILayout.EndVertical();
        }

        private void readPath() {
            if (File.Exists(dataPath)) {
                string[] datas = File.ReadAllLines(dataPath);
                if (datas.Length != 3) {
                    Debug.LogError("UI窗口生成器>Error>设置文件受损，请重新设置路径");
                    return;
                }
                nameSpace = datas[0];
                UIScriptsFormPath = datas[1];
                UIPrefabPath = datas[2];
            } else {
                changePath();
            }
        }
        private void changePath() {
            if (!Directory.Exists(UIScriptsFormPath)) {
                Debug.LogError("UI窗口生成器>Error>UIScriptsForm路径不存在，请重新设置路径");
                return;
            }
            if (!Directory.Exists(UIPrefabPath)) {
                Debug.LogError("UI窗口生成器>Error>UIPrefab路径不存在，请重新设置路径");
                return;
            }
            StreamWriter sw = new StreamWriter(dataPath);
            sw.WriteLine(nameSpace);
            sw.WriteLine(UIScriptsFormPath);
            sw.WriteLine(UIPrefabPath);
            sw.Close();
            Debug.Log("UI窗口生成器>Success>成功保存设定");
        }

        private void generateUIForms() {
            //获取指定路径下面的所有资源文件  
            if (Directory.Exists(UIScriptsFormPath)) {
                DirectoryInfo direction = new DirectoryInfo(UIScriptsFormPath);
                DirectoryInfo[] childrenDir = direction.GetDirectories();
                for (int i = 0; i < childrenDir.Length; i++) {
                    //Debug.Log("文件夹名字:" + childrenDir[i].Name);
                    System.IO.FileInfo[] files = childrenDir[i].GetFiles("*.cs");
                    for (int j = 0; j < files.Length; j++) {
                        //Debug.Log("文件名:" + files[j].Name);
                        var componentName = files[j].Name.Replace("Form.cs", "");

                        //Debug.Log("componentName:" + componentName);
                        var prefabName = files[j].Name.Replace(".cs", "");
                        // 创建GameObject对象
                        GameObject gameObj = new GameObject();
                        gameObj.SetLayerRecursively(5);
                        UIPanel panel = gameObj.AddComponent(typeof(UIPanel)) as UIPanel;
                        //panel.packageName = childrenDir[i].Name;
                        //panel.componentName = componentName;

                        panel.SendMessage("OnUpdateSource",
                       new object[] { childrenDir[i].Name, "FGUI/" + childrenDir[i].Name, componentName, true },
                       SendMessageOptions.DontRequireReceiver);
                        UIContentScaler contentScaler = gameObj.AddComponent(typeof(UIContentScaler)) as UIContentScaler;

                        contentScaler.scaleMode = UIContentScaler.ScaleMode.ScaleWithScreenSize;
                        contentScaler.designResolutionX = 1920;
                        contentScaler.designResolutionY = 1080;
                        contentScaler.screenMatchMode = UIContentScaler.ScreenMatchMode.MatchWidthOrHeight;
                        var scriptType = GetUnityType(prefabName);
                        //Debug.Log("prefabName:" + prefabName);
                        // 如果还没有对应的Form,则暂时不创建
                        if (scriptType != null) {
                            gameObj.AddComponent(scriptType);
                            CreateUIPrefab(gameObj, prefabName);
                        }
                        UnityEngine.Object.DestroyImmediate(gameObj);
                    }
                }
            } else {
                Debug.LogError("UI窗口生成器>Error>路径不存在!" + UIScriptsFormPath);
            }

        }

        private void CreateUIPrefab(GameObject obj, string prefabName) {
            string path = UIPrefabPath + "/" + prefabName + ".prefab";
            //参数1 创建路径，参数2 需要创建的对象， 如果路径下已经存在该名字的prefab，则覆盖
            PrefabUtility.SaveAsPrefabAsset(obj, path);
        }

        private Type GetUnityType(string TypeName)
            => Assembly.Load("Assembly-CSharp").GetType(nameSpace + TypeName);
    }
}
