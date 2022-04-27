using MycroftToolkit.QuickCode;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OasisProject3D {
    public class DataManager : Singleton<DataManager> {
        string JsonFilePaht = "Assets/GameMain/DataTables/Json/";
        public cfg.Tables Tables;
        public DataManager() {
            Tables = new cfg.Tables(file => JSON.Parse(File.ReadAllText(JsonFilePaht + "/" + file + ".json")));
        }

        public cfg.Tables LoadTable(string tableName) {
            string pathName = JsonFilePaht + "/" + tableName + ".json";
            if (!File.Exists(pathName)) {
                Debug.LogError($"DataManager>Error> 该文件不存在:{pathName}");
                return null;
            }
            return new cfg.Tables(file => JSON.Parse(File.ReadAllText(JsonFilePaht + "/" + tableName + ".json")));
        }
    }
}
