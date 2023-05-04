/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class BuildingDescStateIcon : GComponent
    {
        public Controller BuildingStateIconCtrl;
        public const string URL = "ui://t09fsbe0qo0z4p";

        public static BuildingDescStateIcon CreateInstance()
        {
            return (BuildingDescStateIcon)UIPackage.CreateObject("GameMainUIPackage", "BuildingDescStateIcon");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            BuildingStateIconCtrl = GetControllerAt(0);
        }
    }
}