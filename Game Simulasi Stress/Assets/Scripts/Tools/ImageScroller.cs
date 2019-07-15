using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScroller : MonoBehaviour
{
    private RawImage target;
    //public bool horizontal = true;
    public float speed = 0.01f;
    Rect uvRect;
    // Start is called before the first frame update
    void Start()
    {
        target = gameObject.GetComponent<RawImage>();
        uvRect = target.uvRect;
    }

    // Update is called once per frame
    void Update()
    {
        uvRect.x -= speed * Time.deltaTime;   
        target.uvRect = uvRect;
    }
}
