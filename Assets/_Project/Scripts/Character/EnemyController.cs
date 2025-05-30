using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool IsDie => healthController.IsDie;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] HealthController healthController;
    [SerializeField] Animator animator;

    private Rigidbody rb;
    private Collider col;

    private Collider[] childColliders;
    private Rigidbody[] childRigidbodies;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        healthController = GetComponent<HealthController>();
        healthController.OnDieEvent.AddListener(OnDieEvent);
        childColliders = GetComponentsInChildren<Collider>();
        childRigidbodies = GetComponentsInChildren<Rigidbody>();

        var allColliders = GetComponentsInChildren<Collider>();
        var allRigidbodies = GetComponentsInChildren<Rigidbody>();

        // Filter to exclude components on this GameObject
        childColliders = allColliders.Where(c => c != col).ToArray();
        childRigidbodies = allRigidbodies.Where(r => r != rb).ToArray();

        if (GameManager.Instance)
            healthController.OnDieEvent.AddListener(GameManager.Instance.OnEnemyDie);
        ToggleRagdoll(false);

    }

    private void ToggleRagdoll(bool isActive)
    {
        if (childColliders.Length > 0)
            foreach (var item in childColliders)
            {
                item.isTrigger = !isActive;
            }

        if (childRigidbodies.Length > 0)
            foreach (var item in childRigidbodies)
            {
                item.isKinematic = !isActive;
            }
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
        foreach (var item in childRigidbodies)
        {
            if (Random.Range(0f, 1f) < 0.5f) // 50% chance to apply force
            {
                item.AddForce(Vector3.forward * force, ForceMode.Force);
            }
        }

        StartCoroutine(DisableRagdollCor());
        IEnumerator DisableRagdollCor()
        {
            yield return new WaitForSeconds(3);
            ToggleRagdoll(false);
            rb.isKinematic = false;
            col.enabled = false;
        }
    }

    public void OnEnemySpawn()
    {
        gameObject.SetActive(true);
        this.enabled = true;
        healthController.ResetHealth();

        if (rb)
            rb.isKinematic = false;
        if (col)
            col.enabled = true;
        if (animator)
            animator.enabled = true;

    }
}
