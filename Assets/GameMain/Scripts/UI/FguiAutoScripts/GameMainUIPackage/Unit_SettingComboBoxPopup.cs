/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class Unit_SettingComboBoxPopup : GComponent
    {
        public GList list;
        public const string URL = "ui://t09fsbe0jtrv3o";

        public static Unit_SettingComboBoxPopup CreateInstance()
        {
            return (Unit_SettingComboBoxPopup)UIPackage.CreateObject("GameMainUIPackage", "Unit_SettingComboBoxPopup");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            list = (GList)GetChildAt(1);
        }
    }
}