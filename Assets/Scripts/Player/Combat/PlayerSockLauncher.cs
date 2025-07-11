using UnityEngine;

public class PlayerSockLauncher : MonoBehaviour
{
    public SockFactory sockData;

    public int ammo = 3;
    public float cooldown = 1;

    private float lastShootTime = 0;

    public bool CanShoot(bool wantsToShoot)
    {
        float timebetweenshots = Time.time - lastShootTime;
        return isActiveAndEnabled && wantsToShoot && timebetweenshots > cooldown && ammo > 0;
    }

    public void Shoot(Vector2 direction)
    {
        if (!isActiveAndEnabled) return;

        sockData.CreateSock(transform.position, direction);

        ammo--;
        lastShootTime = Time.time;
    }
}
