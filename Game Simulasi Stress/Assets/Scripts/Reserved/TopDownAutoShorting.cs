using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAutoShorting : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;

    [SerializeField]
    private bool isStatic = false; //if true the script only run  once

    private Renderer TargetRenderer;
    public BoxCollider2D TransitionOffset;

    private void Awake()
    {
        TargetRenderer = gameObject.GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        TargetRenderer.sortingOrder = (int)(transform.position.y - TransitionOffset.offset.y);
        if (isStatic)
        {
            Destroy(this);
        }
    }
}
