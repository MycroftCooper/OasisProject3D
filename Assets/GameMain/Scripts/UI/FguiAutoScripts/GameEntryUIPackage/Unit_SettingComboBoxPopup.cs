/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameEntryUIPackage
{
    public partial class Unit_SettingComboBoxPopup : GComponent
    {
        public GList list;
        public const string URL = "ui://awx3ckgti4mbfq";

        public static Unit_SettingComboBoxPopup CreateInstance()
        {
            return (Unit_SettingComboBoxPopup)UIPackage.CreateObject("GameEntryUIPackage", "Unit_SettingComboBoxPopup");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            list = (GList)GetChildAt(1);
        }
    }
}