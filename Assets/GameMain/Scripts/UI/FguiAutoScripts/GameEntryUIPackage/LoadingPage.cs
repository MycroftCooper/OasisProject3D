/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameEntryUIPackage
{
    public partial class LoadingPage : GComponent
    {
        public GImage BackGround;
        public GImage Title;
        public ProgressBar1 ProgressBar;
        public GTextField LoadingText;
        public const string URL = "ui://awx3ckgtlzb4fa";

        public static LoadingPage CreateInstance()
        {
            return (LoadingPage)UIPackage.CreateObject("GameEntryUIPackage", "LoadingPage");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            BackGround = (GImage)GetChildAt(0);
            Title = (GImage)GetChildAt(1);
            ProgressBar = (ProgressBar1)GetChildAt(2);
            LoadingText = (GTextField)GetChildAt(3);
        }
    }
}