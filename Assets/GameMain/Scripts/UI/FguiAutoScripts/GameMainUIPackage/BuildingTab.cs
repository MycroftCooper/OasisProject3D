/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class BuildingTab : GComponent
    {
        public Controller TabSelected;
        public GImage BuildingListBackground;
        public GList BuildingCaseList;
        public BuildingTypeBtn BuildingTabAnyBtn;
        public BuildingTypeBtn BuildingTabFunctionBtn;
        public BuildingTypeBtn BuildingTabEcoBtn;
        public BuildingTypeBtn BuildingTabProductBtn;
        public BuildingTypeBtn BuildingTabStorageBtn;
        public GGroup BuildingTypeBtnList;
        public const string URL = "ui://t09fsbe0jlch1r";

        public static BuildingTab CreateInstance()
        {
            return (BuildingTab)UIPackage.CreateObject("GameMainUIPackage", "BuildingTab");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            TabSelected = GetControllerAt(0);
            BuildingListBackground = (GImage)GetChildAt(0);
            BuildingCaseList = (GList)GetChildAt(1);
            BuildingTabAnyBtn = (BuildingTypeBtn)GetChildAt(2);
            BuildingTabFunctionBtn = (BuildingTypeBtn)GetChildAt(3);
            BuildingTabEcoBtn = (BuildingTypeBtn)GetChildAt(4);
            BuildingTabProductBtn = (BuildingTypeBtn)GetChildAt(5);
            BuildingTabStorageBtn = (BuildingTypeBtn)GetChildAt(6);
            BuildingTypeBtnList = (GGroup)GetChildAt(7);
        }
    }
}