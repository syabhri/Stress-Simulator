using UnityEngine;
using TMPro;

public class DropdownSetter : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public StringListContainer Options;
    public IntContainer Value;
    [SerializeField]
    [Tooltip("continuesly update on Value Changes, Enable only if dropdown is used as output otherwise will couse feedback loop")]
    private bool IsContinues = false;

    private void Awake()
    {
        if (dropdown == null)
        {
            dropdown = GetComponent<TMP_Dropdown>();
        }

        if (IsContinues)
        {
            Value.OnValueChanged += UpdateChanges;
        }
    }

    private void OnEnable()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(Options.Value);
        UpdateChanges(Value);
    }

    private void OnDestroy()
    {
        if (IsContinues)
        {
            Value.OnValueChanged -= UpdateChanges;
        }
    }

    public void UpdateChanges(IntContainer value)
    {
        dropdown.value = value.Value;
        dropdown.RefreshShownValue();
    }
}
