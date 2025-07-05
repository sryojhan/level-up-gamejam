using UnityEngine;

[CreateAssetMenu(menuName = "Sock data")]
public class SockFactory : ScriptableObject {

    public Sprite[] allSprites;
    public GameObject sockPrefab;
    public GameObject CreateSock(Vector2 originPosition, Vector2 direction)
    {
        GameObject go = Instantiate(sockPrefab, originPosition, Quaternion.identity);

        SockBoomerang sock = go.GetComponent<SockBoomerang>();
        sock.direction = direction;

        SpriteRenderer sprRend = go.GetComponentInChildren<SpriteRenderer>();
        sprRend.sprite = allSprites[Random.Range(0, allSprites.Length)];

        return go;
    }
}
