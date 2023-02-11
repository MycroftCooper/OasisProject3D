using System;
using cfg;
using cfg.MapSystem;
using MycroftToolkit.DiscreteGridToolkit.Hex;
using MycroftToolkit.MathTool;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using MycroftToolkit.DiscreteGridToolkit;
using QuickGameFramework.Runtime;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OasisProject3D.MapSystem {

    public partial class BlockFactory : Singleton<BlockFactory>, IEntityFactory<BlockCtrl> {
        private AssetManager AssetMgr => GameEntry.AssetMgr;
        private const string PackageName = "MainPackage";
        
        public GameObject BlockParent;
        public GameObject BlockPrefab;
        public Dictionary<string, Material> Materials;
        [ShowInInspector, LabelText("地块大小")]
        public static float BlockSize = 10;
        [ShowInInspector, LabelText("地块间距")]
        public static float BlockDistance => BlockSize * 2 * Mathf.Cos(30 * Mathf.Deg2Rad);

        private float _steepParameter;
        private QuickRandom _random;
        
        public Dictionary<EBlockType, BlockConfig> BlockConf;

        public void PreLoadAsset(Action callBack = null) {
            AssetMgr.LoadAssetAsync<GameObject>("Block_Block", target => { BlockPrefab = target;}, PackageName);
            
            Materials = new Dictionary<string, Material>();
            AssetMgr.LoadAssetAsync<Material>("Block_desert_material", target => { Materials.Add(target.name, target);}, PackageName);
            AssetMgr.LoadAssetAsync<Material>("Block_gobi_material", target => { Materials.Add(target.name, target);}, PackageName);
            AssetMgr.LoadAssetAsync<Material>("Block_oasis_material", target => { Materials.Add(target.name, target);}, PackageName);
            PreLoadBlockElementAsset();
        }

        public void Init() {
            BlockParent = GameObject.Find("MapSystem");
            _steepParameter = MapManager.Instance.steepParameter;
            _random = MapManager.Instance.Random;
            BlockConf = MapManager.Instance.BlockConf;
            InitBlockElementFactory();
        }

        public BlockCtrl CreateEntity(string entityID, object data = null) {
            if (data == null) {
                QLog.Error("MapSystem>BlockFactory>地块生成参数为空！生成失败！");
                return default;
            }
            Tuple<Vector2Int, EBlockType> info =(Tuple<Vector2Int, EBlockType>) data;
            BlockCtrl output = addBlock_Base(info.Item1);
            addBlock_Type(output, info.Item2);
            AddBlock_Element(output, info.Item2);
            
            output.Init(entityID, this);
            return output;
        }

        public void RecycleEntity(Entity entity) { }

        private BlockCtrl addBlock_Base(Vector2Int logicalPos) {
            GameObject block = Object.Instantiate(BlockPrefab, BlockParent.transform);
            BlockCtrl output = block.GetComponent<BlockCtrl>();
            output.logicalPos = logicalPos;
            output.height = _random.Noise.GetNoise(logicalPos.x, logicalPos.y) * _steepParameter;
            output.worldPos = HexGridTool.Coordinate_Axial.DiscreteToContinuity(logicalPos, BlockSize, false).ToVec3().SwapYZ();
            output.worldPos += Vector3.up * output.height;
            output.transform.position = output.worldPos;
            return output;
        }

        private void addBlock_Type(BlockCtrl blockCtrl, EBlockType blockType) {
            blockCtrl.blockType = blockType;
            Vector2 vcRange = BlockConf[blockType].GreennessRange;
            blockCtrl.vegetationCoverage = _random.GetFloat(vcRange.x, vcRange.y);
            blockCtrl.InfectionData = GetNewInfectionData(blockType);
            blockCtrl.buildable = BlockConf[blockType].Buildable;
            ChangeBlockMaterials(blockCtrl, blockType);
        }

        private InfectionData GetNewInfectionData(EBlockType blockType) {
            InfectionData data = BlockConf[blockType].InfectionData;
            InfectionData output = new InfectionData(data.Range, data.Factor, data.Time, data.CanInfectious, data.CanBeInfectious);
            return output;
        }

        public void ChangeBlockMaterials(BlockCtrl blockCtrl, EBlockType blockType) {
            Transform blockBase = blockCtrl.transform.Find("Base");
            MeshRenderer renderer = blockBase.GetComponent<MeshRenderer>();
            Material[] newMaterials = new Material[2];
            newMaterials[0] = Materials["Land"];
            newMaterials[1] = Materials[blockType.ToString()];
            renderer.materials = newMaterials;
        }
    }
}
