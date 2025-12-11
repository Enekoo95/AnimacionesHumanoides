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
            enableLookAtIK = !enableLookAtIK;
    }

    void OnAnimatorIK(int layerIndex)
    {
        // No hacer IK si está desactivado o en Ragdoll
        if (!enableIK || (playerRagdoll != null && playerRagdoll.IsRagdoll))
            return;

        // --- IK Mirada ---
        if (enableLookAtIK && lookAtTarget != null)
        {
            animator.SetLookAtWeight(lookAtWeight);
            animator.SetLookAtPosition(lookAtTarget.position);
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
            }

            if (leftHandTarget != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, handIKWeight);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, handIKWeight);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
            }
        }
    }
}
