using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountdown : MonoBehaviour
{
    public float timestart = 10;
    public Text CountdownText;

    // Start is called before the first frame update
    void Start()
    {
        CountdownText.text = timestart.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        if (CountdownText.enabled)
        {
            timestart -= Time.deltaTime;
            CountdownText.text = timestart.ToString("0");
        }
    }
}
