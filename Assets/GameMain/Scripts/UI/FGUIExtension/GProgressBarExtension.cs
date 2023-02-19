using FairyGUI;

namespace OasisProject3D.UI.FGUIExtension
{
    public static class GProgressBarExtension
    {
        public static void SetMaxValue(this GProgressBar bar, float max)
        {
            if (bar == null)
                return;

            bar.max = max;
        }

        public static void SetValue(this GProgressBar bar, float value)
        {
            if (bar == null)
                return;

            bar.value = value;
        }

        public static void SetMinValue(this GProgressBar bar, float min)
        {
            if (bar == null)
                return;

            bar.min = min;
        }
    }
}
