/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class Unit_SettingWindow : GComponent
    {
        public GImage Background;
        public GButton SettingBtn;
        public GButton CameraBtn;
        public const string URL = "ui://t09fsbe0jlch1u";

        public static Unit_SettingWindow CreateInstance()
        {
            return (Unit_SettingWindow)UIPackage.CreateObject("GameMainUIPackage", "Unit_SettingWindow");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Background = (GImage)GetChildAt(0);
            SettingBtn = (GButton)GetChildAt(1);
            CameraBtn = (GButton)GetChildAt(2);
        }
    }
}