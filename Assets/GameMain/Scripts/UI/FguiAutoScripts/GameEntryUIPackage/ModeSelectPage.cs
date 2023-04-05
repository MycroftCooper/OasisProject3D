/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameEntryUIPackage
{
    public partial class ModeSelectPage : GComponent
    {
        public Controller state;
        public GGraph TabPageBackground;
        public GTextField title;
        public GButton TabBtn2;
        public GButton TabBtn1;
        public GGroup Tutorial;
        public GGroup CommonMode;
        public const string URL = "ui://awx3ckgtjlchic";

        public static ModeSelectPage CreateInstance()
        {
            return (ModeSelectPage)UIPackage.CreateObject("GameEntryUIPackage", "ModeSelectPage");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            state = GetControllerAt(0);
            TabPageBackground = (GGraph)GetChildAt(0);
            title = (GTextField)GetChildAt(1);
            TabBtn2 = (GButton)GetChildAt(2);
            TabBtn1 = (GButton)GetChildAt(3);
            Tutorial = (GGroup)GetChildAt(7);
            CommonMode = (GGroup)GetChildAt(11);
        }
    }
}