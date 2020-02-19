using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IntContainer))]
public class IntContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        IntContainer m_Target = target as IntContainer;
        if (GUILayout.Button("Update Changes"))
            m_Target.OnValueChanged.Invoke(m_Target);
        GUILayout.Label("Delegate Count : " + m_Target.OnValueChanged.GetInvocationList().Length.ToString());
    }
}
