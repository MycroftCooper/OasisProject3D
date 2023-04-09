/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class BuildingTypeBtn : GButton
    {
        public Controller TypeCtrl;
        public const string URL = "ui://t09fsbe0jlch1s";

        public static BuildingTypeBtn CreateInstance()
        {
            return (BuildingTypeBtn)UIPackage.CreateObject("GameMainUIPackage", "BuildingTypeBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            TypeCtrl = GetControllerAt(1);
        }
    }
}