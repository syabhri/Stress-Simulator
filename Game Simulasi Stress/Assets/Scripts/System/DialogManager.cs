using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [Header("Properties")]
    public float typingDelay = .01f;
    public float punctuationDelay = .5f;
    [SerializeField]
    private List<char> punctuation = new List<char>();
    [Tooltip("replace words that match in \"local\" with \"external\" from the dilaog")]
    public List<StringPair> replace = new List<StringPair>();

    [Header("UI Output")]
    public StringVariable nameText;
    public StringVariable dialogueText;
    //public Sprite avatarSprite;

    [Header("Reference")]
    public ThingRuntimeSet decisionPanel;

    [Header("Condition")]
    public BoolVariable IsDialogOpen;
    public BoolVariable IsDecisionOpen;

    [Header("Event")]
    public GameEvent OnDecisionStart;
    public GameEvent OnPlayerMove;
    public GameEvent OnPlayerStop;

    [Header("Data Passer")]
    public Dialogue dialoguePasser;

    //temp data container
    private Queue<string> sentences;
    private Queue<Speaker> speakers;
    private  Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        speakers = new Queue<Speaker>();
    }
    private void Update()
    {
        //listen submit only when dilaog panel is open but decision panel is closed
        if (Input.GetButtonDown("Submit"))
            if (IsDialogOpen.value && !IsDecisionOpen.value)
            {
                DisplayNextSentences();
                Debug.Log("Displaying next sentence....");
            }
    }

    private void OnEnable()
    {
        //reset decision and dialog panel
        IsDialogOpen.value = false;
        IsDecisionOpen.value = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("DialogStarted");
        this.dialogue = dialogue;

        OnPlayerStop.Raise();

        IsDialogOpen.value = true;

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
            if (dialogue.nextDialog.Length != 0 || dialogue.doActivities.Length != 0)
                StartDecision();
            else
                Invoke("EndDialogue", .1f);
            return;
        }

        Speaker speaker = speakers.Dequeue();
        nameText.Value = speaker.name;
        //avatarSprite = speaker.avatar;
        sentences.Clear();

        foreach (string sentence in speaker.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentences();
    }

    public void DisplayNextSentences()
    {
        if (sentences.Count == 0)
        {
            DisplayNextSpeaker();
            return;
        }

        string sentence = sentences.Dequeue();

        sentence = ReplaceText(sentence);

        StopAllCoroutines();

        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.Value = "";
        foreach(char letter in sentence.ToCharArray())
        {
            if (punctuation.Contains(letter))
            {
                dialogueText.Value += letter;
                yield return new WaitForSeconds(punctuationDelay);
            }
            else
            {
                dialogueText.Value += letter;
                yield return new WaitForSeconds(typingDelay);
            }
        }
    }

    public string ReplaceText(string sentence)
    {
        foreach (StringPair pair in replace)
        {
            if (sentence.Contains(pair.local))
            {
                sentence = sentence.Replace(pair.local, pair.external.Value);
                Debug.Log("Text Replaced : " + pair.local + " -> " + pair.external.Value);
            }
        }
        return sentence;
    }

    public void EndDialogue()
    {
        OnPlayerMove.Raise();
        IsDialogOpen.value = false;
        Debug.Log("Dialog Ended");
    }

    public void StartDecision()
    {
        Debug.Log("Decision Started");
        foreach (GameObject thing in decisionPanel.Items)
        {
            thing.SetActive(true);
        }
        IsDecisionOpen.value = true;
        dialoguePasser = dialogue;
        OnDecisionStart.Raise();
    }

    public void EndDecision()
    {
        IsDecisionOpen.value = false;
        foreach (GameObject thing in decisionPanel.Items)
        {
            thing.SetActive(false);
        }
        Debug.Log("Decision Ended");
    }
}
