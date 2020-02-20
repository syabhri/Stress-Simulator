using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDirection : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite left;
    public Sprite right;
    public Sprite up;
    public Sprite down;

    private Vector3 direction;
    private Sprite defaultSprite;

    private void OnTriggerStay2D(Collider2D collision)
    {
        direction = transform.InverseTransformPoint(collision.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        defaultSprite = spriteRenderer.sprite;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.sprite = defaultSprite;
    }

    public void UpdateDirection()
    {
        if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))
        {
            if (direction.x >= 0)
                spriteRenderer.sprite = right;
            else
                spriteRenderer.sprite = left;
        }
        else
        {
            if (direction.y >= 0)
                spriteRenderer.sprite = up;
            else
                spriteRenderer.sprite = down;
        }
    }
}
