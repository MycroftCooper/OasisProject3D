/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class SettingPage : GComponent
    {
        public Controller state;
        public GGraph TabPageBackground;
        public GButton TabBtn1;
        public GButton TabBtn2;
        public GButton TabBtn3;
        public GButton TabBtn4;
        public GTextField title;
        public GComboBox PictureQualityCombo;
        public GComboBox AntiAliasingCombo;
        public GComboBox MeshQualityCombo;
        public GComboBox VSyncCombo;
        public GComboBox UISizeCombo;
        public GComboBox ScreenResolutionCombo;
        public GComboBox ScreenModeCombo;
        public GComboBox PostProcessCombo;
        public GComboBox TerrainDetailCombo;
        public GGroup ViewSetting;
        public GSlider slider1;
        public GSlider slider2;
        public GSlider slider3;
        public GGroup AudioSetting;
        public GButton CtrlBtn1;
        public GButton ResetBtn1;
        public GButton CtrlBtn2;
        public GButton ResetBtn2;
        public GButton CtrlBtn3;
        public GButton ResetBtn3;
        public GButton CtrlBtn4;
        public GButton ResetBtn4;
        public GButton CtrlBtn5;
        public GButton ResetBtn5;
        public GButton CtrlBtn6;
        public GButton ResetBtn6;
        public GButton CtrlBtn7;
        public GButton ResetBtn7;
        public GButton CtrlBtn8;
        public GButton ResetBtn8;
        public GGroup ControlSetting;
        public const string URL = "ui://t09fsbe0jtrv2u";

        public static SettingPage CreateInstance()
        {
            return (SettingPage)UIPackage.CreateObject("GameMainUIPackage", "SettingPage");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            state = GetControllerAt(0);
            TabPageBackground = (GGraph)GetChildAt(0);
            TabBtn1 = (GButton)GetChildAt(1);
            TabBtn2 = (GButton)GetChildAt(2);
            TabBtn3 = (GButton)GetChildAt(3);
            TabBtn4 = (GButton)GetChildAt(4);
            title = (GTextField)GetChildAt(5);
            PictureQualityCombo = (GComboBox)GetChildAt(16);
            AntiAliasingCombo = (GComboBox)GetChildAt(17);
            MeshQualityCombo = (GComboBox)GetChildAt(18);
            VSyncCombo = (GComboBox)GetChildAt(19);
            UISizeCombo = (GComboBox)GetChildAt(20);
            ScreenResolutionCombo = (GComboBox)GetChildAt(21);
            ScreenModeCombo = (GComboBox)GetChildAt(22);
            PostProcessCombo = (GComboBox)GetChildAt(23);
            TerrainDetailCombo = (GComboBox)GetChildAt(24);
            ViewSetting = (GGroup)GetChildAt(25);
            slider1 = (GSlider)GetChildAt(26);
            slider2 = (GSlider)GetChildAt(28);
            slider3 = (GSlider)GetChildAt(30);
            AudioSetting = (GGroup)GetChildAt(32);
            CtrlBtn1 = (GButton)GetChildAt(43);
            ResetBtn1 = (GButton)GetChildAt(44);
            CtrlBtn2 = (GButton)GetChildAt(45);
            ResetBtn2 = (GButton)GetChildAt(46);
            CtrlBtn3 = (GButton)GetChildAt(47);
            ResetBtn3 = (GButton)GetChildAt(48);
            CtrlBtn4 = (GButton)GetChildAt(49);
            ResetBtn4 = (GButton)GetChildAt(50);
            CtrlBtn5 = (GButton)GetChildAt(51);
            ResetBtn5 = (GButton)GetChildAt(52);
            CtrlBtn6 = (GButton)GetChildAt(53);
            ResetBtn6 = (GButton)GetChildAt(54);
            CtrlBtn7 = (GButton)GetChildAt(55);
            ResetBtn7 = (GButton)GetChildAt(56);
            CtrlBtn8 = (GButton)GetChildAt(57);
            ResetBtn8 = (GButton)GetChildAt(58);
            ControlSetting = (GGroup)GetChildAt(59);
        }
    }
}