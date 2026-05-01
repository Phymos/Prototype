using UnityEngine;

public class GrabbableObject : MonoBehaviour, IInteractable
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Interact()
    {
        rb.isKinematic = true;
    }
}
