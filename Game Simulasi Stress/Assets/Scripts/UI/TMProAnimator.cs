using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMProAnimator : MonoBehaviour
{
    public TextMeshProUGUI target;
    public float delay;
    public List<string> Text;

    private void Start()
    {
        StartCoroutine(CycleText());
    }

    IEnumerator CycleText()
    {
        while (true)
        {
            foreach (string text in Text)
            {
                target.SetText(text);
                yield return new WaitForSeconds(delay);
            }
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
