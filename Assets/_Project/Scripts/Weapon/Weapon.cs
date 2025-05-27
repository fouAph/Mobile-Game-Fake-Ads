using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] AudioClip shootSFX;

    [SerializeField] int damage = 10;
    [SerializeField] float fireRate = .5f;
    [SerializeField] int bulletCountPerShoot = 1;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float knockBackForce = 2500;

    /*
    public void Attack()
    {
        for (int i = 0; i < bulletCountPerShoot; i++)
        {
            float xOffset = 0f;
            if (bulletCountPerShoot > 1)
            {
                // Spread bullets evenly around the original Y position
                float spacing = 0.2f; // adjust this value for wider/narrower spread
                xOffset = (i - (bulletCountPerShoot - 1) / 2f) * spacing;
            }

            Vector3 offset = new Vector3(xOffset, 0f, 0f);
            SpawnBullet(offset);
        }
    }

    private void SpawnBullet(Vector3 offset)
    {
        Bullet b = BulletPoolManager.Instance.SpawnFromPool(bulletPrefab.name);
        if (SoundEffectPoolManager.Instance)
            SoundEffectPoolManager.Instance.PlayAudioAtPosition(shootSFX.name, bulletSpawnPoint.position);
        Vector3 spawnPos = bulletSpawnPoint.position + offset;
        b.OnBulletSpawn(this, spawnPos);
        b.OnBulletLaunch(bulletSpeed, Vector3.forward);
    }
    */

    public void Attack()
    {
        for (int i = 0; i < bulletCountPerShoot; i++)
        {
            float angleOffset = 0f;
            if (bulletCountPerShoot > 1)
            {
                float totalSpreadAngle = 15f; // total spread in degrees
                float angleStep = totalSpreadAngle / (bulletCountPerShoot - 1);
                angleOffset = -totalSpreadAngle / 2f + i * angleStep;
            }

            SpawnBullet(angleOffset);
        }
    }

    private void SpawnBullet(float angleOffset)
    {
        Bullet b = BulletPoolManager.Instance.SpawnFromPool(bulletPrefab.name);
        if (SoundEffectPoolManager.Instance)
            SoundEffectPoolManager.Instance.PlayAudioAtPosition(shootSFX.name, bulletSpawnPoint.position);

        Vector3 direction = Quaternion.Euler(0f, angleOffset, 0f) * Vector3.forward;

        b.OnBulletSpawn(this, bulletSpawnPoint.position);
        b.OnBulletLaunch(bulletSpeed, direction);
    }



    public bool CheckCanShoot(float lastFired)
    {
        float secondsPerShot = 1f / fireRate;
        return Time.time - lastFired >= secondsPerShot;
    }

    public int GetDamage() => damage;
    public float GetFireRate() => fireRate;
    public float GetBulletSpeed() => bulletSpeed;
    public float GetKnockBackForce() => knockBackForce;
}

public interface IWeapon
{
    bool CheckCanShoot(float lastFired);
    void Attack();
    int GetDamage();
    float GetFireRate();
    float GetBulletSpeed();
    float GetKnockBackForce();
}
