using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private int enemiesAlive;
    public GameObject[] walls;

    private void Awake()
    {
        EnsureInitialised();
        WallsSetActive(false);

        enemiesAlive = 0;

    }

    public void AddEnemy()
    {
        enemiesAlive++;

        WallsSetActive(true);
    }

    public void EnemyDied()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0)
        {
            WallsSetActive(false);
        }
    }


    void WallsSetActive(bool value)
    {
        foreach (GameObject go in walls)
        {
            go.SetActive(value);
        }
    }

}
