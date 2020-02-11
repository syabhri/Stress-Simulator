using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatPair
{
    public enum Operation { assign, add, subtract, multiply, divide }

    [Tooltip("Target is the external.Value to be chaged by effector using the operator")]
    public FloatContainer target;

    [Tooltip("effector is the.Value used to change target (not used if using external effector)")]
    public float effector;

    [Tooltip("Operation is the basic operator \"=+-*/\" used to change the target")]
    public Operation operation;

    [Header("Range"), Tooltip("Use Range To Limit the Operation Result")]
    public bool useRange = true;
    public float min = 0;
    public float max = 100;

    [Header("External Effector"), Tooltip("Use External Effector Instead of the effector.Value above (Optional)")]
    public bool useExternalEeffector;
    public FloatContainer externalEffector;

    // default constructor
    public FloatPair() { }

    // constructor
    public FloatPair(FloatContainer target, float effector, string operation)
    {
        this.target = target;
        this.effector = effector;
    }

    // counstructor tp be used if using external effector.Value
    public FloatPair(FloatContainer target, FloatContainer externalEffector, string operation)
    {
        this.target = target;
        this.externalEffector = externalEffector;
        useExternalEeffector = true;
    }

    // do a calculation between target and effector
    public void DoOperation()
    {
        if (useExternalEeffector)
        {
            switch (operation)
            {
                case Operation.assign:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value = externalEffector.Value, min, max);
                    else
                        target.Value = externalEffector.Value;
                    break;
                case Operation.add:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value += externalEffector.Value, min, max);
                    else
                        target.Value += externalEffector.Value;
                    break;
                case Operation.subtract:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value -= externalEffector.Value, min, max);
                    else
                        target.Value -= externalEffector.Value;
                    break;
                case Operation.multiply:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value *= externalEffector.Value, min, max);
                    else
                        target.Value *= externalEffector.Value;
                    break;
                case Operation.divide:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value /= externalEffector.Value, min, max);
                    else
                        target.Value /= externalEffector.Value;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (operation)
            {
                case Operation.assign:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value = effector, min, max);
                    else
                        target.Value = effector;
                    break;
                case Operation.add:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value += effector, min, max);
                    else
                        target.Value += effector;
                    break;
                case Operation.subtract:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value -= effector, min, max);
                    else
                        target.Value -= effector;
                    break;
                case Operation.multiply:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value *= effector, min, max);
                    else
                        target.Value *= effector;
                    break;
                case Operation.divide:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value /= effector, min, max);
                    else
                        target.Value /= effector;
                    break;
                default:
                    break;
            }
        }
    }

    // used for repeted operation
    public void DoOperation(int multiplier)
    {
        if (useExternalEeffector)
        {
            switch (operation)
            {
                case Operation.assign:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value = externalEffector.Value * multiplier, min, max);
                    else
                        target.Value = externalEffector.Value * multiplier;
                    break;
                case Operation.add:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value += externalEffector.Value * multiplier, min, max);
                    else
                        target.Value += externalEffector.Value * multiplier;
                    break;
                case Operation.subtract:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value -= externalEffector.Value * multiplier, min, max);
                    else
                        target.Value -= externalEffector.Value * multiplier;
                    break;
                case Operation.multiply:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value *= externalEffector.Value * multiplier, min, max);
                    else
                        target.Value *= externalEffector.Value * multiplier;
                    break;
                case Operation.divide:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value /= externalEffector.Value * multiplier, min, max);
                    else
                        target.Value /= externalEffector.Value * multiplier;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (operation)
            {
                case Operation.assign:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value = effector * multiplier, min, max);
                    else
                        target.Value = effector * multiplier;
                    break;
                case Operation.add:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value += effector * multiplier, min, max);
                    else
                        target.Value += effector * multiplier;
                    break;
                case Operation.subtract:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value -= effector * multiplier, min, max);
                    else
                        target.Value -= effector * multiplier;
                    break;
                case Operation.multiply:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value *= effector * multiplier, min, max);
                    else
                        target.Value *= effector * multiplier;
                    break;
                case Operation.divide:
                    if (useRange)
                        target.Value = Mathf.Clamp(target.Value /= effector * multiplier, min, max);
                    else
                        target.Value /= effector * multiplier;
                    break;
                default:
                    break;
            }
        }
    }

    // do operation with a custom effector instead
    public void DoOperation(float cutomEffector)
    {
        switch (operation)
        {
            case Operation.assign:
                if (useRange)
                    target.Value = Mathf.Clamp(target.Value = cutomEffector, min, max);
                else
                    target.Value = cutomEffector;
                break;
            case Operation.add:
                if (useRange)
                    target.Value = Mathf.Clamp(target.Value += cutomEffector, min, max);
                else
                    target.Value += cutomEffector;
                break;
            case Operation.subtract:
                if (useRange)
                    target.Value = Mathf.Clamp(target.Value -= cutomEffector, min, max);
                else
                    target.Value -= cutomEffector;
                break;
            case Operation.multiply:
                if (useRange)
                    target.Value = Mathf.Clamp(target.Value *= cutomEffector, min, max);
                else
                    target.Value *= cutomEffector;
                break;
            case Operation.divide:
                if (useRange)
                    target.Value = Mathf.Clamp(target.Value /= cutomEffector, min, max);
                else
                    target.Value /= cutomEffector;
                break;
            default:
                break;
        }
    }

    // used to pass.Value to other instace of this class
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
