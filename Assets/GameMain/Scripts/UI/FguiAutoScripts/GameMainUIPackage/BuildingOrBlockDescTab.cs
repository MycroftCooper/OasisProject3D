/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class BuildingOrBlockDescTab : GComponent
    {
        public Controller BuildingStateCtrl;
        public Controller CanUpgrade;
        public Controller IsOpen;
        public BuildingBasicInfo BasicInfoBox;
        public BuildingDescBuildCostInfoBox CostInfoBox;
        public BuildingDescProduceInfoBox ProduceInfoBox;
        public GGroup Body;
        public BuildingIconCaseHex IconCase;
        public BuildingDescStateIcon StateIcon;
        public GButton DeleteBtn;
        public GButton UpgradeBtn;
        public GButton RebuildBtn;
        public GButton MoveBtn;
        public GButton OpenCloseSwitchBtn;
        public GButton NextBuilding;
        public GButton LastBuilding;
        public GGroup BuildingDescGroup;
        public const string URL = "ui://t09fsbe0jobk20";

        public static BuildingOrBlockDescTab CreateInstance()
        {
            return (BuildingOrBlockDescTab)UIPackage.CreateObject("GameMainUIPackage", "BuildingOrBlockDescTab");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            BuildingStateCtrl = GetControllerAt(0);
            CanUpgrade = GetControllerAt(1);
            IsOpen = GetControllerAt(2);
            BasicInfoBox = (BuildingBasicInfo)GetChildAt(1);
            CostInfoBox = (BuildingDescBuildCostInfoBox)GetChildAt(2);
            ProduceInfoBox = (BuildingDescProduceInfoBox)GetChildAt(3);
            Body = (GGroup)GetChildAt(4);
            IconCase = (BuildingIconCaseHex)GetChildAt(5);
            StateIcon = (BuildingDescStateIcon)GetChildAt(6);
            DeleteBtn = (GButton)GetChildAt(7);
            UpgradeBtn = (GButton)GetChildAt(8);
            RebuildBtn = (GButton)GetChildAt(9);
            MoveBtn = (GButton)GetChildAt(10);
            OpenCloseSwitchBtn = (GButton)GetChildAt(11);
            NextBuilding = (GButton)GetChildAt(12);
            LastBuilding = (GButton)GetChildAt(13);
            BuildingDescGroup = (GGroup)GetChildAt(14);
        }
    }
}