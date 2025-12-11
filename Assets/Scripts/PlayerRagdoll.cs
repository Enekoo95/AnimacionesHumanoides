using UnityEngine;

public class PlayerRagdoll : MonoBehaviour
{
    public Animator animator;
    private Rigidbody[] ragdollRigidbodies;
    private Collider[] ragdollColliders;
    private bool isRagdoll = false;

    // Getter para que otros scripts puedan saber si está en Ragdoll
    public bool IsRagdoll => isRagdoll;

    void Start()
    {
        // Obtener todos los rigidbodies y colliders de los hijos
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        // Desactivar Ragdoll al inicio
        SetRagdollActive(false);
    }

    void Update()
    {
        // Activar/desactivar Ragdoll con tecla R
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isRagdoll)
                ActivateRagdoll();
            else
                DeactivateRagdoll();
        }
    }

    void SetRagdollActive(bool active)
    {
        foreach (Rigidbody rb in ragdollRigidbodies)
            rb.isKinematic = !active;

        foreach (Collider col in ragdollColliders)
        {
            if (col.gameObject != this.gameObject) // excluir el collider principal
                col.enabled = active;
        }

        animator.enabled = !active;
        isRagdoll = active;
    }

    public void ActivateRagdoll()
    {
        // Mantener posición actual del Animator antes de activar
        CopyAnimatorPoseToRagdoll();
        SetRagdollActive(true);
    }

    public void DeactivateRagdoll()
    {
        // Aplicar la pose actual del ragdoll al Animator
        CopyRagdollPoseToAnimator();
        SetRagdollActive(false);
    }

    void CopyAnimatorPoseToRagdoll()
    {
        // Opcional: mejorar para blend de transición
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.transform.position = rb.transform.position;
            rb.transform.rotation = rb.transform.rotation;
        }
    }

    void CopyRagdollPoseToAnimator()
    {
        // Aplicar pose del ragdoll al Animator
        foreach (Rigidbody rb in ragdollRigidbodies)
            rb.transform.rotation = rb.transform.rotation;
    }
}
