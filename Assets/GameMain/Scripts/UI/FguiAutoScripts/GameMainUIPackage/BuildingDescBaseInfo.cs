/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class BuildingDescBaseInfo : GComponent
    {
        public GTextField Desc;
        public GTextField Name;
        public const string URL = "ui://t09fsbe0qo0z4q";

        public static BuildingDescBaseInfo CreateInstance()
        {
            return (BuildingDescBaseInfo)UIPackage.CreateObject("GameMainUIPackage", "BuildingDescBaseInfo");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Desc = (GTextField)GetChildAt(1);
            Name = (GTextField)GetChildAt(3);
        }
    }
}