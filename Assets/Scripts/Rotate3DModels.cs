using UnityEngine;

public class Rotate3DModels : MonoBehaviour
{

    public bool rotateToMouse = false;

    public void SetRotation(float rotation)
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, rotation, 0));
    }

    private void Update()
    {
        if (!rotateToMouse) return;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = (mousePosition - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * -Mathf.Rad2Deg;

        SetRotation(angle);

    }

}
