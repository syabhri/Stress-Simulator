// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
//
// Modified By : Muhammad syabhri Mustafa
// Date        : 29/05/19
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SliderSetter : MonoBehaviour
{
    public Slider slider;
    public FloatVariable variable;
    [SerializeField]
    [Tooltip("continuesly update on Value Changes, Enable only if slider is used as output otherwise will couse feedback loop")]
    private bool isContinues = false;

    private void Start()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }

        if (isContinues)
        {
            variable.OnValueChange += UpdateChanges;
        }
    }

    private void OnEnable()
    {
        slider.value = variable.value;
    }

    private void OnDestroy()
    {
        if (isContinues)
        {
            variable.OnValueChange -= UpdateChanges;
        }
    }

    public void UpdateChanges(FloatVariable variable)
    {
        slider.value = variable.value;
    }
            
}
