using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] Transform leftBorderTr, rightBorderTr;
    [SerializeField] InputController inputController;
    [SerializeField] WeaponController weaponController;
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] Animator animator;

    private float horizontal;

    private void Start()
    {
        inputController = GetComponent<InputController>();
        weaponController = GetComponent<WeaponController>();
        animator = GetComponentInChildren<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        //if (inputController.IsAttacking)
        if (weaponController.CheckCanShoot())
            //{
            weaponController.UseWeapon();
        //}

        if (inputController.IsMoving)
        {
            Move(moveSpeed);
        }

        animator.SetFloat("MoveDirection", horizontal);
        animator.SetBool("IsMoving", inputController.IsMoving);
    }

    private void Move(float _moveSpeed)
    {
        horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");
        transform.position += _moveSpeed * Time.deltaTime * new Vector3(horizontal, 0, 0);
    }
}

