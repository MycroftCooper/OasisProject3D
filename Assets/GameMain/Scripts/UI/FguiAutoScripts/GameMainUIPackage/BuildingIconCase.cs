/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class BuildingIconCase : GButton
    {
        public GLoader BuildingIcon;
        public GTextField BuildingName;
        public const string URL = "ui://t09fsbe0jlch1t";

        public static BuildingIconCase CreateInstance()
        {
            return (BuildingIconCase)UIPackage.CreateObject("GameMainUIPackage", "BuildingIconCase");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            BuildingIcon = (GLoader)GetChildAt(0);
            BuildingName = (GTextField)GetChildAt(2);
        }
    }
}