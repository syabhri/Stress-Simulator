// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;

[CreateAssetMenu(menuName = "Runtime Set/Thing")]
public class ThingRuntimeSet : RuntimeSet<GameObject>
{
    // spesific funtion for gameobject type
    public void Acitvate()
    {
        if (isSingle)
        {
            Item.SetActive(true);
        }
        else
        {
            foreach (var item in Items)
            {
                item.SetActive(true);
            }
        }
    }
    public void Deactivate()
    {
        if (isSingle)
        {
            Item.SetActive(false);
        }
        else
        {
            foreach (var item in Items)
            {
                item.SetActive(false);
            }
        }
    }
}