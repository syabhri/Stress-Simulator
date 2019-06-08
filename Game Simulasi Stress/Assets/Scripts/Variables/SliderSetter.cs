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
    public Slider Slider;
    public FloatVariable Variable;
    [Tooltip("normilize variable 1-100 to range 0-1")]
    public bool normalize;

    private void Update()
    {
        if (Slider != null && Variable != null)
            if (normalize)
                Slider.value = Variable.value / 100;
            else
                Slider.value = Variable.value;
    }
}
