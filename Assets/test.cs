using UnityEngine;

public class test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("DUMMY HIT BY: " + other.gameObject.name);
    }
}
