/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class BuildingDescTab : GComponent
    {
        public Controller BuildingStateCtrl;
        public Controller CanUpgrade;
        public Controller IsOpen;
        public BuildingIconCaseHex IconCase;
        public BuildingDescStateIcon StateIcon;
        public GButton nextBuilding;
        public GButton lastBuilding;
        public BuildingDescBaseInfo BasicInfoBox;
        public BuildingDescBuildCostInfoBox CostInfoBox;
        public BuildingDescProduceInfoBox ProduceInfoBox;
        public GGroup Body;
        public GButton DeletBtn;
        public GButton UpgradeBtn;
        public GButton RebuildBtn;
        public GButton MoveBtn;
        public GButton SwitchBtn;
        public GGroup BuildingDesc;
        public const string URL = "ui://t09fsbe0jobk20";

        public static BuildingDescTab CreateInstance()
        {
            return (BuildingDescTab)UIPackage.CreateObject("GameMainUIPackage", "BuildingDescTab");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            BuildingStateCtrl = GetControllerAt(0);
            CanUpgrade = GetControllerAt(1);
            IsOpen = GetControllerAt(2);
            IconCase = (BuildingIconCaseHex)GetChildAt(1);
            StateIcon = (BuildingDescStateIcon)GetChildAt(2);
            nextBuilding = (GButton)GetChildAt(3);
            lastBuilding = (GButton)GetChildAt(4);
            BasicInfoBox = (BuildingDescBaseInfo)GetChildAt(5);
            CostInfoBox = (BuildingDescBuildCostInfoBox)GetChildAt(6);
            ProduceInfoBox = (BuildingDescProduceInfoBox)GetChildAt(7);
            Body = (GGroup)GetChildAt(8);
            DeletBtn = (GButton)GetChildAt(9);
            UpgradeBtn = (GButton)GetChildAt(10);
            RebuildBtn = (GButton)GetChildAt(11);
            MoveBtn = (GButton)GetChildAt(12);
            SwitchBtn = (GButton)GetChildAt(13);
            BuildingDesc = (GGroup)GetChildAt(14);
        }
    }
}