/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class MainPage : GComponent
    {
        public Controller BuildingListCtrl;
        public Controller SettingPanelCtrl;
        public ConfirmWindow SettingPanel;
        public GImage TopBarBackground;
        public GTextField ElectricityNum;
        public GTextField SaplingNum;
        public GTextField WaterNum;
        public GTextField WoodNum;
        public GButton Speed1XBtn;
        public GButton Speed2XBtn;
        public GButton Speed3XBtn;
        public GProgressBar GreenRateBar;
        public GButton WeatherBtn;
        public GButton SettingBtn;
        public BuildingTab BuildingList;
        public GButton BuildBtn;
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
            SettingPanel = (ConfirmWindow)GetChildAt(0);
            TopBarBackground = (GImage)GetChildAt(1);
            ElectricityNum = (GTextField)GetChildAt(6);
            SaplingNum = (GTextField)GetChildAt(7);
            WaterNum = (GTextField)GetChildAt(8);
            WoodNum = (GTextField)GetChildAt(9);
            Speed1XBtn = (GButton)GetChildAt(10);
            Speed2XBtn = (GButton)GetChildAt(11);
            Speed3XBtn = (GButton)GetChildAt(12);
            GreenRateBar = (GProgressBar)GetChildAt(13);
            WeatherBtn = (GButton)GetChildAt(14);
            SettingBtn = (GButton)GetChildAt(15);
            BuildingList = (BuildingTab)GetChildAt(16);
            BuildBtn = (GButton)GetChildAt(17);
        }
    }
}