using System;
using UnityEngine;

namespace Borodar.FarlandSkies.LowPoly.DotParams
{
    [Serializable]
    public class MoonParam
    {
        public float Time;
        public Color TintColor = Color.gray;
        public Color LightColor = Color.gray;
        [Range(0, 8)]
        public float LightIntencity = 1;
    }
}