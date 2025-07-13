using UnityEngine;

public class CheckColliderWalls : MonoBehaviour
{
    public RoombaEnemy roomba;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        roomba.RoombaCollision(collision);
    }

}
