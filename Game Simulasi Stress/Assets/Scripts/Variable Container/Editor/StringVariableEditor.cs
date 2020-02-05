using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StringVariable))]
public class StringVariableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        StringVariable m_Target = (StringVariable)target;

        Undo.RecordObject(m_Target, "Changed Value");
        m_Target.Value = EditorGUILayout.TextField("Value", m_Target.Value);
    }
}
