using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatPair
{
    [Tooltip("Name The float Pair For Easy Identification (Optional)")]
    public string name;

    [Tooltip("Target is the external value to be chaged by effector using the operator")]
    public FloatVariable target;

    [Tooltip("effector is the value used to change target (not used if using external effector)")]
    public float effector;

    [Space, Tooltip("Use External Effector Instead of the effector value above (Optional)")]
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

    // used to pass value to other instace of this class
    public void PassValue(FloatPair floatPair)
    {
        name = floatPair.name;
        target = floatPair.target;
        effector = floatPair.effector;

        useExternalEeffector = floatPair.useExternalEeffector;
        externalEffector = floatPair.externalEffector;
    }
}
