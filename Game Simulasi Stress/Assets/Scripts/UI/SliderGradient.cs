using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderGradient : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private void Start()
    {
        setGradient();

        slider.onValueChanged.AddListener(delegate { setGradient(); });
    }

    public void setGradient()
    {
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
