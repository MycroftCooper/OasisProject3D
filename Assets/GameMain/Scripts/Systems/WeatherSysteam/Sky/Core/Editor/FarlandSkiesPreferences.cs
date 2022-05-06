using System;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEngine;

namespace Borodar.FarlandSkies.Core.Editor
{
    public static class FarlandSkiesPreferences
    {
        private const string HOME_FOLDER_PREF_KEY = "Borodar.FarlandSkies.HomeFolder.";
        private const string HOME_FOLDER_DEFAULT = "Assets/Farland Skies";
        private const string HOME_FOLDER_HINT = "Change this setting to the new location of the \"Farland Skies\" folder if you move it around in your project.";

        private static readonly EditorPrefsString HOME_FOLDER_PREF;

        public static string HomeFolder;

        static FarlandSkiesPreferences()
        {
            HOME_FOLDER_PREF = new EditorPrefsString(HOME_FOLDER_PREF_KEY + ProjectName, "Folder Location", HOME_FOLDER_DEFAULT);
            HomeFolder = HOME_FOLDER_PREF.Value;
        }

        //---------------------------------------------------------------------
        // Messages
        //---------------------------------------------------------------------

        [SettingsProvider]
        public static SettingsProvider CreateSettingProvider()
        {
            return new SettingsProvider("Borodar/FarlandSkies", SettingsScope.Project)
            {
                label = "Farland Skies",
                guiHandler = (searchContext) =>
                {
                    EditorGUILayout.HelpBox(HOME_FOLDER_HINT, MessageType.Info);
                    EditorGUILayout.Separator();
                    HOME_FOLDER_PREF.Draw();
                    HomeFolder = HOME_FOLDER_PREF.Value;
                },
            };
        }

        //---------------------------------------------------------------------
        // Helpers
        //---------------------------------------------------------------------

        private static string ProjectName
        {
            get
            {
                var s = Application.dataPath.Split('/');
                var p = s[s.Length - 2];
                return p;
            }
        }

        //---------------------------------------------------------------------
        // Nested
        //---------------------------------------------------------------------

        private abstract class EditorPrefsItem<T>
        {
            protected readonly string Key;
            protected readonly string Label;
            protected readonly T DefaultValue;

            protected EditorPrefsItem(string key, string label, T defaultValue)
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("key");
                }

                Key = key;
                Label = label;
                DefaultValue = defaultValue;
            }

            [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
            public abstract T Value { get; set; }

            public abstract void Draw();

            public static implicit operator T(EditorPrefsItem<T> s)
            {
                return s.Value;
            }
        }

        private class EditorPrefsString : EditorPrefsItem<string>
        {
            public EditorPrefsString(string key, string label, string defaultValue)
                : base(key, label, defaultValue) { }

            public override string Value
            {
                get { return EditorPrefs.GetString(Key, DefaultValue); }
                set { EditorPrefs.SetString(Key, value); }
            }

            public override void Draw()
            {
                EditorGUIUtility.labelWidth = 100f;
                Value = EditorGUILayout.TextField(Label, Value);
            }
        }
    }
}