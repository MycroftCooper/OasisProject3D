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
        public GGroup TabBtn;
        public GButton StartBtn;
        public GButton ContinueBtn;
        public GButton SettingBtn;
        public GButton AboutBtn;
        public GButton ExitBtn;
        public const string URL = "ui://awx3ckgtaspm0";

        public static TitlePage CreateInstance()
        {
            return (TitlePage)UIPackage.CreateObject("GameEntryUIPackage", "TitlePage");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            IsSelectBtn = GetControllerAt(0);
            TabPageBackground = (GGraph)GetChildAt(0);
            TabBtnBackground = (GImage)GetChildAt(1);
            TitleBtn = (GButton)GetChildAt(2);
            TabBtn = (GGroup)GetChildAt(3);
            StartBtn = (GButton)GetChildAt(4);
            ContinueBtn = (GButton)GetChildAt(5);
            SettingBtn = (GButton)GetChildAt(6);
            AboutBtn = (GButton)GetChildAt(7);
            ExitBtn = (GButton)GetChildAt(8);
        }
    }
}