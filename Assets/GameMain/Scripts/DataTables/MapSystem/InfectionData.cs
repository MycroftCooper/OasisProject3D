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

public sealed partial class InfectionData :  Bright.Config.BeanBase 
{
    public InfectionData(JSONNode _json) 
    {
        { if(!_json["Range"].IsNumber) { throw new SerializationException(); }  Range = _json["Range"]; }
        { if(!_json["Factor"].IsNumber) { throw new SerializationException(); }  Factor = _json["Factor"]; }
        { if(!_json["Time"].IsNumber) { throw new SerializationException(); }  Time = _json["Time"]; }
        { if(!_json["canInfectious"].IsBoolean) { throw new SerializationException(); }  CanInfectious = _json["canInfectious"]; }
        { if(!_json["canBeInfectious"].IsBoolean) { throw new SerializationException(); }  CanBeInfectious = _json["canBeInfectious"]; }
        PostInit();
    }

    public InfectionData(int Range, float Factor, int Time, bool canInfectious, bool canBeInfectious ) 
    {
        this.Range = Range;
        this.Factor = Factor;
        this.Time = Time;
        this.CanInfectious = canInfectious;
        this.CanBeInfectious = canBeInfectious;
        PostInit();
    }

    public static InfectionData DeserializeInfectionData(JSONNode _json)
    {
        return new MapSystem.InfectionData(_json);
    }

    /// <summary>
    /// 传播范围
    /// </summary>
    public int Range { get; private set; }
    /// <summary>
    /// 传播参数
    /// </summary>
    public float Factor { get; private set; }
    /// <summary>
    /// 刷新时间
    /// </summary>
    public int Time { get; private set; }
    /// <summary>
    /// 可传播
    /// </summary>
    public bool CanInfectious { get; private set; }
    /// <summary>
    /// 可被传播
    /// </summary>
    public bool CanBeInfectious { get; private set; }

    public const int __ID__ = -1322788902;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "Range:" + Range + ","
        + "Factor:" + Factor + ","
        + "Time:" + Time + ","
        + "CanInfectious:" + CanInfectious + ","
        + "CanBeInfectious:" + CanBeInfectious + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
