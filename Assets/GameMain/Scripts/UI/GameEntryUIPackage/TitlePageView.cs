/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameEntryUIPackage
{
    public partial class TitlePageView : GComponent
    {
        public Controller IsSelectBtn;
        public GGraph TabPageBackground;
        public GImage TabBtnBackground;
        public GButton TitleBtn;
        public GButton StartBtn;
        public GButton ContinueBtn;
        public GButton SettingBtn;
        public GButton AboutBtn;
        public GButton ExitBtn;
        public GGroup BtnGroup;
        public GGroup TabBtn;
        public const string URL = "ui://awx3ckgtaspm0";

        public static TitlePageView CreateInstance()
        {
            return (TitlePageView)UIPackage.CreateObject("GameEntryUIPackage", "TitlePageView");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            IsSelectBtn = GetControllerAt(0);
            TabPageBackground = (GGraph)GetChildAt(0);
            TabBtnBackground = (GImage)GetChildAt(1);
            TitleBtn = (GButton)GetChildAt(2);
            StartBtn = (GButton)GetChildAt(3);
            ContinueBtn = (GButton)GetChildAt(4);
            SettingBtn = (GButton)GetChildAt(5);
            AboutBtn = (GButton)GetChildAt(6);
            ExitBtn = (GButton)GetChildAt(7);
            BtnGroup = (GGroup)GetChildAt(8);
            TabBtn = (GGroup)GetChildAt(9);
        }
    }
}