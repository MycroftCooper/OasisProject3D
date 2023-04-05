/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class Unit_Btn1 : GButton
    {
        public Controller type;
        public const string URL = "ui://t09fsbe0jlch1s";

        public static Unit_Btn1 CreateInstance()
        {
            return (Unit_Btn1)UIPackage.CreateObject("GameMainUIPackage", "Unit_Btn1");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            type = GetControllerAt(1);
        }
    }
}