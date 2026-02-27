using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInput : MonoBehaviour
{
    private InputAction openMenu;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private bool isMenuOpen;

    void Start()
    {
        openMenu = InputSystem.actions.FindAction("UI/Menu");
        openMenu.started += ToggleMenu;
    }

    private void OnDisable()
    {
        openMenu.started -= ToggleMenu;
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        isMenuOpen = !isMenuOpen;
        menuPanel.SetActive(isMenuOpen);

        if (isMenuOpen)
        {
            GetComponent<PlayerInput>().enabled = false; // Disable PlayerInput script
            InputSystem.actions.FindActionMap("Player").Disable(); // Disable Player action map

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            GetComponent<PlayerInput>().enabled = true;
            InputSystem.actions.FindActionMap("Player").Enable();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}