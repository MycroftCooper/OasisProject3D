using System;
using Borodar.FarlandSkies.Core.DotParams;
using UnityEngine;

namespace Borodar.FarlandSkies.LowPoly.DotParams
{
    [Serializable]
    public class SkyParam : DotParam
    {
        public Color TopColor = Color.gray;
        public Color MiddleColor = Color.gray;
        public Color BottomColor = Color.gray;
        public Color CloudsTint = Color.gray;
    }
}