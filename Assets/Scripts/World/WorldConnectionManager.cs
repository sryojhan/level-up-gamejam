using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1001)]

public class WorldConnectionManager : Singleton<WorldConnectionManager>
{


    private void Start()
    {
        if (PersistentData.connection_id < 0) return;

        int connection_id = PersistentData.connection_id;
        PersistentData.connection_id = -1;

        Dictionary<int, Vector2> connectors = new();

        foreach (Transform connectorTr in transform)
        {
            WorldConnector connector = connectorTr.GetComponent<WorldConnector>();

            if (connector)
            {
                if (connectors.ContainsKey(connector.connection_id))
                {
                    Debug.LogError("This level connectors are wrong!! " + SceneManager.GetActiveScene().name);
                }
                else connectors.Add(connector.connection_id, connector.spawnPoint);
            }
        }

        if (!connectors.ContainsKey(connection_id)) return;

        PlayerController.instance.transform.position = connectors[connection_id];
    }

}
