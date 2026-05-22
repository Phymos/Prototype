using UnityEngine;
using UnityEngine.InputSystem;

public class ElementSwitcher : MonoBehaviour
{
    bool isElementMenuOpen = false;
    public GameObject elementMenuUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
