using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] AudioClip shootSFX;

    [SerializeField] int damage = 10;
    [SerializeField] float fireRate = .5f;
    public void Attack()
    {
        Bullet b = BulletPoolManager.Instance.SpawnFromPool(bulletPrefab.name);
        if (SoundEffectPoolManager.Instance)
            SoundEffectPoolManager.Instance.PlayAudioToPosition(shootSFX.name, bulletSpawnPoint.position);
        b.OnBulletSpawn(this, bulletSpawnPoint.position);
        b.OnBulletLaunch(Vector3.forward);
    }

    public int GetDamage() => damage;
    public float GetFireRate() => fireRate;
}

public interface IWeapon
{
    void Attack();
    int GetDamage();
    float GetFireRate();
}
