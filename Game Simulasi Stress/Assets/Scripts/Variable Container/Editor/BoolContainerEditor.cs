using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoolContainer))]
public class BoolContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        BoolContainer m_Target = target as BoolContainer;
        if (GUILayout.Button("Update Changes"))
            m_Target.OnValueChanged.Invoke(m_Target);
    }
}
