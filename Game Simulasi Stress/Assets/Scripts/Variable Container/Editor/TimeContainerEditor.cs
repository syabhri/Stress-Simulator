using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TimeContainer))]
public class TimeContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        TimeContainer m_Target = target as TimeContainer;
        if (GUILayout.Button("Update Changes"))
            m_Target.OnValueChanged.Invoke(m_Target);
    }
}
