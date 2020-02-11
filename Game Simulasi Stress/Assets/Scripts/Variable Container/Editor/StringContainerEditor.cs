using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StringContainer))]
public class StringContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        StringContainer m_Target = target as StringContainer;
        if (GUILayout.Button("Update Changes"))
            m_Target.OnValueChanged.Invoke(m_Target);
    }
}
