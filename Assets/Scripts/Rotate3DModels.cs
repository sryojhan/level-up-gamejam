using UnityEngine;

public class Rotate3DModels : MonoBehaviour
{
    public float rotationSpeed = 100;

    private void Update()
    {
        transform.parent.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
