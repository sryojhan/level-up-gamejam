using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldConnector : MonoBehaviour
{
    public int connection_id;
    public Vector2 spawnPoint;

    struct Connection
    {
        public int id;
        public string scene;

        public readonly string CreateConnectionUID()
        {
            return scene + "_" + id.ToString();
        }
    }

    private static void InitialiseDictionary()
    {
        connectionDictionary = new Dictionary<string, Connection>();

        static Connection c(string _scene, int _id) { return new Connection { id = _id, scene = _scene }; }

        KeyValuePair<Connection, Connection>[] allConnections =
        {
            new (c("Bedroom Main", 0), c("Bedroom Single 1", 1) ),
            new (c("Bedroom Main", 1), c("Bedroom Single 2", 1) ),
            new (c("Bedroom Main", 2), c("Bedroom Single 3", 0) ),
            new (c("Bedroom Main", 3), c("Initial room", 0) ),

            new (c("Bathroom Single 2", 1), c("Bedroom Single 1", 0)),

            new (c("Bathroom Main", 0), c("Bathroom Single 1", 0) ),
            new (c("Bathroom Main", 1), c("Bathroom Single 2", 0) ),
            new (c("Bathroom Main", 2), c("Bathroom Laundry", 0) ),


        };


        foreach (var connection in allConnections)
        {
            static void TryAdd(string key, Connection value)
            {
                if (connectionDictionary.ContainsKey(key)) return;

                connectionDictionary.Add(key, value);
            }

            TryAdd(connection.Key.CreateConnectionUID(), connection.Value);
            TryAdd(connection.Value.CreateConnectionUID(), connection.Key);
        }

    }

    static Dictionary<string, Connection> connectionDictionary;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != PlayerController.instance.gameObject) return;

        PlayerController.instance.Movement.enabled = false;

        if (connectionDictionary == null) InitialiseDictionary();

        string currentScene = SceneManager.GetActiveScene().name;


        Connection connection = connectionDictionary[new Connection { id = connection_id, scene = currentScene }.CreateConnectionUID()];

        PersistentData.connection_id = connection.id;
        SceneTransition.SceneTransitionManager.instance.ChangeScene(connection.scene);
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
