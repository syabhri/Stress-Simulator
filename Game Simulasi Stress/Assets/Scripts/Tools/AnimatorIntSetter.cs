﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorIntSetter : MonoBehaviour
{
    [Tooltip("Integer to read from and send to the Animator as the specified parameter.")]
    public IntContainer Int;

    [Tooltip("Animator to set parameters on.")]
    public Animator Animator;

    [Tooltip("Name of the parameter to set with the value of Integer.")]
    public string ParameterName;

    [Tooltip("Only Set Animator Parameter Once At Enabled")]
    public bool setOnce = true;

    /// <summary>
    /// Animator Hash of ParameterName, automatically generated.
    /// </summary>
    [SerializeField] private int parameterHash;

    private void OnValidate()
    {
        parameterHash = Animator.StringToHash(ParameterName);
    }

    private void OnEnable()
    {
        if (setOnce) SetInteger(Int);
    }

    private void Start()
    {
        if (!setOnce)
        {
            SetInteger(Int);
            Int.OnValueChanged += SetInteger;
        }
    }

    private void OnDestroy()
    {
        if (!setOnce) Int.OnValueChanged -= SetInteger;
    }

    public void SetInteger(IntContainer container)
    {
        Animator.SetInteger(parameterHash, container.Value);
    }
}
