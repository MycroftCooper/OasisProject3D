/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;
using OasisProject3D.UI.GameEntryUIPackage;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class MainPage : GComponent
    {
        public Controller BuildingListCtrl;
        public Controller SettingPanelCtrl;
        public GProgressBar GreenRateBar;
        public BuildingTab BuildingList;
        public GButton BuildBtn;
        public GImage TopBarBackground;
        public GTextField PeopleNum;
        public GTextField SaplingNum;
        public GTextField WaterNum;
        public GTextField WoodNum;
        public GButton Speed1XBtn;
        public GButton Speed2XBtn;
        public GButton Speed3XBtn;
        public GButton WeatherBtn;
        public GButton SettingBtn;
        public ConfirmWindow SettingPanel;
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
            GreenRateBar = (GProgressBar)GetChildAt(0);
            BuildingList = (BuildingTab)GetChildAt(1);
            BuildBtn = (GButton)GetChildAt(2);
            TopBarBackground = (GImage)GetChildAt(3);
            PeopleNum = (GTextField)GetChildAt(8);
            SaplingNum = (GTextField)GetChildAt(9);
            WaterNum = (GTextField)GetChildAt(10);
            WoodNum = (GTextField)GetChildAt(11);
            Speed1XBtn = (GButton)GetChildAt(12);
            Speed2XBtn = (GButton)GetChildAt(13);
            Speed3XBtn = (GButton)GetChildAt(14);
            WeatherBtn = (GButton)GetChildAt(15);
            SettingBtn = (GButton)GetChildAt(16);
            SettingPanel = (ConfirmWindow)GetChildAt(17);
        }
    }
}