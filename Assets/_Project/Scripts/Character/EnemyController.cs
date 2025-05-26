using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;

    private void Update()
    {
        Move(-Vector3.forward);
    }

    public void Move(Vector3 direction)
    {
        transform.position += moveSpeed * Time.deltaTime * direction;
    }
}
