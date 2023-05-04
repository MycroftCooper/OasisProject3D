/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class BuildingIconHex : GComponent
    {
        public GLoader BuildingIcon;
        public const string URL = "ui://t09fsbe0qo0z4o";

        public static BuildingIconHex CreateInstance()
        {
            return (BuildingIconHex)UIPackage.CreateObject("GameMainUIPackage", "BuildingIconHex");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            BuildingIcon = (GLoader)GetChildAt(1);
        }
    }
}