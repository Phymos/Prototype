using NUnit.Framework;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockOnSystem : MonoBehaviour
{
    public Transform player;
    public CinemachineCamera cam;
    public CinemachineTargetGroup targetGroup;
    public LayerMask enemyLayers;
    bool lockedOn = false;
    Transform currentTarget;

    public void OnLockOn(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (Physics.SphereCast(player.position, 2f, cam.transform.forward, out RaycastHit hit, 10f, enemyLayers) && !lockedOn)
        {
            currentTarget = hit.transform;
            targetGroup.AddMember(currentTarget, 0.5f, 0.5f);
            lockedOn = true;
        }
        else
        {
            targetGroup.RemoveMember(currentTarget);
            currentTarget = null;
            lockedOn = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}
