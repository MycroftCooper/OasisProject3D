/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class BuildingDescTab : GComponent
    {
        public Controller buildingStateCtrl;
        public GTextField buildingName;
        public GLoader buildingIcon;
        public GTextField buildingDesc;
        public GButton lastBuilding;
        public GButton nextBuilding;
        public GButton buildingStateIcon;
        public const string URL = "ui://t09fsbe0jobk20";

        public static BuildingDescTab CreateInstance()
        {
            return (BuildingDescTab)UIPackage.CreateObject("GameMainUIPackage", "BuildingDescTab");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            buildingStateCtrl = GetControllerAt(0);
            buildingName = (GTextField)GetChildAt(2);
            buildingIcon = (GLoader)GetChildAt(3);
            buildingDesc = (GTextField)GetChildAt(4);
            lastBuilding = (GButton)GetChildAt(5);
            nextBuilding = (GButton)GetChildAt(6);
            buildingStateIcon = (GButton)GetChildAt(7);
        }
    }
}