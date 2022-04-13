using System;
using System.Collections.Generic;
using UnityEngine;
namespace UGFExtensions{
    public static partial class DataTableExtension {
        public static Dictionary<int, int> ParseInt32Int32Dictionary(string value) {
            if (string.IsNullOrEmpty(value) || value.ToLowerInvariant().Equals("null"))
                return null;
            string[] splitValue = value.Split('|');
            Dictionary<int, int> dictionary = new Dictionary<int, int>(splitValue.Length);
            for (int i = 0; i < splitValue.Length; i++) {
                string[] keyValue = splitValue[i].Split('#');
                dictionary.Add(Int32.Parse(keyValue[0].Substring(1)), Int32.Parse(keyValue[1].Substring(0, keyValue[1].Length - 1)));
            }
            return dictionary;
        }
        public static Dictionary<int, Vector3> ParseInt32Vector3Dictionary(string value) {
            if (string.IsNullOrEmpty(value) || value.ToLowerInvariant().Equals("null"))
                return null;
            string[] splitValue = value.Split('|');
            Dictionary<int, Vector3> dictionary = new Dictionary<int, Vector3>(splitValue.Length);
            for (int i = 0; i < splitValue.Length; i++) {
                string[] keyValue = splitValue[i].Split('#');
                dictionary.Add(Int32.Parse(keyValue[0].Substring(1)), ParseVector3(keyValue[1].Substring(0, keyValue[1].Length - 1)));
            }
            return dictionary;
        }

        public static Dictionary<int, Vector2> ParseInt32Vector2Dictionary(string value) {
            if (string.IsNullOrEmpty(value) || value.ToLowerInvariant().Equals("null"))
                return null;
            string[] splitValue = value.Split('|');
            Dictionary<int, Vector2> dictionary = new Dictionary<int, Vector2>(splitValue.Length);
            for (int i = 0; i < splitValue.Length; i++) {
                string[] keyValue = splitValue[i].Split('#');
                dictionary.Add(Int32.Parse(keyValue[0].Substring(1)), ParseVector2(keyValue[1].Substring(0, keyValue[1].Length - 1)));
            }
            return dictionary;
        }

        public static Dictionary<int, float> ParseInt32SingleDictionary(string value) {
            if (string.IsNullOrEmpty(value) || value.ToLowerInvariant().Equals("null"))
                return null;
            string[] splitValue = value.Split('|');
            Dictionary<int, float> dictionary = new Dictionary<int, float>(splitValue.Length);
            for (int i = 0; i < splitValue.Length; i++) {
                string[] keyValue = splitValue[i].Split('#');
                dictionary.Add(Int32.Parse(keyValue[0].Substring(1)), float.Parse(keyValue[1].Substring(0, keyValue[1].Length - 1)));
            }
            return dictionary;
        }

        public static Dictionary<float, string> ParseSingleStringDictionary(string value) {
            if (string.IsNullOrEmpty(value) || value.ToLowerInvariant().Equals("null"))
                return null;
            string[] splitValue = value.Split('|');
            Dictionary<float, string> dictionary = new Dictionary<float, string>(splitValue.Length);
            for (int i = 0; i < splitValue.Length; i++) {
                string[] keyValue = splitValue[i].Split('#');
                dictionary.Add(float.Parse(keyValue[0].Substring(1)), keyValue[1].Substring(0, keyValue[1].Length - 1));
            }
            return dictionary;
        }

        public static Dictionary<int, string> ParseReadInt32StringDictionary(string value) {
            if (string.IsNullOrEmpty(value) || value.ToLowerInvariant().Equals("null"))
                return null;
            string[] splitValue = value.Split('|');
            Dictionary<int, string> dictionary = new Dictionary<int, string>(splitValue.Length);
            for (int i = 0; i < splitValue.Length; i++) {
                string[] keyValue = splitValue[i].Split('#');
                dictionary.Add(Int32.Parse(keyValue[0].Substring(1)), keyValue[1].Substring(0, keyValue[1].Length - 1));
            }
            return dictionary;
        }

        public static Dictionary<string, string> ParseReadStringStringDictionary(string value) {
            if (string.IsNullOrEmpty(value) || value.ToLowerInvariant().Equals("null"))
                return null;
            string[] splitValue = value.Split('|');
            Dictionary<string, string> dictionary = new Dictionary<string, string>(splitValue.Length);
            for (int i = 0; i < splitValue.Length; i++) {
                string[] keyValue = splitValue[i].Split('#');
                dictionary.Add(keyValue[0].Substring(1), keyValue[1].Substring(0, keyValue[1].Length - 1));
            }
            return dictionary;
        }
    }
}
