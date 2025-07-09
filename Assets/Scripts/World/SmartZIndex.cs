using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SmartZIndex : MonoBehaviour
{
    SpriteRenderer sprRenderer;

    public bool canMove = false;

    private void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();

        sprRenderer.sortingOrder = Mathf.FloorToInt(transform.position.y * -100);

        if (!canMove)
            Destroy(this);
    }

    private void Update()
    {
        sprRenderer.sortingOrder = Mathf.FloorToInt(transform.position.y * -100);
    }

}
