/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class ResIconProgressBar : GProgressBar
    {
        public Controller ColorCtrl;
        public Controller ModeCtrl;
        public GLoader ResIcon;
        public GTextField Speed;
        public const string URL = "ui://t09fsbe0qo0z4t";

        public static ResIconProgressBar CreateInstance()
        {
            return (ResIconProgressBar)UIPackage.CreateObject("GameMainUIPackage", "ResIconProgressBar");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            ColorCtrl = GetControllerAt(0);
            ModeCtrl = GetControllerAt(1);
            ResIcon = (GLoader)GetChildAt(3);
            Speed = (GTextField)GetChildAt(5);
        }
    }
}