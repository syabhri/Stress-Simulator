using UnityEngine;
using TMPro;


[RequireComponent(typeof(TextMeshProUGUI))]
public class TextSetter : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    public StringVariable Text;
    public bool Continues = false;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        SetText(Text);

        if (Continues)
            Text.OnValueChange += SetText;
    }

    private void OnDestroy()
    {
        if (Continues)
            Text.OnValueChange -= SetText;
    }

    public void SetText(StringVariable text)
    {
        textMeshPro.SetText(text.Value);
    }
}
