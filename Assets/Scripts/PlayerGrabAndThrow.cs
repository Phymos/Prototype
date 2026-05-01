using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrabAndThrow : MonoBehaviour
{
    public Transform holdPoint;
    public float throwForce = 10f;

    public float grabRange = 3f;
    public bool isHoldingObject = false;
    public LayerMask grabbableLayer;

    private GrabbableObject grabbableObject;

    void Update()
    {
        if (isHoldingObject && grabbableObject != null)
        {
            grabbableObject.transform.position = holdPoint.position;
            grabbableObject.transform.rotation = holdPoint.rotation;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (isHoldingObject)
            ThrowObject();
        else
            TryGrab();
    }

    void TryGrab()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
        
        if (Physics.Raycast(ray, out RaycastHit hit, grabRange, grabbableLayer))
        {
            if (hit.collider.TryGetComponent(out GrabbableObject grabbable))
            {
                grabbableObject = grabbable;
                grabbable.Interact();
                isHoldingObject = true;
            }
        }
    }

    void ThrowObject()
    {
        if (grabbableObject != null)
        {
            Rigidbody rb = grabbableObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(Camera.main.transform.forward * throwForce, ForceMode.VelocityChange);
            grabbableObject = null;
            isHoldingObject = false;
        }
    }

}