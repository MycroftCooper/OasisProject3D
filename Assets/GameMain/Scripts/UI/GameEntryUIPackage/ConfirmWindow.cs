/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameEntryUIPackage
{
    public partial class ConfirmWindow : GComponent
    {
        public GImage Background;
        public GTextField TitleText;
        public GButton YesBtn;
        public GButton NoBtn;
        public const string URL = "ui://awx3ckgtlzb4f5";

        public static ConfirmWindow CreateInstance()
        {
            return (ConfirmWindow)UIPackage.CreateObject("GameEntryUIPackage", "ConfirmWindow");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Background = (GImage)GetChildAt(0);
            TitleText = (GTextField)GetChildAt(1);
            YesBtn = (GButton)GetChildAt(2);
            NoBtn = (GButton)GetChildAt(3);
        }
    }
}