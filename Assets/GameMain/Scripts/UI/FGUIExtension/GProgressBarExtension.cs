namespace FairyGUI.Extension {
    public static class GProgressBarExtension {
        public static void SetMaxValue(this GProgressBar bar, float max) {
            if (bar == null)
                return;

            bar.max = max;
        }

        public static void SetValue(this GProgressBar bar, float value) {
            if (bar == null)
                return;

            bar.value = value;
        }

        public static void SetMinValue(this GProgressBar bar, float min) {
            if (bar == null)
                return;

            bar.min = min;
        }

        public static void SetPercent(this GProgressBar bar, float percent) {
            if (bar == null)
                return;

            bar.max = 1;
            bar.value = percent;
        }

        public static void SetValue(this GProgressBar bar, float value, float max) {
            if (bar == null)
                return;

            bar.value = value;
            bar.max = max;
        }
    }
}
