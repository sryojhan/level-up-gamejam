using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;
    private int currentHealth;

    public delegate void OnHealthUpdate(int currentHealth);
    public OnHealthUpdate onHealthUpdate;
    public OnHealthUpdate onMaxHealthUpdate;

    public UnityEvent onPlayerDeath;

    public void InitialiseHealth()
    {
        if(PersistentData.maxHealth > 0)
        {
            maxHealth = PersistentData.maxHealth;
            currentHealth = PersistentData.currentHealth;
        }
        else
        {
            currentHealth = maxHealth;
        }

        onHealthUpdate += UpdatePersistentHealth;
        onMaxHealthUpdate += UpdatePersistentMaxHealth;

        onMaxHealthUpdate(maxHealth);
        onHealthUpdate(currentHealth);
    }

    private void UpdatePersistentHealth(int health)
    {
        PersistentData.currentHealth = health;
    }
    private void UpdatePersistentMaxHealth(int maxHealth)
    {
        PersistentData.maxHealth = maxHealth;
    }

    [EasyButtons.Button]
    public void LoseHealth()
    {
        currentHealth--;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            onPlayerDeath?.Invoke();
        }

        onHealthUpdate(currentHealth);
    }

    [EasyButtons.Button]
    public void RestoreHealth()
    {
        currentHealth++;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        onHealthUpdate(currentHealth);
    }


    [EasyButtons.Button]
    public void AddMaxHealth()
    {
        maxHealth++;
        currentHealth++;

        onMaxHealthUpdate(maxHealth);
        onHealthUpdate(currentHealth);
    }

}
