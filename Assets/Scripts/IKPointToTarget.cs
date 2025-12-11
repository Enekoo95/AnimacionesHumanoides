using UnityEngine;

public class IK_PointToTarget : MonoBehaviour
{
    public Animator anim;
    public Transform target;   // Cubo a señalar
    public float ikWeight = 1f;

    void OnAnimatorIK(int layerIndex)
    {
        if (anim == null || target == null)
            return;

        // IK solo si presionamos Q
        if (Input.GetKey(KeyCode.Q))
        {
            // IK SOLO PARA LA MANO DERECHA
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeight);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, ikWeight);

            // La mano va hacia el cubo
            anim.SetIKPosition(AvatarIKGoal.RightHand, target.position);
            anim.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.LookRotation(target.position - anim.GetBoneTransform(HumanBodyBones.RightHand).position));

            // Hints del codo (opcional para mejor pose)
            Vector3 elbowOffset = transform.right * 0.3f; // mueve el codo hacia un lado
            anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, ikWeight);
            anim.SetIKHintPosition(AvatarIKHint.RightElbow, anim.GetBoneTransform(HumanBodyBones.RightUpperArm).position + elbowOffset);
        }
        else
        {
            // Reset de pesos cuando no se presiona Q
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 0);
        }
    }
}
