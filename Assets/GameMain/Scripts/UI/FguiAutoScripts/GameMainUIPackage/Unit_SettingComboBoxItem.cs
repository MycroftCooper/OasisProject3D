/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class Unit_SettingComboBoxItem : GButton
    {
        public Controller isChecked;
        public const string URL = "ui://t09fsbe0jtrv3q";

        public static Unit_SettingComboBoxItem CreateInstance()
        {
            return (Unit_SettingComboBoxItem)UIPackage.CreateObject("GameMainUIPackage", "Unit_SettingComboBoxItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            isChecked = GetControllerAt(1);
        }
    }
}