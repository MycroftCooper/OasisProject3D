using System.Collections.Generic;
using SimpleJSON;
using System.IO;
using System.Linq;
using cfg;
using UnityEngine;
using YooAsset;

namespace QuickGameFramework.Runtime {
    public class DataTableManager {
        private List<AssetOperationHandle> _jsonFileLoadHandles;
        private const string JsonFilePath = "Assets/GameMain/DataTables/Json/";
        private const string JsonFileTag = "DataTable";
        public Tables Tables;

        public DataTableManager() {
            Dictionary<string, string> tableRawFiles = new Dictionary<string, string>();
            _jsonFileLoadHandles = GameEntry.AssetMgr.LoadAssetsAsyncByTag<TextAsset>(JsonFileTag,
                (text, path) => { tableRawFiles.Add(path, text.text); }).ToList();
            AssetLoadProgress progress = new AssetLoadProgress();
            _jsonFileLoadHandles.ForEach(_ => progress += _);
            progress.Completed += () => {
                Tables = new Tables(file => JSON.Parse(tableRawFiles[$"{JsonFilePath}{file}.json"]));
                if (Tables != null) {
                    QLog.Log("QuickGameFramework>DataTableManager>配表系统初始化成功!");
                } else {
                    QLog.Error("QuickGameFramework>DataTableManager>配表系统初始化失败!");
                }
            };
        }

        public Tables LoadTable(string tableName) {
            string pathName = JsonFilePath + "/" + tableName + ".json";
            if (File.Exists(pathName)) {
                return new Tables(_ => JSON.Parse(File.ReadAllText(JsonFilePath + "/" + tableName + ".json")));
            }
            Debug.LogError($"QuickGameFramework>DataTableManager>Error> 该文件不存在:{pathName}");
            return null;
        }
    }
}
