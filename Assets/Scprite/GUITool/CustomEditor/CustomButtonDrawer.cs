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

        // 기본 Button의 인스펙터 UI 표시
        base.OnInspectorGUI();

        // HoverEnter 이벤트 필드를 인스펙터에 표시
        EditorGUILayout.Space();

        // 변경 사항 적용
        serializedObject.ApplyModifiedProperties();
    }
}