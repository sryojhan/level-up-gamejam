using UnityEngine;

public class PlayerSockLauncher : MonoBehaviour
{
    public SockFactory sockData;

    public int simultaneousSocks = 0;

    public float minTimeBetweenShots = 1;
    public float timeToRecoverSock = 8;

    private int currentSockNumber = 0;

    private float lastShootTime = 0;
    private float sockCooldown = 0;

    public delegate void OnSockUpdate(int count);
    public OnSockUpdate onSockCountUpdate;
    public OnSockUpdate onMaxSockCountUpgrade;

    public void InitialisePersistentData()
    {
        currentSockNumber = simultaneousSocks;


        onSockCountUpdate += RecoverSock;
    }

    public bool CanShoot(bool wantsToShoot)
    {
        float timebetweenshots = Time.time - lastShootTime;
        return isActiveAndEnabled && wantsToShoot && timebetweenshots > minTimeBetweenShots && currentSockNumber > 0;
    }

    public void Shoot(Vector2 direction)
    {
        if (!isActiveAndEnabled) return;

        sockData.CreateSock(transform.position, direction);

        currentSockNumber--;
        lastShootTime = Time.time;

        onSockCountUpdate(currentSockNumber);

        SoundManager.instance.PlayThrowBoomerang();

    }


    public void UpdateSockCooldown()
    {
        if (currentSockNumber >= simultaneousSocks)
        {
            sockCooldown = 0;
            return;
        }

        sockCooldown += Time.deltaTime;

        if(sockCooldown > timeToRecoverSock)
        {
            sockCooldown = 0;
            SoundManager.instance.PlayUIBoing();
            onSockCountUpdate(++currentSockNumber);
        }
    }

    public void RecoverSock()
    {
        if (currentSockNumber < simultaneousSocks)
        {
            SoundManager.instance.PlayUIBoing();
            currentSockNumber++;
            onSockCountUpdate(currentSockNumber);
        }
    }


    private void RecoverSock(int count)
    {
        
        PersistentData.currentSockCount = currentSockNumber;
    }


    [EasyButtons.Button]
    public void AddOneMaxSock()
    {
        onMaxSockCountUpgrade?.Invoke(++simultaneousSocks);
    }


    public float CurrentSockCooldown()
    {
        return sockCooldown / timeToRecoverSock;
    }

    public int GetCurrentSocks()
    {
        return currentSockNumber;
    }

}
