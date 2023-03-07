using System.Collections.Generic;

namespace OasisProject3D.ResourceSystem {
    /// <summary>
    /// 资源操作句柄
    /// </summary>
    public class ResHandle {
        private ResHandleData _data;
        public Dictionary<EResourceType, float> Production => _data.Production;
        public Dictionary<EResourceType, float> Consumption => _data.Production;

        public ResHandle() {
            _data = new ResHandleData {
                Production = new Dictionary<EResourceType, float>(),
                Consumption = new Dictionary<EResourceType, float>(),
                Source = null
            };
        }

        public void AddProduction(EResourceType resType, float num) {
            if (_data.Production.ContainsKey(resType)) {
                _data.Production[resType] += num;
            }
            _data.Production.Add(resType, num);
        }

        public void AddConsumption(EResourceType resType, float num) {
            if (_data.Consumption.ContainsKey(resType)) {
                _data.Consumption[resType] += num;
            }
            _data.Consumption.Add(resType, num);
        }

        public Dictionary<EResourceType, float> GetProfits() {
            var output = new Dictionary<EResourceType, float>();
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