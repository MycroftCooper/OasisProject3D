/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameEntryUIPackage
{
    public partial class ProgressBar1 : GProgressBar
    {
        public Transition t0;
        public const string URL = "ui://awx3ckgti4mbfl";

        public static ProgressBar1 CreateInstance()
        {
            return (ProgressBar1)UIPackage.CreateObject("GameEntryUIPackage", "ProgressBar1");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            t0 = GetTransitionAt(0);
        }
    }
}