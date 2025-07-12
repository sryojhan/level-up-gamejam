using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SimpleShake))]
public class RandomShake : MonoBehaviour
{
    public float minSeconds = 3f;
    public float maxSeconds = 10f;

    IEnumerator Start()
    {
        SimpleShake shake = GetComponent<SimpleShake>();

        while (true)
        {
            shake.Shake();
            yield return new WaitForSeconds(Random.Range(minSeconds, maxSeconds));
        }
    }


}
