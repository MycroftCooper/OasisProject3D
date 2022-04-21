using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

using MycroftToolkit.QuickCode;
using MycroftToolkit.DiscreteGridToolkit.Hex;
using System;

namespace OasisProject3D.MapSystem {

    public class BlockFactory : Singleton<BlockFactory> {
        public GameObject BlockParent;
        [ShowInInspector, LabelText("地块大小")]
        public static float BlockSize = 10;
        [ShowInInspector, LabelText("地块间距")]
        public static float BlockDistance { get => BlockSize * 2 * Mathf.Cos(30 * Mathf.Deg2Rad); }
        public GameObject BlockBasePrefab;
        [AssetList(Path = "GameMain/Resources/Blocks/Prefabs/"), LabelText("地块预制体")]
        public List<GameObject> BlockTypePrefabs;

        public BlockFactory() {
            BlockParent = GameObject.Find("MapSystem");
            GameObject[] prefabs = Resources.LoadAll<GameObject>("Blocks/Prefabs/");
            BlockTypePrefabs = new List<GameObject>(prefabs);
            BlockBasePrefab = BlockTypePrefabs.Find(p => p.name == "Block");
            BlockTypePrefabs.Remove(BlockBasePrefab);
        }

        public BlockCtrl AddBlock_Base(Vector2Int logicalPos) {
            GameObject block = GameObject.Instantiate(BlockBasePrefab, BlockParent.transform);
            BlockCtrl output = block.GetComponent<BlockCtrl>();
            output.LogicalPos = logicalPos;
            output.WorldPos = HexGridTool.Coordinate_Axial.DiscreteToContinuity(logicalPos, BlockSize, false).ToVec3().SwapYZ();
            return output;
        }
        public void AddBlock_Type(BlockCtrl blockCtrl, EBlockType blockType) {
            blockCtrl.BlockTypeGO = new Dictionary<EBlockType, GameObject>();
            Transform parent = blockCtrl.transform.Find("BlockType");
            foreach (GameObject prefab in BlockTypePrefabs) {
                GameObject targetType = GameObject.Instantiate(prefab, parent);
                blockCtrl.BlockTypeGO.Add((EBlockType)Enum.Parse(typeof(EBlockType), prefab.name), targetType);
                if (prefab.name != blockType.ToString()) {
                    targetType.SetActive(false);
                }
            }
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
