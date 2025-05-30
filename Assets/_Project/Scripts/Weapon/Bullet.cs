using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    [SerializeField] float maxBulletDistance = 20f;
    // [SerializeField] float bulletSpeed = 5f;
    [SerializeField] Weapon weapon;
    private Vector3 initialPos;

    public void OnBulletSpawn(Weapon _weapon, Vector3 spawnPos = new())
    {
        if (spawnPos != Vector3.zero)
        {
            transform.position = spawnPos;
        }

        if (weapon == null) weapon = _weapon;
        initialPos = transform.position;
        gameObject.SetActive(true);
    }

    public void OnBulletLaunch(float bulletSpeed, Vector3 direction)
    {
        StartCoroutine(LaunchBulletCor());
        IEnumerator LaunchBulletCor()
        {
            while ((transform.position - initialPos).magnitude < maxBulletDistance)
            {
                transform.position += Time.deltaTime * bulletSpeed * direction;
                yield return null;
            }

            OnBulletDestroy();
        }
    }

    public void OnBulletDestroy()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<EnemyController>(out var enemy);
        if (enemy == false) return;
        if (enemy.IsDie == true) return;
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {

            damageable.OnDamage(weapon.GetDamage());
            if (VFXPoolManager.Instance)
            {
                VFXPoolManager.Instance.PlayVFXAtPosition("BloodVFX", transform.position);
                SoundEffectPoolManager.Instance.PlayAudioAtPosition("FleshHit", transform.position);

            }
            if (enemy.IsDie)
                enemy.PushBackWhenDie(weapon.GetKnockBackForce());
            OnBulletDestroy();
        }
    }
}

public interface IBullet
{
    void OnBulletSpawn(Weapon _weapon, Vector3 spawnPos);
    void OnBulletLaunch(float bulletSpeed, Vector3 direction);
    void OnBulletDestroy();
}
