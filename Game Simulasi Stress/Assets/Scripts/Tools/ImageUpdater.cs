using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImageUpdater : MonoBehaviour
{
    public Image image;
    public TMP_Dropdown dropdown;

    public List<Sprite> sprites;

    private void Start()
    {
        UpDateImage();
    }

    public void UpDateImage()
    {
        image.sprite = sprites[dropdown.value];
    }
}
