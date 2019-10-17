using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("Properties")]
    public float typingDelay = .01f;
    public float punctuationDelay = .5f;
    [SerializeField]
    private List<char> punctuation = new List<char>();
    [Tooltip("replace words that match in \"local\" with \"external\" from the dilaog")]
    public List<StringPair> replace = new List<StringPair>();

    [Header("GUI")]
    public StringVariable nameText;
    public StringVariable dialogueText;
    //public Sprite avatarSprite;

    [Header("Reference")]
    //public ThingRuntimeSet decisionPanel;
    public ThingRuntimeSet decisionPanel;
    public ThingRuntimeSet ActivityManager;

    [Header("Event")]
    public UnityEvent OnDialogStart;
    public UnityEvent OnDialogEnd;
    public UnityEvent OnTypeSentenceFinish;

    [Header("Conditions")]
    public BoolVariable isDialogOpen;

    [Header("Data Passer")]
    public StringVariable ButtonTextPasser;
    public TimeContainer CurrentTime;

    //temp data container
    private Queue<string> sentences;
    private Queue<Speaker> speakers;
    private  Dialogue dialogue;

    //temp Reference
    private ButtonGenerator buttonGenerator;
    private ActivityManager activityManager;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        speakers = new Queue<Speaker>();

        buttonGenerator = decisionPanel.Item.GetComponent<ButtonGenerator>();
        activityManager = ActivityManager.Item.GetComponent<ActivityManager>();
    }
    private void Update()
    {
        // Replaced By New Input Event Component
        //listen submit only when dilaog panel is open but decision panel is closed
        /*if (Input.GetButtonDown("Submit"))
            if (IsDialogOpen.value && !IsDecisionOpen.value)
            {
                DisplayNextSentences();
                Debug.Log("Displaying next sentence....");
            }*/
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("DialogStarted");
        isDialogOpen.value = true;
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
            if (!decisionPanel.Item.activeSelf)
            {
                if (dialogue.nextDialog.Length != 0 || dialogue.doActivities.Length != 0)
                    StartDecision();
                else
                    Invoke("EndDialogue", .1f);
            }
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
        isDialogOpen.value = false;
        OnDialogEnd.Invoke();
        Debug.Log("Dialog Ended");
    }

    public void StartDecision()
    {
        Debug.Log("Decision Started");
        decisionPanel.Item.SetActive(true);
        AssignNextDialogues();
        AssignActivities();

        ButtonTextPasser.Value = dialogue.dismisses;
        Button button = buttonGenerator.AssignButton();
        button.onClick.AddListener(delegate { EndDialogue(); } ); 
    }

    public void AssignNextDialogues()
    {
        foreach (Dialogue dialogue in dialogue.nextDialog)
        {
            ButtonTextPasser.Value = dialogue.response;
            Button button = buttonGenerator.AssignButton();
            button.onClick.AddListener(delegate { StartDialogue(dialogue); });
        }
    }

    public void AssignActivities()
    {
        foreach (Activity activity in dialogue.doActivities)
        {
            if (activity.isScheduled)
            {
                if (TimeManager.currentDay == activity.schedule.days &&
                    TimeManager.currentHours >= activity.schedule.hours &&
                    TimeManager.currentMinutes >= activity.schedule.minutes &&
                    TimeManager.currentHours <= activity.tolerance.days &&
                    TimeManager.currentMinutes >= activity.tolerance.minutes)
                {
                    break;
                }
            }
            ButtonTextPasser.Value = activity.name;
            Button button = buttonGenerator.AssignButton();
            button.onClick.AddListener(delegate { activityManager.DoActivity(activity); });
            button.onClick.AddListener(delegate { EndDialogue(); });
        }
    }

    public void AssignChoice(Dialogue dialogue)
    {
        foreach (Dialogue.ChoiceEvent choiceEvent in dialogue.choiceEvent)
        {
            ButtonTextPasser.Value = choiceEvent.name;
            Button button = buttonGenerator.AssignButton();
            button.onClick.AddListener(delegate { choiceEvent.unityEvent.Invoke(); });
        }
    }
}
