using UnityEngine;
using UnityEngine.Events;

public class CheatInput : MonoBehaviour
{
    public string code;
    public UnityEvent OnCodeCorrect;
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
            OnCodeCorrect.Invoke();
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
