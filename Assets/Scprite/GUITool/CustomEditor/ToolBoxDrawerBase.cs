using UnityEditor;
using UnityEngine;

namespace Custom.Editor.Drawers
{
    public abstract class ToolBoxDrawerBase : PropertyDrawer
    {
        public abstract PropertyCondition OnGuiValidate(SerializedProperty property);
        public abstract PropertyCondition OnGuiValidate(SerializedProperty property, CustomAttribute attribute);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // ��ȿ�� �˻翡 ���� ǥ�� ���� ����
            if (OnGuiValidate(property) == PropertyCondition.Valid)
            {
                EditorGUI.PropertyField(position, property, label,true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (OnGuiValidate(property) == PropertyCondition.Valid) ? EditorGUI.GetPropertyHeight(property) : 0;
        }
    }
}