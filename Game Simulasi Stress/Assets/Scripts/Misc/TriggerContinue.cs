using UnityEngine;
using System.Collections;

public class TriggerContinue : MonoBehaviour
{
    public GameObject text1;
    public GameObject text2;

    public void triggerContinue()
    {
        gameObject.SetActive(false);
        text1.SetActive(true);
        text2.SetActive(true);
    }
    public void stopAnimation()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }
}
