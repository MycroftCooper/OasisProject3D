/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class Unit_BuildComp : GComponent
    {
        public Controller tab;
        public Unit_Btn1 BuildTabBtn1;
        public Unit_Btn1 BuildTabBtn2;
        public Unit_Btn1 BuildTabBtn3;
        public GList BuildList;
        public const string URL = "ui://t09fsbe0jlch1r";

        public static Unit_BuildComp CreateInstance()
        {
            return (Unit_BuildComp)UIPackage.CreateObject("GameMainUIPackage", "Unit_BuildComp");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            tab = GetControllerAt(0);
            BuildTabBtn1 = (Unit_Btn1)GetChildAt(1);
            BuildTabBtn2 = (Unit_Btn1)GetChildAt(2);
            BuildTabBtn3 = (Unit_Btn1)GetChildAt(3);
            BuildList = (GList)GetChildAt(4);
        }
    }
}