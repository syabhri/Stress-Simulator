// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
//
// Modified By : Muhammad Syabhri Mustafa
// Date:   10/05/19
// ----------------------------------------------------------------------------

using UnityEngine;

public class ThingDisabler : MonoBehaviour
{
    public ThingRuntimeSet Set;

    public void DisableAll()
    {
        // Loop backwards since the list may change when disabling
        for (int i = Set.Items.Count - 1; i >= 0; i--)
        {
            Set.Items[i].gameObject.SetActive(false);
        }
    }

    public void DisableRandom()
    {
        int index = Random.Range(0, Set.Items.Count);
        Set.Items[index].gameObject.SetActive(false);
    }

    public void Disable(int i)
    {
        Set.Items[i].gameObject.SetActive(false);
    }

    public void Disable(string name)
    {
        Thing T = Set.Items.Find(Thing => Thing.gameObject.name == name);
        if (T == null)
        {
            Debug.LogWarning("Thing with name " + name + "Not Found");
            return;
        }
        T.gameObject.SetActive(false);
    }
}