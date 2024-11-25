using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(ButtonEvent))]
public class ButtonEventEditor : Editor
{
    SerializedProperty hoverEnterProp;
    SerializedProperty hoverExitProp;

    void OnEnable()
    {
    }

    public override void OnInspectorGUI()
    {
        ButtonEvent example = (ButtonEvent)target;
        var buttonType = example.GetComponent<CustomButtonScrite>();

        // �⺻ Button�� �ν����� UI ǥ��
        base.OnInspectorGUI();

        // HoverEnter �̺�Ʈ �ʵ带 �ν����Ϳ� ǥ��
        EditorGUILayout.Space();

        // ���� ���� ����
        serializedObject.ApplyModifiedProperties();
    }
}