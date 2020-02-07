using UnityEditor;

[CustomEditor(typeof(VariableContainer<>) , true)]
public class VariableContainerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("It Works");
    }
}
