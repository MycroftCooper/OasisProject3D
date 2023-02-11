//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;



namespace cfg.BuildingSystem
{ 

public sealed partial class DTBuildingConfig
{
    private readonly Dictionary<string, BuildingSystem.BuildingConfig> _dataMap;
    private readonly List<BuildingSystem.BuildingConfig> _dataList;
    
    public DTBuildingConfig(JSONNode _json)
    {
        _dataMap = new Dictionary<string, BuildingSystem.BuildingConfig>();
        _dataList = new List<BuildingSystem.BuildingConfig>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = BuildingSystem.BuildingConfig.DeserializeBuildingConfig(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.Name, _v);
        }
        PostInit();
    }

    public Dictionary<string, BuildingSystem.BuildingConfig> DataMap => _dataMap;
    public List<BuildingSystem.BuildingConfig> DataList => _dataList;

    public BuildingSystem.BuildingConfig GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public BuildingSystem.BuildingConfig Get(string key) => _dataMap[key];
    public BuildingSystem.BuildingConfig this[string key] => _dataMap[key];

    public void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var v in _dataList)
        {
            v.Resolve(_tables);
        }
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var v in _dataList)
        {
            v.TranslateText(translator);
        }
    }
    
    
    partial void PostInit();
    partial void PostResolve();
}

}