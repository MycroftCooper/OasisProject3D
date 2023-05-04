/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class BuildingDescBuildCostInfoBox : GComponent
    {
        public Controller BuildInfoCtrl;
        public GTextField Title;
        public GList ResLest;
        public LeftTimeProgressBar BuildProgressBar;
        public LeftTimeProgressBar2 UpgradeProgressBar;
        public const string URL = "ui://t09fsbe0qo0z4r";

        public static BuildingDescBuildCostInfoBox CreateInstance()
        {
            return (BuildingDescBuildCostInfoBox)UIPackage.CreateObject("GameMainUIPackage", "BuildingDescBuildCostInfoBox");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            BuildInfoCtrl = GetControllerAt(0);
            Title = (GTextField)GetChildAt(2);
            ResLest = (GList)GetChildAt(3);
            BuildProgressBar = (LeftTimeProgressBar)GetChildAt(4);
            UpgradeProgressBar = (LeftTimeProgressBar2)GetChildAt(5);
        }
    }
}