using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class EmptyInputEvent : MonoBehaviour
{
    TMP_InputField textMesh;

    public UnityEvent OnEmpty;
    public UnityEvent OnExist;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TMP_InputField>();
        checkText();
    }

    public void checkText()
    {
        if (textMesh.text == string.Empty)
        {
            OnEmpty.Invoke();
            Debug.Log("empty");
        }
        else
        {
            OnExist.Invoke();
            Debug.Log("not empty");
        }
    }
}
