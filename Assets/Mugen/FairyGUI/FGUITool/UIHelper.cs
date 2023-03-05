using FairyGUI;
using Mugen;

namespace Medium.FGUI
{
    public static partial class UIHelper
    {
        private static bool _initPackageItemExtensions = false;
        public static void InitPackageItemExtensions()
        {
            if (_initPackageItemExtensions)
                return;

            _initPackageItemExtensions = true;

            var types = Reflect.GetAllTypes();

            foreach (var type in types)
            {
                if (!type.IsClass || type.IsAbstract || !typeof(GObject).IsAssignableFrom(type))
                    continue;

                var attributes = type.GetCustomAttributes(typeof(UIPackageItemExtension), false);
                for (int i = 0; i < attributes.Length; i++)
                {
                    var attribute = (UIPackageItemExtension)attributes[i];
                    UIObjectFactory.SetPackageItemExtension(attribute.url, type);
                }
            }
        }

        private static char[] transitionPathseparator = new char[] { '\\', '/' };
        public static Transition GetTransitionFullPath(this GComponent component, string path)
        {
            if (component == null || path == null || path.Length <= 0)
                return null;

            var names = path.Split(transitionPathseparator);
            if (names.Length == 1)
                return component.GetTransition(names[0]);
            else
            {
                var temp = component;
                for (int i = 0; i < names.Length && temp != null; i++)
                {
                    if (i == names.Length - 1)
                        return temp.GetTransition(names[i]);
                    else
                        temp = component.GetChildByPath(names[i])?.asCom;
                }

                return null;
            }
        }
    }
}
