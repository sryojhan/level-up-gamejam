using UnityEngine;

public class SimpleShake : MonoBehaviour
{
    private float shakeTimer = 0f;
    private float shakeStrength = 0f;
    private float shakeDuration = 0f;

    public float defaultDuration = 0.2f;
    public float defaultStrength = 0.1f;

    private float noiseSeed;

    private void Awake()
    {
        noiseSeed = Random.value * 100f;
    }

    [EasyButtons.Button]
    public void Shake()
    {
        shakeTimer = defaultDuration;
        shakeStrength = defaultStrength;

        shakeDuration = shakeTimer;
    }

    [EasyButtons.Button]
    public void ShakeCustom(float duration, float strength)
    {
        shakeTimer = duration;
        shakeStrength = strength;

        shakeDuration = shakeTimer;
    }


    void LateUpdate()
    {
        if (shakeTimer > 0f)
        {
            float damper = shakeTimer / shakeDuration;
            float offsetX = (Mathf.PerlinNoise(noiseSeed, Time.time * 25f) - 0.5f) * 2f;
            float offsetY = (Mathf.PerlinNoise(noiseSeed + 1f, Time.time * 25f) - 0.5f) * 2f;

            Vector3 offset = damper * shakeStrength * new Vector3(offsetX, offsetY, 0f);
            transform.localPosition = offset;

            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0f)
                transform.localPosition = Vector3.zero;
        }
    }
}
