/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameEntryUIPackage
{
    public partial class TitlePage : GComponent
    {
        public Controller IsSelectBtn;
        public GGraph TabPageBackground;
        public GImage TabBtnBackground;
        public GButton TitleBtn;
        public GGroup TabHead;
        public GButton StartBtn;
        public GButton ContinueBtn;
        public GButton SettingBtn;
        public GButton AboutBtn;
        public GButton ExitBtn;
        public GGroup TabBtns;
        public SettingPage Setting;
        public ModeSelectPage ModeSelect;
        public const string URL = "ui://awx3ckgtaspm0";

        public static TitlePage CreateInstance()
        {
            return (TitlePage)UIPackage.CreateObject("GameEntryUIPackage", "TitlePage");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            IsSelectBtn = GetControllerAt(0);
            TabPageBackground = (GGraph)GetChildAt(1);
            TabBtnBackground = (GImage)GetChildAt(2);
            TitleBtn = (GButton)GetChildAt(3);
            TabHead = (GGroup)GetChildAt(4);
            StartBtn = (GButton)GetChildAt(5);
            ContinueBtn = (GButton)GetChildAt(6);
            SettingBtn = (GButton)GetChildAt(7);
            AboutBtn = (GButton)GetChildAt(8);
            ExitBtn = (GButton)GetChildAt(9);
            TabBtns = (GGroup)GetChildAt(10);
            Setting = (SettingPage)GetChildAt(18);
            ModeSelect = (ModeSelectPage)GetChildAt(19);
        }
    }
}