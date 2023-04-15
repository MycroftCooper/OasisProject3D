using System.Collections.Generic;
using cfg;

namespace OasisProject3D.ResourceSystem {
    /// <summary>
    /// 资源操作句柄
    /// </summary>
    public class ResRecorder {
        private ResRecordData _data;
        public Dictionary<EResType, float> Production => _data.Production;
        public Dictionary<EResType, float> Consumption => _data.Production;

        public ResRecorder() {
            _data = new ResRecordData {
                Production = new Dictionary<EResType, float>(),
                Consumption = new Dictionary<EResType, float>(),
                Source = null
            };
        }

        public ResRecorder(ResRecordData data) {
            _data = data;
        }

        public void AddProduction(EResType resType, float num) {
            if (_data.Production.ContainsKey(resType)) {
                _data.Production[resType] += num;
            }
            _data.Production.Add(resType, num);
        }

        public void AddConsumption(EResType resType, float num) {
            if (_data.Consumption.ContainsKey(resType)) {
                _data.Consumption[resType] += num;
            }
            _data.Consumption.Add(resType, num);
        }

        public Dictionary<EResType, float> GetProfits() {
            var output = new Dictionary<EResType, float>();
            foreach (var kv in Production) {
                output.Add(kv.Key,kv.Value);
            }

            foreach (var kv in Consumption) {
                if (output.ContainsKey(kv.Key)) {
                    output[kv.Key] -= kv.Value;
                } else {
                    output.Add(kv.Key, -kv.Value);
                }
            }
            return output;
        }
    }
}