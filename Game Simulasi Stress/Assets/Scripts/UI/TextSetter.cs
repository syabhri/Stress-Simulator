using UnityEngine;
using TMPro;


[RequireComponent(typeof(TextMeshProUGUI))]
public class TextSetter : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    public StringContainer Text;
    public bool Continues = false;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        SetText(Text);

        if (Continues)
            Text.OnValueChanged += SetText;
    }

    private void OnDestroy()
    {
        if (Continues)
            Text.OnValueChanged -= SetText;
    }

    public void SetText(StringContainer text)
    {
        textMeshPro.SetText(text.Value);
    }
}
