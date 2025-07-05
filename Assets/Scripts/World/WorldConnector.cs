using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldConnector : MonoBehaviour
{
    [Header("This scene connector")]
    public int connection_id;
    public Vector2 spawnPoint;

    [Header("Other scene connector")]
    public string scene;
    public int connects_with;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != PlayerController.instance.gameObject) return;


        PlayerController.instance.Movement.enabled = false;

        PersistentData.connection_id = connects_with;

        SceneTransition.SceneTransitionManager.instance.ChangeScene(scene);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(spawnPoint, Vector3.one);
    }

    [EasyButtons.Button]
    public void CenterSpawnPoint()
    {
        spawnPoint = transform.position;
    }
}
