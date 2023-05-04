/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class LeftTimeProgressBar : GProgressBar
    {
        public GTextField LeftTime;
        public const string URL = "ui://t09fsbe0qo0z4v";

        public static LeftTimeProgressBar CreateInstance()
        {
            return (LeftTimeProgressBar)UIPackage.CreateObject("GameMainUIPackage", "LeftTimeProgressBar");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            LeftTime = (GTextField)GetChildAt(3);
        }
    }
}