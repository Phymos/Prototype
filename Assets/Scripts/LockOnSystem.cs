using NUnit.Framework;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockOnSystem : MonoBehaviour
{
    public Transform player;
    public CinemachineCamera cam;
    public LayerMask enemyLayers;
    bool lockedOn = false;

    public void OnLockOn(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (Physics.SphereCast(player.position, 2f, cam.transform.forward, out RaycastHit hit, 10f, enemyLayers) && !lockedOn)
        {
            cam.LookAt = hit.transform;
            lockedOn = true;
        }
        else
        {
            cam.LookAt = player.transform;
            lockedOn = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}
