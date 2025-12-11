using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float walkSpeed = 2f;
    public float runSpeed = 5f;

    [Header("Ragdoll")]
    public PlayerRagdoll playerRagdoll; // Arrastrar el objeto con PlayerRagdoll en el Inspector

    void Start()
    {
        animator.applyRootMotion = false;
        Debug.Log("Start Root Motion desactivado: " + animator.applyRootMotion);
    }

    void Update()
    {
        // Si el Ragdoll está activo, no permitir movimiento ni animaciones
        if (playerRagdoll != null && playerRagdoll.IsRagdoll)
            return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(h, 0, v);

        // Debug de Input
        Debug.Log("Input H: " + h + " | V: " + v + " | Magnitude: " + input.magnitude);

        bool running = Input.GetKey(KeyCode.LeftShift);

        float speed = running ? runSpeed : walkSpeed;

        // Movimiento físico
        if (input.sqrMagnitude > 0.01f)
        {
            transform.Translate(input.normalized * speed * Time.deltaTime, Space.World);
            Debug.Log("MOVIENDO personaje con speed: " + speed);

            Quaternion targetRot = Quaternion.LookRotation(input);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 10f * Time.deltaTime);
        }
        else
        {
            Debug.Log("Player sin movimiento (Idle)");
        }

        // ANIMACIONES
        float animSpeed = 0f;

        if (input.sqrMagnitude > 0.01f)
            animSpeed = running ? 1f : 0.3f;

        animator.SetFloat("Speed", animSpeed);

        // Debug Speed en Animator
        Debug.Log("Animator Speed PARAMETER = " + animator.GetFloat("Speed"));
    }
}
