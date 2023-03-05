using UnityEditor;

namespace OasisProject3D.Editor {
    public static class ProjectNavigation  {
        [MenuItem("OasisProject3D/文档/导航/开发书签")]
        public static void OpenDevelopBookmark() {
            System.Diagnostics.Process.Start("https://qm2p49yrba.feishu.cn/wiki/wikcnBoGYXWyyVCjLV1ukvGEzJb");
        }
        
        [MenuItem("OasisProject3D/文档/导航/需求文档")]
        public static void OpenRequirementDocumentation() {
            System.Diagnostics.Process.Start("https://qm2p49yrba.feishu.cn/wiki/wikcnv0r9OsTZCAXGyRuzwTmpVe");
        }
        
        [MenuItem("OasisProject3D/打开/资源/Icon")]
        public static void OpenIconPath() {
            EditorUtility.RevealInFinder("Assets/GameRes/UI/Icon/IconAtlas");
        }
        
        [MenuItem("OasisProject3D/打开/资源/UI")]
        public static void OpenUIPath() {
            EditorUtility.RevealInFinder("Assets/GameRes/UI/GameEntry");
        }
    }
}

