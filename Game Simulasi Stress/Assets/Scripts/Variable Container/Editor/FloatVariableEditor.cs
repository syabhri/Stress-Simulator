using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FloatVariable))]
public class FloatVariableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        FloatVariable m_Target = (FloatVariable)target;

        Undo.RecordObject(m_Target, "Changed Value");

        EditorGUILayout.LabelField("Developer Descriptions");
        m_Target.DeveloperDescription = EditorGUILayout.TextArea(m_Target.DeveloperDescription);

        m_Target.SetValue(EditorGUILayout.FloatField("Value", m_Target.value));
    }
}
