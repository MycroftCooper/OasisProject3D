/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class BuildingTab : GComponent
    {
        public Controller TabSelected;
        public GImage BuildingListBackground;
        public GList BuildList;
        public BuildingTypeBtn BuildingTabAllBtn;
        public BuildingTypeBtn BuildingTabFunctionBtn;
        public BuildingTypeBtn BuildingEcoTabBtn;
        public BuildingTypeBtn BuildingTabProductionBtn;
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
            BuildList = (GList)GetChildAt(1);
            BuildingTabAllBtn = (BuildingTypeBtn)GetChildAt(2);
            BuildingTabFunctionBtn = (BuildingTypeBtn)GetChildAt(3);
            BuildingEcoTabBtn = (BuildingTypeBtn)GetChildAt(4);
            BuildingTabProductionBtn = (BuildingTypeBtn)GetChildAt(5);
            BuildingTabStorageBtn = (BuildingTypeBtn)GetChildAt(6);
            BuildingTypeBtnList = (GGroup)GetChildAt(7);
        }
    }
}