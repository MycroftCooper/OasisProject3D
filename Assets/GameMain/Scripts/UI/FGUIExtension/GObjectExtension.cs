using System;

namespace FairyGUI.Extension {
    public static class GObjectExtension {
        public static void SetContent(this GObject obj, string text) {
            if (obj == null)
                return;

            obj.text = text;
        }

        public static void SetContent<T>(this GObject obj, T value) {
            if (obj == null)
                return;

            obj.text = value.ToString();
        }

        public static void SetOnClick(this GObject obj, Action callback) {
            obj?.onClick.Add(() => { callback(); });
        }

        public static void SetOnClick<T>(this GObject obj, Action<T> callback) {
            obj?.onClick.Add((context) => {
                if (context.data is not T data)
                    return;

                callback(data);
            });
        }

        public static void SetVisible(this GObject obj, bool value) {
            if (obj != null)
                obj.visible = value;
        }

        /// <summary>
        /// 设置灰态+点击穿透
        /// </summary>
        public static void SetEnabled(this GObject obj, bool value) {
            if (obj != null)
                obj.enabled = value;
        }

        /// <summary>
        /// 设置灰态
        /// </summary>
        public static void SetGrayed(this GObject obj, bool value) {
            if (obj != null)
                obj.grayed = value;
        }
    }
}
