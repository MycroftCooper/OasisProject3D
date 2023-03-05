using System;

namespace FairyGUI.Extension {
    public static class GListExtension {
        public static void SetItemRenderer<T>(this GList list, Action<int, T> callback) where T : GObject {
            if (list == null)
                return;

            list.itemRenderer = (index, obj) => {
                if (obj is not T item)
                    return;

                callback(index, item);
            };
        }

        public static void SetOnClickItem<T>(this GList list, Action<T> callback) {
            list?.onClickItem.Add((context) => {
                if (context.data is not T data)
                    return;

                callback(data);
            });
        }

        public static void SetItemNum(this GList list, int num) {
            if (list == null)
                return;

            list.numItems = num;
        }

        public static GObject GetItem(this GList list, int index) {
            if (list == null)
                return null;

            var cIndex = list.ItemIndexToChildIndex(index);
            return list.GetChildAt(cIndex);
        }

        public static void ScrollTop(this GList list) {
            list?.scrollPane?.ScrollTop();
        }
    }
}
