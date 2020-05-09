using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    #region Data Class
    //replace targeted string with external value
    [System.Serializable]
    public struct StringReplace
    {
        public string target;
        public StringContainer replacement;
    }
    #endregion

    #region Variables
    [Header("Properties")]
    public float typingDelay = .01f;
    public float punctuationDelay = .5f;
    public List<char> punctuation = new List<char>();
    [Tooltip("replace target string in the dilaog with value from external variable")]
    public List<StringReplace> replace = new List<StringReplace>();

    [Header("Activated UI")]
    public BoolContainer DialogPanel;
    public GameObjectContainer DecisionPanel;

    [Header("Output UI")]
    public StringContainer nameText;
    public StringContainer dialogueText;
    public GameObjectContainer CharacterPotrait;

    [Header("Conditions")]
    public BoolContainer IsSentenceReady;

    [Header("Event")]
    public UnityEvent OnDialogStart;
    public UnityEvent OnDialogEnd;

    //temp data container
    private Queue<string> sentences;
    private Queue<Speaker> speakers;
    private Dialogue dialogue;
    private string sentence;

    //temp Reference
    private ButtonGenerator buttonGenerator;
    private Image characterPotrait;
    #endregion

    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        speakers = new Queue<Speaker>();

        buttonGenerator = DecisionPanel.Value.GetComponent<ButtonGenerator>();
        characterPotrait = CharacterPotrait.Value.GetComponent<Image>();

        DialogPanel.Value = false;
    }
    /* Replaced By New Input Event Component
    private void Update()
    {
        
        //listen submit only when dilaog panel is open but decision panel is closed
        if (Input.GetButtonDown("Submit"))
            if (IsDialogOpen.value && !IsDecisionOpen.value)
            {
                DisplayNextSentences();
                Debug.Log("Displaying next sentence....");
            }
    }*/
    #endregion

    #region Dialog Functions
    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("DialogStarted");
        if (DialogPanel.Value)// prevent double trigger
            return;
        DialogPanel.Value = true;
        IsSentenceReady.Value = true;
        OnDialogStart.Invoke();

        this.dialogue = dialogue;

        speakers.Clear();

        foreach (Speaker speaker in dialogue.speakers)
        {
            speakers.Enqueue(speaker);
        }
        
        DisplayNextSpeaker();
    }

    public void DisplayNextSpeaker()
    {
        Debug.Log("Displaying Next Speaker");
        if (speakers.Count == 0)
        {
            if (!buttonGenerator.gameObject.activeSelf)
            {
                if (dialogue.choiceEvent.Count > 0)
                    StartDecision();
                else
                    EndDialogue();
            }
            return;
        }

        Speaker speaker = speakers.Dequeue();
        nameText.Value = speaker.name;

        if (speaker.avatar != null)
        {
            characterPotrait.sprite = speaker.avatar;
            characterPotrait.gameObject.SetActive(true);
        }
        else
        {
            characterPotrait.gameObject.SetActive(false);
        }

        sentences.Clear();

        foreach (string sentence in speaker.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentences();
    }

    public void DisplayNextSentences()
    {
        if (!IsSentenceReady.Value)
        {
            StopAllCoroutines();
            dialogueText.Value = sentence;
            IsSentenceReady.Value = true;
            return;
        }

        if (sentences.Count == 0)
        {
            DisplayNextSpeaker();
            return;
        }

        sentence = sentences.Dequeue();

        sentence = ReplaceText(sentence);

        StopAllCoroutines();

        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        IsSentenceReady.Value = false;
        dialogueText.Value = string.Empty;
        foreach(char letter in sentence.ToCharArray())
        {
            if (punctuation.Contains(letter))
            {
                dialogueText.Value += letter;
                yield return new WaitForSecondsRealtime(punctuationDelay);
            }
            else
            {
                dialogueText.Value += letter;
                yield return new WaitForSecondsRealtime(typingDelay);
            }
        }
        IsSentenceReady.Value = true;
    }

    public string ReplaceText(string sentence)
    {
        foreach (StringReplace stringReplace in replace)
        {
            if (sentence.Contains(stringReplace.target))
            {
                sentence = sentence.Replace(stringReplace.target, stringReplace.replacement.Value);
                Debug.Log("Text Replaced : " + stringReplace.target + " -> " + stringReplace.replacement.Value);
            }
        }
        return sentence;
    }

    public void EndDialogue()
    {
        nameText.Value = string.Empty;
        characterPotrait.gameObject.SetActive(false);
        dialogueText.Value = string.Empty;
        DialogPanel.Value = false;
        OnDialogEnd.Invoke();
        Debug.Log("Dialog Ended");
    }
    #endregion

    #region Decision Functions
    public void StartDecision()
    {
        Debug.Log("Decision Started");
        buttonGenerator.gameObject.SetActive(true);
        buttonGenerator.DefaultAction.AddListener(delegate { EndDialogue(); });

        AssignChoice(dialogue);
    }

    public void AssignChoice(Dialogue dialogue)
    {
        foreach (Dialogue.ChoiceEvent choiceEvent in dialogue.choiceEvent)
        {
            Button button = buttonGenerator.AssignButton(choiceEvent.name);
            button.onClick.AddListener(delegate { choiceEvent.unityEvent.Invoke(); });
        }
    }
    #endregion
}
