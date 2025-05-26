using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool IsAttacking => Input.GetKey(KeyCode.Mouse0);
    public bool IsMoving => Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
}