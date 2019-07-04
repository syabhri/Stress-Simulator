using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatInput : MonoBehaviour
{
    public string code;
    public GameEvent EventToRise;
    char[] charArray;
    int i = 0;

    private void Start()
    {
        charArray = code.ToCharArray();
    }

    // Update is called once per frame
    void Update()
    {
        ListenCode();
    }

    public void ListenCode()
    {
        if (i >= charArray.Length)
        {
            Debug.Log("cheat Activated");
            EventToRise.Raise();
            i = 0;
        }
        else
        {
            foreach (var c in Input.inputString)
            {
                if (c == charArray[i])
                {
                    //Debug.Log(charArray[i].ToString());
                    i++;
                }
                else
                {
                    i = 0;
                }
            }
        }
    }
}
