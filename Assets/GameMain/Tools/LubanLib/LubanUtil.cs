using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class LubanUtil {
    [MenuItem("QuickResources/LubanUtil/ImportExcel")]
    public static void ImportExcel() {
        CleanJson();
        Process proc = new Process();//new 一个Process对象
        string targetDir = string.Format(@"Luban/");//文件目录

        proc.StartInfo.WorkingDirectory = targetDir;
        proc.StartInfo.FileName = "gen_code_json.bat";//文件名字

        proc.Start();
        proc.WaitForExit();
    }
    [MenuItem("QuickResources/LubanUtil/CleanJson")]
    public static void CleanJson() {
        DirectoryInfo dir = new DirectoryInfo("Assets/GameMain/DataTables/Json/");
        if (dir.Exists) {
            DirectoryInfo[] childs = dir.GetDirectories();
            foreach (DirectoryInfo child in childs) {
                child.Delete(true);
            }
            dir.Delete(true);
        }
    }
}
