using UnityEngine;
using TMPro;

public class DropdownSetter : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public IntVariable SelectedOption;

    private void Start()
    {
        if (dropdown == null)
        {
            dropdown = GetComponent<TMP_Dropdown>();
        }
    }

    private void OnEnable()
    {
        dropdown.value = SelectedOption.value;
    }
}
