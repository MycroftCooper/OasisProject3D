/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameEntryUIPackage
{
    public partial class Unit_SettingComboBoxItem : GButton
    {
        public Controller isChecked;
        public const string URL = "ui://awx3ckgti4mbfp";

        public static Unit_SettingComboBoxItem CreateInstance()
        {
            return (Unit_SettingComboBoxItem)UIPackage.CreateObject("GameEntryUIPackage", "Unit_SettingComboBoxItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            isChecked = GetControllerAt(1);
        }
    }
}