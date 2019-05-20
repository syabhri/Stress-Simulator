using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [Header("UI Output")]
    public StringVariable nameText;
    public StringVariable dialogueText;
    public SpriteContainer avatarSprite;

    [Header("Reference")]
    public MovementController PlayerMovement;
    public ThingRuntimeSet decisionPanel;

    [Header("Condition")]
    public BoolVariable IsDialogOpen;
    public BoolVariable IsDecisionOpen;

    [Header("Event")]
    public GameEvent OnDecisionStart;

    [Header("Data Passer")]
    public Dialogue dialoguePasser;

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
        //Debug.Log(IsDialogOpen.value + " " + !IsDecisionOpen.value);
        if (Input.GetButtonDown("Submit") && IsDialogOpen.value && !IsDecisionOpen.value)
        {
            DisplayNextSentences();
        }
    }

    private void OnEnable()
    {
        IsDialogOpen.value = false;
        IsDecisionOpen.value = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("DialogStarted");
        this.dialogue = dialogue;

        PlayerMovement.enabled = false;

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
        avatarSprite.sprite = speaker.avatar;
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
        StopAllCoroutines();

        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.Value = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.Value += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        Debug.Log("enddialogstart");
        PlayerMovement.enabled = true;
        IsDialogOpen.value = false;
        Debug.Log("Dialog Ended");
    }

    public void StartDecision()
    {
        Debug.Log("Decision Started");
        foreach (Thing thing in decisionPanel.Items)
        {
            thing.gameObject.SetActive(true);
        }
        IsDecisionOpen.value = true;
        dialoguePasser = dialogue;
        OnDecisionStart.Raise();
    }

    public void EndDecision()
    {
        IsDecisionOpen.value = false;
        foreach (Thing thing in decisionPanel.Items)
        {
            thing.gameObject.SetActive(false);
        }
        Debug.Log("Decision Ended");
    }
}
