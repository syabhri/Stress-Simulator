﻿// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
//
// Modified By : Syabhri Mustafa
// Date:    5/8/19
// ----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBoolSetter : MonoBehaviour
{
    [Tooltip("Bool to read from and send to the Animator as the specified parameter.")]
    public BoolContainer Bool;

    [Tooltip("Animator to set parameters on.")]
    public Animator Animator;

    [Tooltip("Name of the parameter to set with the value of Bool.")]
    public string ParameterName;

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
        UpdateChanges(Bool);

        Bool.OnValueChanged += UpdateChanges;
    }

    private void OnDisable()
    {
        Bool.OnValueChanged -= UpdateChanges;
        Bool.Value = false;
    }

    public void UpdateChanges(BoolContainer container)
    {
        Animator.SetBool(parameterHash, container.Value);
    }
}
