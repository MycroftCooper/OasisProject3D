using System.Collections.Generic;

namespace OasisProject3D.ResourceSystem {
    public enum EResourceType {
        BuildingMaterials,  // 建筑材料
        Water,              // 水
        Electricity,        // 电力
        Seedlings,          // 幼苗
        Money,              // 金币
    }
    
    public enum EStorageStatus {
        Normal,    // 成功
        Overflow,   // 溢出
        Lack,       // 缺乏
    }

    public struct ResRecordData {
        public Dictionary<EResourceType, float> Production;
        public Dictionary<EResourceType, float> Consumption;
        public string Source;
    }
    
    public class ResourceData {
    }
}