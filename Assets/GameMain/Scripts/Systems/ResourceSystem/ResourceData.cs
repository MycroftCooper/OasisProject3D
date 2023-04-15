using System.Collections.Generic;
using cfg;

namespace OasisProject3D.ResourceSystem {
    public enum EStorageStatus {
        Normal,    // 成功
        Overflow,   // 溢出
        Lack,       // 缺乏
    }

    public struct ResRecordData {
        public Dictionary<EResType, float> Production;
        public Dictionary<EResType, float> Consumption;
        public string Source;
    }
    
    public class ResourceData {
    }
}