using System;
using Borodar.FarlandSkies.Core.DotParams;
using UnityEngine;

namespace Borodar.FarlandSkies.LowPoly.DotParams
{
    [Serializable]
    public class CelestialParam : DotParam
    {
        public Color TintColor = Color.gray;
        public Color LightColor = Color.gray;
        [Range(0, 8)]
        public float LightIntencity = 1;
    }
}