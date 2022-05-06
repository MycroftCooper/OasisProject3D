
using Borodar.FarlandSkies.Core.DotParams;
using UnityEditor;
using UnityEngine;

namespace Borodar.FarlandSkies.LowPoly.DotParams
{
    [CustomPropertyDrawer(typeof(CelestialParam))]
    public class CelestialParamDrawer : BaseParamDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 2f * EditorGUIUtility.singleLineHeight + 4f * V_PAD;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);
            // Tint Color
            position.x += TIME_FIELD_WIDHT;
            position.y += 1.5f * V_PAD;
            var baseX = position.x;
            var baseWidth = position.width;
            position.width = (position.width - TIME_FIELD_WIDHT) / 2f - 0.5f * H_PAD;
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("TintColor"), GUIContent.none);
            // Light Color
            position.x += position.width + H_PAD;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("LightColor"), GUIContent.none);
            // Light Intencity
            position.x = baseX;
            position.y += position.height + 2f * V_PAD;
            position.width = baseWidth - TIME_FIELD_WIDHT;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("LightIntencity"), GUIContent.none);
        }
    }
}