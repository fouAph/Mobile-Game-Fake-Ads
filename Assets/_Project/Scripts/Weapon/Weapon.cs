using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] AudioClip shootSFX;

    [SerializeField] int damage = 10;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float fireRate = .5f;
    [SerializeField] float knockBackForce = 2500;
    public void Attack()
    {
        Bullet b = BulletPoolManager.Instance.SpawnFromPool(bulletPrefab.name);
        if (SoundEffectPoolManager.Instance)
            SoundEffectPoolManager.Instance.PlayAudioAtPosition(shootSFX.name, bulletSpawnPoint.position);
        b.OnBulletSpawn(this, bulletSpawnPoint.position);
        b.OnBulletLaunch(bulletSpeed, Vector3.forward);
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
