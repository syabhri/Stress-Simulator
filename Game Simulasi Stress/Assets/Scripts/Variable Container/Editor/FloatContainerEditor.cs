using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FloatContainer))]
public class FloatContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        FloatContainer m_Target = target as FloatContainer;
        if (GUILayout.Button("Update Changes"))
            m_Target.OnValueChanged.Invoke(m_Target);
    }
}
