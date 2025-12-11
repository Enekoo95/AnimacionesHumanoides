using UnityEngine;

public class PlayerIK : MonoBehaviour
{
    public Animator animator;
    public PlayerRagdoll playerRagdoll; // Para desactivar IK si está en Ragdoll

    [Header("IK activable")]
    public bool enableIK = true;

    [Header("IK de manos")]
    public bool enableHandsIK = true;
    public Transform rightHandTarget;
    public Transform leftHandTarget;
    public float handIKWeight = 1f;

    [Header("IK mirada")]
    public bool enableLookAtIK = true;
    public Transform lookAtTarget;
    public float lookAtWeight = 1f;

    void Update()
    {
        // Activar/desactivar IK de mirada con la tecla E
        if (Input.GetKeyDown(KeyCode.E))
        {
            enableLookAtIK = !enableLookAtIK;
            Debug.Log("IK Mirada activada/desactivada: " + enableLookAtIK);
        }
    }

    void OnAnimatorIK(int layerIndex)
    {
        // Debug de entrada a OnAnimatorIK
        Debug.Log("OnAnimatorIK llamado, enableIK=" + enableIK + ", IsRagdoll=" + (playerRagdoll != null ? playerRagdoll.IsRagdoll.ToString() : "null"));

        // No hacer IK si está desactivado o en Ragdoll
        if (!enableIK || (playerRagdoll != null && playerRagdoll.IsRagdoll))
            return;

        // --- IK Mirada ---
        if (enableLookAtIK && lookAtTarget != null)
        {
            animator.SetLookAtWeight(lookAtWeight);
            animator.SetLookAtPosition(lookAtTarget.position);
            Debug.Log("Aplicando IK Mirada hacia: " + lookAtTarget.position);
        }
        else if (enableLookAtIK && lookAtTarget == null)
        {
            Debug.LogWarning("enableLookAtIK activo pero lookAtTarget es null");
        }

        // --- IK Manos ---
        if (enableHandsIK)
        {
            if (rightHandTarget != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, handIKWeight);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, handIKWeight);
                animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
                animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
                Debug.Log("Aplicando IK Mano Derecha hacia: " + rightHandTarget.position);
            }
            else
            {
                Debug.LogWarning("enableHandsIK activo pero rightHandTarget es null");
            }

            if (leftHandTarget != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, handIKWeight);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, handIKWeight);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
                Debug.Log("Aplicando IK Mano Izquierda hacia: " + leftHandTarget.position);
            }
            else
            {
                Debug.LogWarning("enableHandsIK activo pero leftHandTarget es null");
            }
        }
    }
}
