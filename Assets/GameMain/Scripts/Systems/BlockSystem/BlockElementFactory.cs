using System.Collections.Generic;
using cfg;
using cfg.MapSystem;
using MycroftToolkit.DiscreteGridToolkit;
using MycroftToolkit.MathTool;
using MycroftToolkit.QuickCode;
using QuickGameFramework.Runtime;
using UnityEngine;

namespace OasisProject3D.BlockSystem {
    public partial class BlockFactory{
        public GameObject ElementPrefab;

        private Dictionary<EBlockType, List<string>> _blockElementDict;
        private Dictionary<string, (List<Mesh> meshes, List<Material> materials)> _elementResDict;

        public GameObjectPool ElementPool;
        public Transform ElementParent;
        
        private Dictionary<string, BlockElementConfig> _elementConfDict;

        private void PreLoadBlockElementAsset(AssetLoadProgress handles) {
            _elementConfDict = GameEntry.DataTableMgr.Tables.DTBlockElementConfig.DataMap;
            
            handles += AssetMgr.LoadAssetAsync<GameObject>("Block_BlockElement", target => { ElementPrefab = target;}); 

            _elementResDict = new Dictionary<string, (List<Mesh> Meshs, List<Material> Materials)>();
            _blockElementDict = new Dictionary<EBlockType, List<string>>();
            foreach (var config in _elementConfDict) {
                string elementName = config.Key;
                EBlockType valueBlockType =config.Value.BlockType;
                if (_blockElementDict.TryGetValue(valueBlockType, out var value)) {
                    value.Add(elementName);
                } else {
                    _blockElementDict.Add(valueBlockType, new List<string>{elementName});
                }

                List<Mesh> meshes = new List<Mesh>(config.Value.ModelPath.Length);
                foreach (var path in config.Value.ModelPath) {
                    handles += AssetMgr.LoadAssetAsync<Mesh>($"Block_{path}", target => {meshes.Add(target);});
                }

                List<Material> materials = new List<Material>(config.Value.MaterialPath.Length);
                foreach (var path in config.Value.MaterialPath) {
                    handles += AssetMgr.LoadAssetAsync<Material>($"Block_{path}", target => {materials.Add(target);});
                }
                _elementResDict.Add(elementName, (meshes, materials));
            }
        }

        public void InitBlockElementFactory() {
            ElementParent = BlockParent.transform.Find("BlockElementPool");
            ElementPool = new GameObjectPool();
            ElementPool.InitPool(ElementPrefab, 1000, ElementParent);
        }

        public void AddBlockElement(BlockCtrl blockCtrl, EBlockType blockType) {
            Transform parent = blockCtrl.transform.Find("Elements");
            for (int i = parent.childCount - 1; i >= 0; i--) {
                ElementPool.Recycle(parent.GetChild(i).gameObject);
            }

            List<string> elementsName = _blockElementDict[blockType];
            foreach (string elemName in elementsName) {
                BlockElementConfig conf = _elementConfDict[elemName];
                (List<Mesh> meshes, List<Material> materials) elemRes = _elementResDict[elemName];
                if (!BlockRandom.GetBool(conf.GenerateRate)) continue;
            
                int num = BlockRandom.GetInt(conf.NumRange + 1);
                for (int i = 0; i < num; i++) {
                    GameObject element = ElementPool.GetObject(parent);
                    element.name = elemName;
                    element.GetComponent<MeshFilter>().mesh = elemRes.meshes.GetRandomObject(BlockRandom);
                    element.GetComponent<MeshRenderer>().materials = new []{elemRes.materials.GetRandomObject(BlockRandom)};
                    Vector3 pos = QuickRandomInArea.GetRandomPoint_Circular(Vector2.zero, (BlockDistance / 2) - 2, BlockRandom).ToVec3().SwapYZ();
                    float rotate = BlockRandom.GetFloat(360);
                    element.transform.localPosition = pos;
                    element.transform.localRotation = Quaternion.Euler(new Vector3(0, rotate, 0));
                    element.transform.localScale = Vector3.one * 2;
                }
            }
        }
    }
}