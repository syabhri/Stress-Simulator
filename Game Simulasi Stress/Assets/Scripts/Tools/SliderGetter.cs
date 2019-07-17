using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderGetter : MonoBehaviour
{
    public Slider Slider;
    public FloatVariable Variable;

    private void Start()
    {
        if (Slider == null)
        {
            Slider = GetComponent<Slider>();
        }
    }

    public void GetValue()
    {
        Variable.value = Slider.value;
    }
}
