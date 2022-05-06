
using Borodar.FarlandSkies.Core.DotParams;
using UnityEditor;
using UnityEngine;

namespace Borodar.FarlandSkies.LowPoly.DotParams
{
    [CustomPropertyDrawer(typeof(StarsParam))]
    public class StarsParamDrawer : BaseParamDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);
            // Tint Color
            position.x += TIME_FIELD_WIDHT;
            position.y += V_PAD;
            position.width -= TIME_FIELD_WIDHT;
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("TintColor"), GUIContent.none);
        }
    }
}