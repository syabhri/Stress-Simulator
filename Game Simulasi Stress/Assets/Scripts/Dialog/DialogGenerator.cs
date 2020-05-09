using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class DialogGenerator : EditorWindow
{
    // Properties Field
    public TextAsset DialogList;
    public string FolderName;

    // Accessed DialogList Content
    private string[] row;
    private string[] col;

    // Accessed Path 
    private string mainPath;
    private string currentPath;

    // Data Parser
    private const char CommaCharacter = ',';
    private const char QuoteCharacter = '"';

    [MenuItem("Window/Dialog Generator")]
    public static void ShowWindow()
    {
        GetWindow<DialogGenerator>("Dialog Generator");
    }

    private void OnGUI()
    {
        DialogList = (TextAsset)EditorGUILayout.ObjectField("Dialog List", DialogList, typeof(TextAsset), false);

        FolderName = EditorGUILayout.TextField("Folder Name", FolderName);

        if (GUILayout.Button("Generate Dialog"))
        {
            if (DialogList != null)
            {
                GenerateDialog(false);
            }
        }

        if (GUILayout.Button("Reset Dialog"))
        {
            if (DialogList != null)
            {
                GenerateDialog(true);
            }
        }
    }

    public void SetDialogPath()
    {
        if (FolderName != string.Empty)
        {
            mainPath = "Assets/" + FolderName;

            if (!AssetDatabase.IsValidFolder(mainPath))
                AssetDatabase.CreateFolder("Assets", FolderName);

            Debug.Log("Dialogue Folder Set To : " + mainPath);
        }
        else
        {
            mainPath = "Assets";
            Debug.Log("Dialogue Folder Set To : " + mainPath);
        }
    }

    public void GetDialogList()
    {
        row = DialogList.text.Split('\n');
        Debug.Log("Dialog List Count : " + row.Length);
    }

    public string[] GetDialogData(string entry)
    {
        List<string> result = new List<string>();
        string context = string.Empty;
        bool isQuote = false;

        foreach (char ch in entry)
        {
            if (ch == QuoteCharacter)
            {
                if (isQuote)
                    isQuote = false;
                else
                    isQuote = true;
                continue;
            }

            if (!isQuote && ch == CommaCharacter)
            {
                result.Add(context);
                context = string.Empty;
                continue;
            }

            context += ch;
        }
        return result.ToArray();
    }

    public void GenerateDialog(bool reset)
    {
        // get list of dialog
        GetDialogList();

        // create folder for dialogue
        SetDialogPath();

        // generate dialog from dialog list
        for (int i = 1; i < row.Length - 1; i++)
        {
            // get dilaog data 
            col = GetDialogData(row[i]);
            Debug.Log(col);

            // create sub folder
            currentPath = mainPath + "/" + col[0];
            if (!AssetDatabase.IsValidFolder(currentPath))
                AssetDatabase.CreateFolder(mainPath, col[0]);
            Debug.Log("Dilaog Sub Folder Generated : " + currentPath);

            // create dialog file
            string filePath = currentPath + "/" + col[1] + ".asset";
            Dialogue dialogue = AssetDatabase.LoadAssetAtPath<Dialogue>(filePath);
            if (dialogue == null)
            {
                dialogue = CreateInstance<Dialogue>();
                AssetDatabase.CreateAsset(dialogue, filePath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                dialogue = AssetDatabase.LoadAssetAtPath<Dialogue>(filePath);
            }
            if (dialogue.speakers == null)
                dialogue.speakers = new List<Speaker>();
            Debug.Log("Dialog file generated : " + col[1]);

            // Reset Dialog
            if (reset == true)
            {
                dialogue.speakers.Clear();
                EditorUtility.SetDirty(dialogue);
                continue;
            }
                
            // Fill Dialog Information
            if (col[2] == string.Empty)
                continue;

            Speaker speaker = new Speaker(col[2]);
            List<string> sentence = new List<string>();

            speaker.avatar = (Sprite)AssetDatabase.LoadAllAssetRepresentationsAtPath(col[3]).FirstOrDefault(x => x.name == "potrait");

            for (int j = 4; j < col.Length; j++)
            {
                if (col[j] == string.Empty)
                    break;
                sentence.Add(col[j]);
            }

            speaker.sentences = sentence.ToArray();
            dialogue.speakers.Add(speaker);
            EditorUtility.SetDirty(dialogue);
        }
    }
}
