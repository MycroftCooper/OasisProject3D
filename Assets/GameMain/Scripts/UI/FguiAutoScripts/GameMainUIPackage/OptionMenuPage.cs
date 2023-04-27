/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class OptionMenuPage : GComponent
    {
        public Controller IsSelectBtn;
        public GGraph TabPageBackground;
        public GImage TabBtnBackground;
        public GButton TitleBtn;
        public GGroup TabHead;
        public GButton ContinueBtn;
        public GButton SettingBtn;
        public GButton AboutBtn;
        public GButton ExitBtn;
        public GGroup TabBtns;
        public SettingPage Setting;
        public const string URL = "ui://t09fsbe0jtrv2m";

        public static OptionMenuPage CreateInstance()
        {
            return (OptionMenuPage)UIPackage.CreateObject("GameMainUIPackage", "OptionMenuPage");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            IsSelectBtn = GetControllerAt(0);
            TabPageBackground = (GGraph)GetChildAt(0);
            TabBtnBackground = (GImage)GetChildAt(1);
            TitleBtn = (GButton)GetChildAt(2);
            TabHead = (GGroup)GetChildAt(3);
            ContinueBtn = (GButton)GetChildAt(4);
            SettingBtn = (GButton)GetChildAt(5);
            AboutBtn = (GButton)GetChildAt(6);
            ExitBtn = (GButton)GetChildAt(7);
            TabBtns = (GGroup)GetChildAt(8);
            Setting = (SettingPage)GetChildAt(16);
        }
    }
}