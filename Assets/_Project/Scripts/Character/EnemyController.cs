using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool IsDie => healthController.IsDie;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] HealthController healthController;
    [SerializeField] Animator animator;

    Collider[] colliders;
    Rigidbody[] rigidbodies;

    void Start()
    {
        healthController = GetComponent<HealthController>();
        healthController.OnDieEvent.AddListener(OnDieEvent);
        colliders = GetComponentsInChildren<Collider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();

        ToggleRagdoll(false);
    }

    private void ToggleRagdoll(bool isActive)
    {
        foreach (var item in colliders)
        {
            item.isTrigger = !isActive;
        }

        foreach (var item in rigidbodies)
        {
            item.isKinematic = !isActive;
        }

        colliders[0].isTrigger = false;
        rigidbodies[0].isKinematic = false;
    }

    private void Update()
    {
        Move(-Vector3.forward);
    }

    public void Move(Vector3 direction)
    {
        transform.position += moveSpeed * Time.deltaTime * direction;
    }

    private void OnDieEvent()
    {
        animator.enabled = false;
        this.enabled = false;
        ToggleRagdoll(true);
    }

    public void PushBackWhenDie(float force)
    {
        foreach (var item in rigidbodies)
        {
            if (Random.Range(0f, 1f) < 0.5f) // 50% chance to apply force
            {
                item.AddForce(Vector3.forward * force, ForceMode.Force);
            }
        }
    }
}
