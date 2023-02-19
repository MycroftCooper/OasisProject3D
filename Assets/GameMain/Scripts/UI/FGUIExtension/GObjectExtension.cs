using FairyGUI;
using System;

namespace OasisProject3D.UI.FGUIExtension
{
    public static class GObjectExtension
    {
        public static void SetContent(this GObject obj, string text)
        {
            if (obj == null)
                return;

            obj.text = "";      // 先设为空一次，防效果错误
            obj.text = text;
        }

        public static void SetOnClick(this GObject obj, Action callback)
        {
            if (obj == null)
                return;

            obj.onClick.Add(() => { callback(); });
        }

        public static void SetOnClick<T>(this GObject obj, Action<T> callback)
        {
            if (obj == null)
                return;

            obj.onClick.Add((context) =>
            {
                if (context.data == default || !(context.data is T))
                    return;

                var data = (T)context.data;
                callback(data);
            });
        }
    }
}
