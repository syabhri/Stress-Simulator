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
    public float min;
    public float max;

    private void Update()
    {
            Slider.value = Mathf.Clamp01(
                Mathf.InverseLerp(min, max, Variable.value));
    }
}
