/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class GameMainUI : GComponent
    {
        public Controller tab;
        public GButton SettingBtn;
        public GButton Speed2xBtn;
        public GButton Speed1xBtn;
        public GButton StopBtn;
        public GButton WeatherBtn;
        public GTextField PeopleNum;
        public GTextField SaplingNum;
        public GTextField WaterNum;
        public GTextField WoodNumm;
        public GButton BuildBtn;
        public GProgressBar GreenRateBar;
        public const string URL = "ui://t09fsbe0jlch1k";

        public static GameMainUI CreateInstance()
        {
            return (GameMainUI)UIPackage.CreateObject("GameMainUIPackage", "GameMainUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            tab = GetControllerAt(0);
            SettingBtn = (GButton)GetChildAt(2);
            Speed2xBtn = (GButton)GetChildAt(3);
            Speed1xBtn = (GButton)GetChildAt(4);
            StopBtn = (GButton)GetChildAt(5);
            WeatherBtn = (GButton)GetChildAt(6);
            PeopleNum = (GTextField)GetChildAt(11);
            SaplingNum = (GTextField)GetChildAt(12);
            WaterNum = (GTextField)GetChildAt(13);
            WoodNumm = (GTextField)GetChildAt(14);
            BuildBtn = (GButton)GetChildAt(15);
            GreenRateBar = (GProgressBar)GetChildAt(16);
        }
    }
}