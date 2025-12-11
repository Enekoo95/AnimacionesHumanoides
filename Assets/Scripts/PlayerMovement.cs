using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float walkSpeed = 2f;
    public float runSpeed = 5f;

    [Header("Ragdoll")]
    public PlayerRagdoll playerRagdoll;

    void Update()
    {
        if (playerRagdoll != null && playerRagdoll.IsRagdoll)
            return;

        // Comprobar baile 1
        bool isDancing1 = Input.GetKey(KeyCode.M);
        animator.SetBool("isDancing1", isDancing1);

        // Comprobar baile 2
        bool isDancing2 = Input.GetKey(KeyCode.B);
        animator.SetBool("isDancing2", isDancing2);

        // Mientras baila cualquiera, no moverse
        if (isDancing1 || isDancing2)
        {
            animator.SetFloat("Speed", 0f);
            return;
        }

        // Movimiento normal
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 input = new Vector3(h, 0, v);

        bool running = Input.GetKey(KeyCode.LeftShift);
        float speed = running ? runSpeed : walkSpeed;

        if (input.sqrMagnitude > 0.01f)
        {
            transform.Translate(input.normalized * speed * Time.deltaTime, Space.World);
            Quaternion targetRot = Quaternion.LookRotation(input);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 10f * Time.deltaTime);
        }

        float animSpeed = 0f;
        if (input.sqrMagnitude > 0.01f)
            animSpeed = running ? 1f : 0.3f;

        animator.SetFloat("Speed", animSpeed);
    }
}
