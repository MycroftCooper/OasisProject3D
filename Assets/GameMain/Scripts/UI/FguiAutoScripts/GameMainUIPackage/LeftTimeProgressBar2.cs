/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class LeftTimeProgressBar2 : GProgressBar
    {
        public GTextField LeftTime;
        public const string URL = "ui://t09fsbe0qo0z4w";

        public static LeftTimeProgressBar2 CreateInstance()
        {
            return (LeftTimeProgressBar2)UIPackage.CreateObject("GameMainUIPackage", "LeftTimeProgressBar2");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            LeftTime = (GTextField)GetChildAt(3);
        }
    }
}