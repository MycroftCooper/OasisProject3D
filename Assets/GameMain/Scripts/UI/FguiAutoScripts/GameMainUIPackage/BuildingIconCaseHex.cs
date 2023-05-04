/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class BuildingIconCaseHex : GComponent
    {
        public Controller BuildingIconCaseStateCtrl;
        public const string URL = "ui://t09fsbe0qo0z4n";

        public static BuildingIconCaseHex CreateInstance()
        {
            return (BuildingIconCaseHex)UIPackage.CreateObject("GameMainUIPackage", "BuildingIconCaseHex");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            BuildingIconCaseStateCtrl = GetControllerAt(0);
        }
    }
}