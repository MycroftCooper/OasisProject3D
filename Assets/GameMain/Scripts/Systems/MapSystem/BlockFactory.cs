using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

using MycroftToolkit.QuickCode;
using MycroftToolkit.DiscreteGridToolkit.Hex;


namespace OasisProject3D.MapSystem {

    public class BlockFactory : Singleton<BlockFactory> {
        public GameObject BlockParent;
        [ShowInInspector, LabelText("地块大小")]
        public static float BlockSize = 10;
        [ShowInInspector, LabelText("地块间距")]
        public static float BlockDistance { get => BlockSize * 2 * Mathf.Cos(30 * Mathf.Deg2Rad); }
        public GameObject BlockBase;
        [AssetList(Path = "GameMain/Entities/Block"), LabelText("地块预制体")]
        public List<GameObject> BlockPrefabs;

        public BlockFactory() {
            BlockParent = GameObject.Find("MapSystem");
            GameObject[] prefabs = Resources.LoadAll<GameObject>("Blocks/");
            BlockPrefabs = new List<GameObject>(prefabs);
            BlockBase = BlockPrefabs.Find(p => p.name == "Block");
        }

        private BlockCtrl CreateHexBlockBase(Vector2Int logicalPos) {
            GameObject block = GameObject.Instantiate(BlockBase, BlockParent.transform);
            BlockCtrl output = block.GetComponent<BlockCtrl>();
            output.LogicalPos = logicalPos;
            output.WorldPos = Coordinate_Axial.DiscreteToContinuity(logicalPos, BlockSize, false).ToVec3().SwapYZ();
            return output;
        }
        public BlockCtrl CreateHexBlock(Vector2Int logicalPos, EBlockType blockType) {
            BlockCtrl output = CreateHexBlockBase(logicalPos);

            output.BlockType = blockType;
            GameObject targetType = BlockPrefabs.Find(x => x.name == blockType.ToString());
            GameObject.Instantiate(targetType, output.transform);

            AddHexBlockElement(output);

            return output;
        }

        public void AddHexBlockElement(BlockCtrl blockCtrl) {

        }
    }
}
