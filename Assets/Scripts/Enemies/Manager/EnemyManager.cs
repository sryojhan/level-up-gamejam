using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public int enemiesAlive;
    public GameObject[] walls;

    private void Awake()
    {
        enemiesAlive = 0;
    }

    public void AddEnemy()
    {
        enemiesAlive++;
    }

    public void EnemyDied()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0)
        {
            foreach (GameObject go in walls)
            {
                go.SetActive(false);
            }
        }
    }
}
