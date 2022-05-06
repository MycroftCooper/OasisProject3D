using Borodar.FarlandSkies.Core.DotParams;
using Borodar.FarlandSkies.Core.Editor;
using Borodar.FarlandSkies.Core.Games.Collections;

using UnityEditor;
using UnityEngine;

namespace Borodar.FarlandSkies.LowPoly {
    [CustomEditor(typeof(SkyboxDayNightCycle))]
    public class SkyboxDayNightCycleEditor : Editor {
        public const string TOOLTIP_SKY = "List of sky colors, based on time of day. Each list item contains “time” filed that should be specified in percents (0 - 100)";
        public const string TOOLTIP_STARS = "Allows you to manage stars tint color over time. Each list item contains “time” filed that should be specified in percents (0 - 100)";
        public const string TOOLTIP_SUN = "Sun appearance and light params depending on time of day. Each list item contains “time” filed that should be specified in percents (0 - 100)";
        public const string TOOLTIP_MOON = "Moon appearance and light params depending on time of day. Each list item contains “time” filed that should be specified in percents (0 - 100)";
        public const string TOOLTIP_CLOUDS = "Allows you to manage clouds tint color over time. Each list item contains “time” filed that should be specified in percents (0 - 100)";

        private const float LIST_CONTROLS_PAD = 20f;
        private const float TIME_WIDTH = BaseParamDrawer.TIME_FIELD_WIDHT + LIST_CONTROLS_PAD;

        // Sky
        private SerializedProperty _skyDotParams;
        // Stars
        private SerializedProperty _starsDotParams;
        // Sun
        private SerializedProperty _sunrise;
        private SerializedProperty _sunset;
        private SerializedProperty _sunAltitude;
        private SerializedProperty _sunLongitude;
        private SerializedProperty _sunOrbit;
        private SerializedProperty _sunDotParams;
        // Moon
        private SerializedProperty _moonrise;
        private SerializedProperty _moonset;
        private SerializedProperty _moonAltitude;
        private SerializedProperty _moonLongitude;
        private SerializedProperty _moonOrbit;
        private SerializedProperty _moonDotParams;
        // Clouds
        private SerializedProperty _cloudsDotParams;
        // General
        private SerializedProperty _framesInterval;

        private static bool _showSkyDotParams;
        private static bool _showStarsDotParams;
        private static bool _showSunDotParams;
        private static bool _showMoonDotParams;
        private static bool _showCloudsDotParams;

        private GUIContent _guiContent;
        // Labels
        private GUIContent _skyParamsLabel;
        private GUIContent _starsParamsLabel;
        private GUIContent _sunParamsLabel;
        private GUIContent _moonParamsLabel;
        private GUIContent _cloudsParamsLabel;
        private GUIContent _framesIntervalLabel;
        // Icons
        private GUIContent _starsIcon;
        private GUIContent _skyIcon;
        private GUIContent _sunIcon;
        private GUIContent _moonIcon;
        private GUIContent _cloudsIcon;
        private GUIContent _generalIcon;

        //---------------------------------------------------------------------
        // Messages
        //---------------------------------------------------------------------

        protected void OnEnable() {
            _guiContent = new GUIContent();
            _skyParamsLabel = new GUIContent("Sky Dot Params", TOOLTIP_SKY);
            _starsParamsLabel = new GUIContent("Stars Dot Params", TOOLTIP_STARS);
            _sunParamsLabel = new GUIContent("Sun Dot Params", TOOLTIP_SUN);
            _moonParamsLabel = new GUIContent("Moon Dot Params", TOOLTIP_MOON);
            _cloudsParamsLabel = new GUIContent("Clouds Dot Params", TOOLTIP_CLOUDS);
            _framesIntervalLabel = new GUIContent("Frames Interval", "Reduce the skybox day-night cycle update to run every \"n\" frames");

            // Sky
            _skyDotParams = serializedObject.FindProperty("_skyParamsList").FindPropertyRelative("Params");
            // Stars
            _starsDotParams = serializedObject.FindProperty("_starsParamsList").FindPropertyRelative("Params");
            // Sun
            _sunrise = serializedObject.FindProperty("_sunrise");
            _sunset = serializedObject.FindProperty("_sunset");
            _sunAltitude = serializedObject.FindProperty("_sunAltitude");
            _sunLongitude = serializedObject.FindProperty("_sunLongitude");
            _sunOrbit = serializedObject.FindProperty("_sunOrbit");
            _sunDotParams = serializedObject.FindProperty("_sunParamsList").FindPropertyRelative("Params");
            // Moon
            _moonrise = serializedObject.FindProperty("_moonrise");
            _moonset = serializedObject.FindProperty("_moonset");
            _moonAltitude = serializedObject.FindProperty("_moonAltitude");
            _moonLongitude = serializedObject.FindProperty("_moonLongitude");
            _moonOrbit = serializedObject.FindProperty("_moonOrbit");
            _moonDotParams = serializedObject.FindProperty("_moonParamsList").FindPropertyRelative("Params");
            // Clouds
            _cloudsDotParams = serializedObject.FindProperty("_cloudsParamsList").FindPropertyRelative("Params");
            // General
            _framesInterval = serializedObject.FindProperty("_framesInterval");

            // Icons
            var starsTex = FarlandSkiesEditorUtility.LoadEditorIcon("Star_16.png");
            var skyTex = FarlandSkiesEditorUtility.LoadEditorIcon("Sky_16.png");
            var sunTex = FarlandSkiesEditorUtility.LoadEditorIcon("Sun_16.png");
            var moonTex = FarlandSkiesEditorUtility.LoadEditorIcon("Moon_16.png");
            var cloudsTex = FarlandSkiesEditorUtility.LoadEditorIcon("Cloud_16.png");
            var generalTex = FarlandSkiesEditorUtility.LoadEditorIcon("Tag_16.png");

            _starsIcon = new GUIContent(starsTex);
            _skyIcon = new GUIContent(skyTex);
            _sunIcon = new GUIContent(sunTex);
            _moonIcon = new GUIContent(moonTex);
            _cloudsIcon = new GUIContent(cloudsTex);
            _generalIcon = new GUIContent(generalTex);
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            CustomGUILayout();
            serializedObject.ApplyModifiedProperties();
        }

        //---------------------------------------------------------------------
        // Helpers
        //---------------------------------------------------------------------

        private void CustomGUILayout() {
            var skyboxController = SkyboxController.Instance;
            if (skyboxController == null) {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox("This component requires the SkyboxController instance to be present in the scene. Please add the SkyboxController prefab to your scene.", MessageType.Error);
                return;
            }

            // Settings            
            SkyGUILayout();
            StarsGUILayout(skyboxController.StarsEnabled);
            SunGUILayout(skyboxController.SunEnabled);
            MoonGUILayout(skyboxController.MoonEnabled);
            CloudsGUILayout(skyboxController.CloudsEnabled);
            GeneralGUILayout();
        }

        private void GeneralGUILayout() {
            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.LabelField(_generalIcon, GUILayout.Width(16f));
            EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();

            _framesInterval.intValue = EditorGUILayout.IntSlider(_framesIntervalLabel, _framesInterval.intValue, 1, 60);
        }

        private void SkyGUILayout() {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.LabelField(_skyIcon, GUILayout.Width(16f));
            EditorGUILayout.LabelField("Sky", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();

            _showSkyDotParams = EditorGUILayout.Foldout(_showSkyDotParams, _skyParamsLabel);
            EditorGUILayout.Space();
            if (_showSkyDotParams) {
                SkyParamsHeader();
                ReorderableListGUI.ListField(_skyDotParams);
            }
        }

        private void StarsGUILayout(bool starsEnabled) {
            GUI.enabled = starsEnabled;

            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.LabelField(_starsIcon, GUILayout.Width(16f));
            EditorGUILayout.LabelField("Stars", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();

            if (starsEnabled) {
                _showStarsDotParams = EditorGUILayout.Foldout(_showStarsDotParams, _starsParamsLabel);
                EditorGUILayout.Space();
                if (_showStarsDotParams) {
                    StarsParamsHeader();
                    ReorderableListGUI.ListField(_starsDotParams);
                }
            } else {
                EditorGUILayout.Space();
            }
        }

        private void SunGUILayout(bool sunEnabled) {
            GUI.enabled = sunEnabled;

            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.LabelField(_sunIcon, GUILayout.Width(16f));
            EditorGUILayout.LabelField("Sun", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();

            if (sunEnabled) {
                EditorGUILayout.PropertyField(_sunrise);
                EditorGUILayout.PropertyField(_sunset);
                EditorGUILayout.PropertyField(_sunAltitude);
                EditorGUILayout.PropertyField(_sunLongitude);
                EditorGUILayout.PropertyField(_sunOrbit);

                _showSunDotParams = EditorGUILayout.Foldout(_showSunDotParams, _sunParamsLabel);
                EditorGUILayout.Space();
                if (_showSunDotParams) {
                    CelestialsParamsHeader();
                    ReorderableListGUI.ListField(_sunDotParams);
                }
            } else {
                EditorGUILayout.Space();
            }
        }

        private void MoonGUILayout(bool moonEnabled) {
            GUI.enabled = moonEnabled;

            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.LabelField(_moonIcon, GUILayout.Width(16f));
            EditorGUILayout.LabelField("Moon", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();

            if (moonEnabled) {
                EditorGUILayout.PropertyField(_moonrise);
                EditorGUILayout.PropertyField(_moonset);
                EditorGUILayout.PropertyField(_moonAltitude);
                EditorGUILayout.PropertyField(_moonLongitude);
                EditorGUILayout.PropertyField(_moonOrbit);

                _showMoonDotParams = EditorGUILayout.Foldout(_showMoonDotParams, _moonParamsLabel);
                EditorGUILayout.Space();
                if (_showMoonDotParams) {
                    CelestialsParamsHeader();
                    ReorderableListGUI.ListField(_moonDotParams);
                }
            } else {
                EditorGUILayout.Space();
            }
        }

        private void CloudsGUILayout(bool cloudsEnabled) {
            GUI.enabled = cloudsEnabled;

            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.LabelField(_cloudsIcon, GUILayout.Width(16f));
            EditorGUILayout.LabelField("Clouds", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();

            if (cloudsEnabled) {
                _showCloudsDotParams = EditorGUILayout.Foldout(_showCloudsDotParams, _cloudsParamsLabel);
                EditorGUILayout.Space();
                if (_showCloudsDotParams) {
                    CloudsParamsHeader();
                    ReorderableListGUI.ListField(_cloudsDotParams);
                }
            } else {
                EditorGUILayout.Space();
            }
        }

        private void SkyParamsHeader() {
            var position = GUILayoutUtility.GetRect(_guiContent, ReorderableListStyles.Instance.Title);
            if (Event.current.type == EventType.Repaint) {
                var baseWidht = position.width;
                // Time
                position.width = TIME_WIDTH;
                _guiContent.text = "Time";
                ReorderableListStyles.Instance.Title.Draw(position, _guiContent, false, false, false, false);
                // Top Color
                position.x += position.width;
                position.width = (baseWidht - position.width) / 3f - LIST_CONTROLS_PAD + BaseParamDrawer.H_PAD;
                _guiContent.text = "Top Color";
                ReorderableListStyles.Instance.Title.Draw(position, _guiContent, false, false, false, false);
                // Middle Color
                position.x += position.width;
                _guiContent.text = "Middle Color";
                ReorderableListStyles.Instance.Title.Draw(position, _guiContent, false, false, false, false);
                // Bottom Color
                position.x += position.width;
                position.width += LIST_CONTROLS_PAD + BaseParamDrawer.H_PAD;
                _guiContent.text = "Bottom Color";
                ReorderableListStyles.Instance.Title.Draw(position, _guiContent, false, false, false, false);
            }
            GUILayout.Space(-1);
        }

        private void StarsParamsHeader() {
            var position = GUILayoutUtility.GetRect(_guiContent, ReorderableListStyles.Instance.Title);
            if (Event.current.type == EventType.Repaint) {
                var baseWidht = position.width;
                // Time
                position.width = TIME_WIDTH;
                _guiContent.text = "Time";
                ReorderableListStyles.Instance.Title.Draw(position, _guiContent, false, false, false, false);
                // Tint Color
                position.x += position.width;
                position.width = baseWidht - position.width;
                _guiContent.text = "Tint Color";
                ReorderableListStyles.Instance.Title.Draw(position, _guiContent, false, false, false, false);
            }
            GUILayout.Space(-1);
        }

        private void CelestialsParamsHeader() {
            var position = GUILayoutUtility.GetRect(_guiContent, ReorderableListStyles.Instance.Title);
            if (Event.current.type == EventType.Repaint) {
                var baseWidht = position.width;
                var baseHeight = position.height;
                // Time
                position.width = TIME_WIDTH;
                position.height *= 2f;
                _guiContent.text = "Time";
                ReorderableListStyles.Instance.Title.Draw(position, _guiContent, false, false, false, false);
                // Tint Color
                position.x += position.width;
                position.width = (baseWidht - position.width - 2f * LIST_CONTROLS_PAD) / 2f + BaseParamDrawer.H_PAD;
                position.height = baseHeight;
                _guiContent.text = "Tint Color";
                ReorderableListStyles.Instance.Title.Draw(position, _guiContent, false, false, false, false);
                // Light Color
                position.x += position.width;
                position.width += LIST_CONTROLS_PAD;
                _guiContent.text = "Light Color";
                ReorderableListStyles.Instance.Title.Draw(position, _guiContent, false, false, false, false);
            }
            GUILayout.Space(-5f);
            position = GUILayoutUtility.GetRect(_guiContent, ReorderableListStyles.Instance.Title);
            if (Event.current.type == EventType.Repaint) {
                // Light Intencity
                position.x += TIME_WIDTH;
                position.width -= TIME_WIDTH;
                _guiContent.text = "Light Intencity";
                ReorderableListStyles.Instance.Title.Draw(position, _guiContent, false, false, false, false);
            }
            GUILayout.Space(-1);
        }

        private void CloudsParamsHeader() {
            var position = GUILayoutUtility.GetRect(_guiContent, ReorderableListStyles.Instance.Title);
            if (Event.current.type == EventType.Repaint) {
                var baseWidht = position.width;
                // Time
                position.width = TIME_WIDTH;
                _guiContent.text = "Time";
                ReorderableListStyles.Instance.Title.Draw(position, _guiContent, false, false, false, false);
                // Tint Color
                position.x += position.width;
                position.width = baseWidht - position.width;
                _guiContent.text = "Tint Color";
                ReorderableListStyles.Instance.Title.Draw(position, _guiContent, false, false, false, false);
            }
            GUILayout.Space(-1);
        }
    }
}