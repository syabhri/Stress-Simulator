using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatPair
{
    [Tooltip("Target is the external value to be chaged by effector using the operator")]
    public FloatVariable target;

    [Tooltip("effector is the value used to change target (not used if using external effector)")]
    public float effector;

    [Tooltip("Operation is the basic operator \"=+-*/\" used to change the target")]
    public string operation;

    [Header("Range"), Tooltip("Use Range To Limit the Operation Result")]
    public bool useRange = true;
    public float min = 0;
    public float max = 100;

    [Header("External Effector"), Tooltip("Use External Effector Instead of the effector value above (Optional)")]
    public bool useExternalEeffector;
    public FloatVariable externalEffector;

    // default constructor
    public FloatPair() { }

    // constructor
    public FloatPair(FloatVariable target, float effector, string operation)
    {
        this.target = target;
        this.effector = effector;
    }

    // counstructor tp be used if using external effector value
    public FloatPair(FloatVariable target, FloatVariable externalEffector, string operation)
    {
        this.target = target;
        this.externalEffector = externalEffector;
        useExternalEeffector = true;
    }

    // do a calculation between target and effector
    public bool DoOperation()
    {
        if (useExternalEeffector)
        {
            switch (operation)
            {
                case "=":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value = externalEffector.value, min, max);
                    else
                        target.value = externalEffector.value;
                    return true;
                case "+":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value += externalEffector.value, min, max);
                    else
                        target.value += externalEffector.value;
                    return true;
                case "-":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value -= externalEffector.value, min, max);
                    else
                        target.value -= externalEffector.value;
                    return true;
                case "*":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value *= externalEffector.value, min, max);
                    else
                        target.value *= externalEffector.value;
                    return true;
                case "/":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value /= externalEffector.value, min, max);
                    else
                        target.value /= externalEffector.value;
                    return true;
                default:
                    Debug.LogError("Operation Not Match : " +
                        operation + ", available operation are \"=+-*/\"");
                    return false;
            }
        }
        else
        {
            switch (operation)
            {
                case "=":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value = effector, min, max);
                    else
                        target.value = effector;
                    return true;
                case "+":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value += effector, min, max);
                    else
                        target.value += effector;
                    return true;
                case "-":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value -= effector, min, max);
                    else
                        target.value -= effector;
                    return true;
                case "*":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value *= effector, min, max);
                    else
                        target.value *= effector;
                    return true;
                case "/":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value /= effector, min, max);
                    else
                        target.value /= effector;
                    return true;
                default:
                    Debug.LogError("Operation Not Match : " +
                        operation + ", available operation are \"=+-*/\"");
                    return false;
            }
        }
    }

    // used for repeted operation
    public bool DoOperation(int multiplier)
    {
        if (useExternalEeffector)
        {
            switch (operation)
            {
                case "=":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value = externalEffector.value * multiplier, min, max);
                    else
                        target.value = externalEffector.value * multiplier;
                    return true;
                case "+":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value += externalEffector.value * multiplier, min, max);
                    else
                        target.value += externalEffector.value * multiplier;
                    return true;
                case "-":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value -= externalEffector.value * multiplier, min, max);
                    else
                        target.value -= externalEffector.value * multiplier;
                    return true;
                case "*":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value *= externalEffector.value * multiplier, min, max);
                    else
                        target.value *= externalEffector.value * multiplier;
                    return true;
                case "/":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value /= externalEffector.value * multiplier, min, max);
                    else
                        target.value /= externalEffector.value * multiplier;
                    return true;
                default:
                    Debug.LogError("Operation Not Match : " +
                        operation + ", available operation are \"=+-*/\"");
                    return false;
            }
        }
        else
        {
            switch (operation)
            {
                case "=":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value = effector * multiplier, min, max);
                    else
                        target.value = effector * multiplier;
                    return true;
                case "+":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value += effector * multiplier, min, max);
                    else
                        target.value += effector * multiplier;
                    return true;
                case "-":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value -= effector * multiplier, min, max);
                    else
                        target.value -= effector * multiplier;
                    return true;
                case "*":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value *= effector * multiplier, min, max);
                    else
                        target.value *= effector * multiplier;
                    return true;
                case "/":
                    if (useRange)
                        target.value = Mathf.Clamp(target.value /= effector * multiplier, min, max);
                    else
                        target.value /= effector * multiplier;
                    return true;
                default:
                    Debug.LogError("Operation Not Match : " +
                        operation + ", available operation are \"=+-*/\"");
                    return false;
            }
        }
    }

    // do operation with a custom effector instead
    public bool DoOperation(float cutomEffector)
    {
        switch (operation)
        {
            case "=":
                if (useRange)
                    target.value = Mathf.Clamp(target.value = cutomEffector, min, max);
                else
                    target.value = cutomEffector;
                return true;
            case "+":
                if (useRange)
                    target.value = Mathf.Clamp(target.value += cutomEffector, min, max);
                else
                    target.value += cutomEffector;
                return true;
            case "-":
                if (useRange)
                    target.value = Mathf.Clamp(target.value -= cutomEffector, min, max);
                else
                    target.value -= cutomEffector;
                return true;
            case "*":
                if (useRange)
                    target.value = Mathf.Clamp(target.value *= cutomEffector, min, max);
                else
                    target.value *= cutomEffector;
                return true;
            case "/":
                if (useRange)
                    target.value = Mathf.Clamp(target.value /= cutomEffector, min, max);
                else
                    target.value /= cutomEffector;
                return true;
            default:
                Debug.LogError("Operation Not Match : " +
                    operation + ", available operation are \"=+-*/\"");
                return false;
        }
    }

    // used to pass value to other instace of this class
    public void PassValue(FloatPair floatPair)
    {
        target = floatPair.target;
        effector = floatPair.effector;

        useRange = floatPair.useRange;
        min = floatPair.min;
        max = floatPair.max;

        useExternalEeffector = floatPair.useExternalEeffector;
        externalEffector = floatPair.externalEffector;
    }
}
