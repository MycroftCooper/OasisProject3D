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
        [ShowInInspector, LabelText("地块大小")]
        public static float blockSize = 10;
        [ShowInInspector, LabelText("地块间距")]
        public static float BlockDistance { get => blockSize * 2 * Mathf.Cos(30 * Mathf.Deg2Rad); }

        public GameObject blockBasePrefab;
        [AssetList(Path = "GameMain/Resources/Blocks/Prefabs/"), LabelText("地块预制体")]
        public List<GameObject> blockTypePrefabs;

        private float _steepParameter;
        private QuickRandom _random;
        public Dictionary<EBlockType, BlockConfig> blockConf;

        public BlockFactory() {
            blockParent = GameObject.Find("MapSystem");
            GameObject[] prefabs = Resources.LoadAll<GameObject>("Blocks/Prefabs/");
            blockTypePrefabs = new List<GameObject>(prefabs);
            blockBasePrefab = blockTypePrefabs.Find(p => p.name == "Block");
            blockTypePrefabs.Remove(blockBasePrefab);

            _steepParameter = MapManager.Instance.SteepParameter;
            _random = MapManager.Instance.Random;

            blockConf = MapManager.Instance.BlockConf;
        }

        public BlockCtrl AddBlock_Base(Vector2Int logicalPos) {
            GameObject block = GameObject.Instantiate(blockBasePrefab, blockParent.transform);
            BlockCtrl output = block.GetComponent<BlockCtrl>();
            output.logicalPos = logicalPos;
            output.hight = _random.Noise.GetNoise(logicalPos.x, logicalPos.y) * _steepParameter;
            output.worldPos = HexGridTool.Coordinate_Axial.DiscreteToContinuity(logicalPos, blockSize, false).ToVec3().SwapYZ();
            output.worldPos += Vector3.up * output.hight;
            output.transform.position = output.worldPos;

            return output;
        }
        public void AddBlock_Type(BlockCtrl blockCtrl, EBlockType blockType) {
            blockCtrl.blockTypeGO = new Dictionary<EBlockType, GameObject>();
            Transform parent = blockCtrl.transform.Find("BlockType");
            foreach (GameObject prefab in blockTypePrefabs) {
                GameObject targetType = GameObject.Instantiate(prefab, parent);
                blockCtrl.blockTypeGO.Add((EBlockType)Enum.Parse(typeof(EBlockType), prefab.name), targetType);
                if (prefab.name != blockType.ToString()) {
                    targetType.SetActive(false);
                }
            }
            Vector2 vc_range = blockConf[blockType].GreennessRange;
            blockCtrl._vegetationCoverage = _random.GetFloat(vc_range.x, vc_range.y);
            blockCtrl.infectionData = getNewInfectionData(blockType);
            blockCtrl.buildable = blockConf[blockType].Buildable;
        }
        private InfectionData getNewInfectionData(EBlockType blockType) {
            InfectionData data = blockConf[blockType].InfectionData;
            InfectionData output = new InfectionData(data.Range, data.Factor, data.Time, data.CanInfectious, data.CanBeInfectious);
            return output;
        }
        public BlockCtrl CreateHexBlock(Vector2Int logicalPos, EBlockType blockType) {
            BlockCtrl output = AddBlock_Base(logicalPos);
            AddBlock_Type(output, blockType);

            AddBlock_Element(output);

            return output;
        }

        public void AddBlock_Element(BlockCtrl blockCtrl) {

        }
    }
}
