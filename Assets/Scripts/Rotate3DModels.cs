using UnityEngine;

public class Rotate3DModels : MonoBehaviour
{
    public Quaternion targetRotation;

    [EasyButtons.Button]
    public void SetTargetRotation()
    {
        targetRotation = transform.localRotation;
    }


    [EasyButtons.Button]
    public void SetRotation()
    {

        float parentRotation = transform.parent.localRotation.eulerAngles.z;

        Quaternion rotatedTilt = targetRotation * Quaternion.AngleAxis(parentRotation, Vector3.up);

        transform.localRotation = rotatedTilt;
    }

    [EasyButtons.Button]
    public void ResetRotation()
    {
        transform.localRotation = targetRotation;
    }


    public float rotationSpeed = 1;

    public bool setRotation = false;

    private void Update()
    {
        transform.parent.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if(setRotation)
        SetRotation();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + targetRotation * Vector3.forward);

        Gizmos.color = Color.red;

        float parentRotation = transform.parent.localRotation.eulerAngles.z;

        Quaternion rotatedTilt = targetRotation * Quaternion.AngleAxis(parentRotation, Vector3.up);

        Gizmos.DrawLine(transform.position, transform.position + rotatedTilt * Vector3.forward);

    }
}
