using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public IWeapon CurrentWeapon => CurrentWeapon;
    // public float FireSpeed => fireRate;
    // public int Damage => damage;

    // [SerializeField] int damage = 10;
    // [SerializeField] float fireRate = .5f;
    [SerializeField] IWeapon currentWeapon;

    private float lastFired;

    private void Start()
    {
        if (TryGetComponent<IWeapon>(out var weapon))
        {
            EquipWeapon(weapon);
        }
    }
    public void EquipWeapon(IWeapon weapon)
    {
        currentWeapon = weapon;
        // damage = weapon.GetDamage();
        // fireRate = weapon.GetFireRate();
    }

    public void UseWeapon()
    {
        lastFired = Time.time;

        currentWeapon.Attack();
    }

    public bool CheckCanShoot()
    {
        return currentWeapon.CheckCanShoot(lastFired);
    }
}
