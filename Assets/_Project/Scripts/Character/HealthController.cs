using System;
using UnityEngine;

public class HealthController : MonoBehaviour, IDamageable
{
    public int CurrentHealth => currentHealth;
    public bool IsDie => isDie;
    [SerializeField] bool hideWhenDie = true;

    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;


    private bool isDie;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void OnDamage(int damage)
    {
        if (isDie) return;
        currentHealth = Math.Clamp(currentHealth, 0, currentHealth - damage);

        if (currentHealth <= 0)
        {
            isDie = true;
            OnDie();
        }
    }

    public void OnDie()
    {
        if (hideWhenDie)
            gameObject.SetActive(false);
    }
}

public interface IDamageable
{
    void OnDamage(int damage);
    void OnDie();
}