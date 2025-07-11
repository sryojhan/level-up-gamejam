using UnityEngine;

public class FloatBehaviour : MonoBehaviour
{
    public Vector2 offset;

    private Vector2 initialPosition;
    private float t;

    public Interpolation movementType;

    public Transform shadow;
    public float shadowScaleMultiplierOnMaxFloat = .5f;

    private Vector3 shadowOriginalScale;

    private void Start()
    {
        initialPosition = transform.position;

        if(shadow)
        shadowOriginalScale = shadow.localScale;
    }

    private void Update()
    {
        t += Time.deltaTime;

        float pingpong = Mathf.PingPong(t, 1);

        float i = movementType.Interpolate(pingpong);

        transform.position = Vector2.Lerp(initialPosition, initialPosition + offset, i);

        if (shadow)
            shadow.localScale = Vector3.Lerp(shadowOriginalScale, shadowOriginalScale * shadowScaleMultiplierOnMaxFloat, i);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)offset);
        Gizmos.DrawSphere(transform.position + (Vector3)offset, .01f);
    }

}
