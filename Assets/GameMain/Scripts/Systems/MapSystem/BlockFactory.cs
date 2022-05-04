using cfg.MapSystem;
using MycroftToolkit.DiscreteGridToolkit.Hex;
using MycroftToolkit.MathTool;
using MycroftToolkit.QuickCode;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace OasisProject3D.MapSystem {

    public class BlockFactory : Singleton<BlockFactory> {
        public GameObject blockParent;
        public GameObject blockPrefab;
        public Dictionary<string, Material> materials;
        [ShowInInspector, LabelText("地块大小")]
        public static float blockSize = 10;
        [ShowInInspector, LabelText("地块间距")]
        public static float BlockDistance { get => blockSize * 2 * Mathf.Cos(30 * Mathf.Deg2Rad); }

        private float _steepParameter;
        private QuickRandom _random;
        public Dictionary<EBlockType, BlockConfig> blockConf;

        public BlockFactory() {
            blockParent = GameObject.Find("MapSystem");
            _steepParameter = MapManager.Instance.SteepParameter;
            _random = MapManager.Instance.Random;
            blockConf = MapManager.Instance.BlockConf;

            // 加载地块材质
            blockPrefab = Resources.Load<GameObject>("Prefabs/Block");
            Material[] m = Resources.LoadAll<Material>("Materials/Blocks");
            materials = new Dictionary<string, Material>();
            foreach (var item in m) {
                materials.Add(item.name, item);
            }

            LoadBlockElementRes();
        }

        private BlockCtrl addBlock_Base(Vector2Int logicalPos) {
            GameObject block = GameObject.Instantiate(blockPrefab, blockParent.transform);
            BlockCtrl output = block.GetComponent<BlockCtrl>();
            output.logicalPos = logicalPos;
            output.hight = _random.Noise.GetNoise(logicalPos.x, logicalPos.y) * _steepParameter;
            output.worldPos = HexGridTool.Coordinate_Axial.DiscreteToContinuity(logicalPos, blockSize, false).ToVec3().SwapYZ();
            output.worldPos += Vector3.up * output.hight;
            output.transform.position = output.worldPos;
            return output;
        }

        private void addBlock_Type(BlockCtrl blockCtrl, EBlockType blockType) {
            blockCtrl.blockType = blockType;
            Vector2 vc_range = blockConf[blockType].GreennessRange;
            blockCtrl._vegetationCoverage = _random.GetFloat(vc_range.x, vc_range.y);
            blockCtrl.infectionData = getNewInfectionData(blockType);
            blockCtrl.buildable = blockConf[blockType].Buildable;
            ChangeBlockMaterials(blockCtrl, blockType);
        }

        private InfectionData getNewInfectionData(EBlockType blockType) {
            InfectionData data = blockConf[blockType].InfectionData;
            InfectionData output = new InfectionData(data.Range, data.Factor, data.Time, data.CanInfectious, data.CanBeInfectious);
            return output;
        }
        public BlockCtrl CreateHexBlock(Vector2Int logicalPos, EBlockType blockType) {
            BlockCtrl output = addBlock_Base(logicalPos);
            addBlock_Type(output, blockType);

            AddBlock_Element(output, blockType);

            return output;
        }
        public void ChangeBlockMaterials(BlockCtrl blockCtrl, EBlockType blockType) {
            Transform blockBase = blockCtrl.transform.Find("Base");
            MeshRenderer renderer = blockBase.GetComponent<MeshRenderer>();
            Material[] newMaterials = new Material[2];
            newMaterials[0] = materials["Land"];
            newMaterials[1] = materials[blockType.ToString()];
            renderer.materials = newMaterials;
        }

        #region 地块元素相关
        public Transform elementParent;
        public Dictionary<string, BlockElementConfig> elementConf;
        public GameObject elementPrefab;

        public struct ElementRes {
            public List<Mesh> meshList;
            public Material[] materials;
            public ElementRes(List<Mesh> meshList, Material[] materials) {
                this.meshList = meshList;
                this.materials = materials;
            }
        }
        public Dictionary<string, ElementRes> elementResDict;
        public GameObjectPool elementPool;

        public void LoadBlockElementRes() {
            // 加载地块元素网格与材质
            elementPrefab = Resources.Load<GameObject>("Prefabs/BlockElement");
            elementParent = blockParent.transform.Find("BlockElementPool");
            elementPool = new GameObjectPool();
            elementPool.InitPool(elementPrefab, 1000, elementParent, false);

            elementConf = DataManager.Instance.Tables.DTBlockElementConfig.DataMap;
            elementResDict = new Dictionary<string, ElementRes>();
            foreach (var dataRow in elementConf) {
                Mesh[] meshs = Resources.LoadAll<Mesh>("Meshes/BlockElements/" + dataRow.Value.ModelPath);
                Material[] materials = Resources.LoadAll<Material>("Materials/BlockElements/" + dataRow.Value.MaterialPath);
                elementResDict.Add(dataRow.Key, new ElementRes(new List<Mesh>(meshs), materials));
            }
        }

        public void AddBlock_Element(BlockCtrl blockCtrl, EBlockType blockType) {
            Transform parent = blockCtrl.transform.Find("Elements");
            for (int i = parent.childCount - 1; i >= 0; i--) {
                elementPool.Recycle(parent.GetChild(i).gameObject);
            }

            string[] elementsName = blockConf[blockType].Elements;
            foreach (string elemName in elementsName) {
                BlockElementConfig conf = elementConf[elemName];
                ElementRes elemRes = elementResDict[elemName];
                if (!_random.GetBool(conf.GenerateRate)) continue;

                int num = _random.GetInt(conf.NumRange + 1);
                for (int i = 0; i < num; i++) {
                    GameObject element = elementPool.GetObject(parent);
                    element.name = elemName;
                    element.GetComponent<MeshFilter>().mesh = elemRes.meshList.GetRandomObject(_random);
                    element.GetComponent<MeshRenderer>().materials = elemRes.materials;
                    Vector3 pos = QuickRandom_Shape.GetRandomPoint_Circular(Vector2.zero, (BlockDistance / 2) - 2, _random).ToVec3().SwapYZ();
                    float rotate = _random.GetFloat(360);
                    element.transform.localPosition = pos + Vector3.up * 5;
                    element.transform.localRotation = Quaternion.Euler(new Vector3(0, rotate, 0));
                    element.transform.localScale = Vector3.one * 2;
                }
            }
        }
        #endregion
    }
}
