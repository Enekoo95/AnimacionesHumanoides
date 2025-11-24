using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float inputMag = new Vector2(h, v).magnitude;

        bool running = Input.GetKey(KeyCode.LeftShift);

        float speed = running ? inputMag * 5f : inputMag * 2f;

        animator.SetFloat("Speed", speed);
        animator.SetBool("isRunning", running);
    }

    void OnAnimatorMove()
    {
        transform.position += animator.deltaPosition;
        transform.rotation *= animator.deltaRotation;
    }
}
