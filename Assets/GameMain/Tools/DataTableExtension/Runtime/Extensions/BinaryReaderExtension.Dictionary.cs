using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UGFExtensions {
    public static partial class BinaryReaderExtension {
        public static Dictionary<int, int> ReadInt32Int32Dictionary(this BinaryReader binaryReader) {
            int count = binaryReader.Read7BitEncodedInt32();
            Dictionary<int, int> dictionary = new Dictionary<int, int>(count);
            for (int i = 0; i < count; i++) {
                dictionary.Add(binaryReader.Read7BitEncodedInt32(), binaryReader.Read7BitEncodedInt32());
            }
            return dictionary;
        }
        public static Dictionary<int, Vector3> ReadInt32Vector3Dictionary(this BinaryReader binaryReader) {
            int count = binaryReader.Read7BitEncodedInt32();
            Dictionary<int, Vector3> dictionary = new Dictionary<int, Vector3>(count);
            for (int i = 0; i < count; i++) {
                dictionary.Add(binaryReader.Read7BitEncodedInt32(), ReadVector3(binaryReader));
            }
            return dictionary;
        }

        public static Dictionary<int, Vector2> ReadInt32Vector2Dictionary(this BinaryReader binaryReader) {
            int count = binaryReader.Read7BitEncodedInt32();
            Dictionary<int, Vector2> dictionary = new Dictionary<int, Vector2>(count);
            for (int i = 0; i < count; i++) {
                dictionary.Add(binaryReader.Read7BitEncodedInt32(), ReadVector2(binaryReader));
            }
            return dictionary;
        }

        public static Dictionary<int, float> ReadInt32SingleDictionary(this BinaryReader binaryReader) {
            int count = binaryReader.Read7BitEncodedInt32();
            Dictionary<int, float> dictionary = new Dictionary<int, float>(count);
            for (int i = 0; i < count; i++) {
                dictionary.Add(binaryReader.Read7BitEncodedInt32(), binaryReader.ReadSingle());
            }
            return dictionary;
        }

        public static Dictionary<float, string> ReadSingleStringDictionary(this BinaryReader binaryReader) {
            int count = binaryReader.Read7BitEncodedInt32();
            Dictionary<float, string> dictionary = new Dictionary<float, string>(count);
            for (int i = 0; i < count; i++) {
                dictionary.Add(binaryReader.ReadSingle(), binaryReader.ReadString());
            }
            return dictionary;
        }

        public static Dictionary<int, string> ReadInt32StringDictionary(this BinaryReader binaryReader) {
            int count = binaryReader.Read7BitEncodedInt32();
            Dictionary<int, string> dictionary = new Dictionary<int, string>(count);
            for (int i = 0; i < count; i++) {
                dictionary.Add(binaryReader.Read7BitEncodedInt32(), binaryReader.ReadString());
            }
            return dictionary;
        }

        public static Dictionary<string, string> ReadStringStringDictionary(this BinaryReader binaryReader) {
            int count = binaryReader.Read7BitEncodedInt32();
            Dictionary<string, string> dictionary = new Dictionary<string, string>(count);
            for (int i = 0; i < count; i++) {
                dictionary.Add(binaryReader.ReadString(), binaryReader.ReadString());
            }
            return dictionary;
        }
    }
}
