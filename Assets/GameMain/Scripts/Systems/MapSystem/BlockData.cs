using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OasisProject3D.MapSystem {
    public class BlockData {
        public Vector3 WorldPos;
        public Vector2Int LogicalPos;
        public float Hight;

        public float VegetationCoverage;
        public BlockInfectionConf InfectionConf;

        public bool Buildable;
    }

    public struct BlockInfectionConf {
        public bool CanInfect;
        public bool CanBeInfect;
        [LabelText("地块转化值")]
        public Vector2 VCRange;
        [LabelText("传染半径")]
        public int Infection_Range;
        [LabelText("传染系数")]
        public float Infection_Factor;
        [LabelText("传染时间间隔")]
        public float Infection_Time;
    }
}
