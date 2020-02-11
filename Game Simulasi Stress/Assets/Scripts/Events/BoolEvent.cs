using UnityEngine;
using UnityEngine.Events;

public class BoolEvent : MonoBehaviour
{
    public BoolContainer condition;
    public bool CheckOnEnable;

    public UnityEvent OnTrue;
    public UnityEvent OnFalse;

    private void OnEnable()
    {
        if (CheckOnEnable) CheckCondition(condition);

        condition.OnValueChanged += CheckCondition;
    }

    private void OnDisable()
    {
        condition.OnValueChanged -= CheckCondition;
    }

    public void CheckCondition(BoolContainer condition)
    {
        if (condition.Value)
            OnTrue.Invoke();
        else
            OnFalse.Invoke();
    }
}
