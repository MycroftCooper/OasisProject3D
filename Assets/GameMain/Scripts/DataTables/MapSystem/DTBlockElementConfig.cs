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



namespace cfg.MapSystem
{ 

public sealed partial class DTBlockElementConfig
{
    private readonly Dictionary<string, MapSystem.BlockElementConfig> _dataMap;
    private readonly List<MapSystem.BlockElementConfig> _dataList;
    
    public DTBlockElementConfig(JSONNode _json)
    {
        _dataMap = new Dictionary<string, MapSystem.BlockElementConfig>();
        _dataList = new List<MapSystem.BlockElementConfig>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = MapSystem.BlockElementConfig.DeserializeBlockElementConfig(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.Name, _v);
        }
        PostInit();
    }

    public Dictionary<string, MapSystem.BlockElementConfig> DataMap => _dataMap;
    public List<MapSystem.BlockElementConfig> DataList => _dataList;

    public MapSystem.BlockElementConfig GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public MapSystem.BlockElementConfig Get(string key) => _dataMap[key];
    public MapSystem.BlockElementConfig this[string key] => _dataMap[key];

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