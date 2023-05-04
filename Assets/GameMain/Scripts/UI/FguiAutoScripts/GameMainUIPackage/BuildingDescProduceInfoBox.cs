/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OasisProject3D.UI.GameMainUIPackage
{
    public partial class BuildingDescProduceInfoBox : GComponent
    {
        public GTextField Title;
        public GList ProduceResList;
        public GList ConsumeResList;
        public const string URL = "ui://t09fsbe0qo0z4x";

        public static BuildingDescProduceInfoBox CreateInstance()
        {
            return (BuildingDescProduceInfoBox)UIPackage.CreateObject("GameMainUIPackage", "BuildingDescProduceInfoBox");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Title = (GTextField)GetChildAt(2);
            ProduceResList = (GList)GetChildAt(3);
            ConsumeResList = (GList)GetChildAt(5);
        }
    }
}