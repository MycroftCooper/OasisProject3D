using FairyGUI;
using System;

namespace OasisProject3D.UI.FGUIExtension
{
    public static class GListExtension
    {
        public static void SetItemRenderer<T>(this GList list, Action<int, T> callback) where T : GObject
        {
            if (list == null)
                return;

            list.itemRenderer = (index, obj) =>
            {
                T item = obj as T;
                if (item == null)
                    return;

                callback(index, item);
            };
        }

        public static void SetOnClickItem<T>(this GList list, Action<T> callback)
        {
            if (list == null)
                return;

            list.onClickItem.Add((context) =>
            {
                if (!(context.data is T))
                    return;

                var data = (T)context.data;
                callback(data);
            });
        }

        public static void SetItemNum(this GList list, int num)
        {
            if (list == null)
                return;

            list.numItems = num;
        }

        public static GObject GetItem(this GList list, int index)
        {
            if (list == null)
                return null;

            var cIndex = list.ItemIndexToChildIndex(index);
            return list.GetChildAt(cIndex);
        }

        public static void ScrollTop(this GList list)
        {
            list?.scrollPane?.ScrollTop();
        }
    }
}
