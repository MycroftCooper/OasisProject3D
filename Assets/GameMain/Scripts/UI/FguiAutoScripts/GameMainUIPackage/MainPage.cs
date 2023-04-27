/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class MainPage : GComponent
    {
        public Controller BuildingListCtrl;
        public Controller SettingPanelCtrl;
        public BuildingTab BuildingList;
        public GButton BuildBtn;
        public OptionMenuPage OptionMenuPage;
        public GImage TopBarBackground;
        public GProgressBar GreenRateBar;
        public GTextField PeopleNum;
        public GTextField SaplingNum;
        public GTextField WaterNum;
        public GTextField WoodNum;
        public GButton Speed1XBtn;
        public GButton Speed2XBtn;
        public GButton Speed3XBtn;
        public GButton WeatherBtn;
        public GButton SettingBtn;
        public GGroup TopBar;
        public const string URL = "ui://t09fsbe0jlch1k";

        public static MainPage CreateInstance()
        {
            return (MainPage)UIPackage.CreateObject("GameMainUIPackage", "MainPage");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            BuildingListCtrl = GetControllerAt(0);
            SettingPanelCtrl = GetControllerAt(1);
            BuildingList = (BuildingTab)GetChildAt(0);
            BuildBtn = (GButton)GetChildAt(1);
            OptionMenuPage = (OptionMenuPage)GetChildAt(2);
            TopBarBackground = (GImage)GetChildAt(3);
            GreenRateBar = (GProgressBar)GetChildAt(4);
            PeopleNum = (GTextField)GetChildAt(9);
            SaplingNum = (GTextField)GetChildAt(10);
            WaterNum = (GTextField)GetChildAt(11);
            WoodNum = (GTextField)GetChildAt(12);
            Speed1XBtn = (GButton)GetChildAt(13);
            Speed2XBtn = (GButton)GetChildAt(14);
            Speed3XBtn = (GButton)GetChildAt(15);
            WeatherBtn = (GButton)GetChildAt(16);
            SettingBtn = (GButton)GetChildAt(17);
            TopBar = (GGroup)GetChildAt(18);
        }
    }
}