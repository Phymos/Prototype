using UnityEngine;
using UnityEngine.InputSystem;

public class ElementSwitcher : MonoBehaviour
{
    bool isElementMenuOpen = false;
    public GameObject elementMenuUI;

    public void OnElementMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isElementMenuOpen = !isElementMenuOpen;
            elementMenuUI.SetActive(isElementMenuOpen);
            Time.timeScale = isElementMenuOpen ? 0.2f : 1f;
        }
    }
}