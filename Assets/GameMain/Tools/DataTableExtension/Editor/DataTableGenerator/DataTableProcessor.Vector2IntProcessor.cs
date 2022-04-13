//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.IO;
using UnityEngine;

namespace DE.Editor.DataTableTools {
    public sealed partial class DataTableProcessor {
        private sealed class Vector2IntProcessor : GenericDataProcessor<Vector2Int> {
            public override bool IsSystem => false;

            public override string LanguageKeyword => "Vector2Int";

            public override string[] GetTypeStrings() {
                return new[]
                {
                    "vector2Int",
                    "unityengine.vector2Int"
                };
            }

            public override Vector2Int Parse(string value) {
                var splitedValue = value.Split(',');
                return new Vector2Int(int.Parse(splitedValue[0]), int.Parse(splitedValue[1]));
            }

            public override void WriteToStream(DataTableProcessor dataTableProcessor, BinaryWriter binaryWriter,
                string value) {
                var vectorInt2 = Parse(value);
                binaryWriter.Write(vectorInt2.x);
                binaryWriter.Write(vectorInt2.y);
            }
        }
    }
}
