using UnityEngine;
using TMPro;

public class DropdownSetter : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public StringListVariable Options;
    public IntVariable Value;
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
            Value.OnValueChange += UpdateChanges;
        }
    }

    private void OnEnable()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(Options.Values);
        UpdateChanges(Value);
    }

    private void OnDestroy()
    {
        if (IsContinues)
        {
            Value.OnValueChange -= UpdateChanges;
        }
    }

    public void UpdateChanges(IntVariable Value)
    {
        dropdown.value = Value.value;
        dropdown.RefreshShownValue();
    }
}
