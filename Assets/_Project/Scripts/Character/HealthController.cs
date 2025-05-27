using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour, IDamageable
{
    public int CurrentHealth => currentHealth;
    public bool IsDie => isDie;
    [SerializeField] bool hideWhenDie = true;

    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;

    [HideInInspector]
    public UnityEvent OnDieEvent = new();


    private bool isDie;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void OnDamage(int damage)
    {
        if (isDie) return;
        currentHealth = Math.Clamp(currentHealth - damage, 0, maxHealth);


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

        OnDieEvent?.Invoke();
    }
}

public interface IDamageable
{
    void OnDamage(int damage);
    void OnDie();
}