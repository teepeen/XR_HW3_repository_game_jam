using UnityEngine;
using UnityEngine.InputSystem;

public class QuitGameWithController : MonoBehaviour
{
    [Header("XR Controller Button Bindings")]
    public InputActionAsset actionAsset;  // This will hold the InputActionAsset, so you can bind actions via the Inspector
    public string quitActionName = "Quit";  // The name of the input action in the InputActionAsset

    private InputAction quitAction;

    private void OnEnable()
    {
        if (actionAsset != null)
        {
            quitAction = actionAsset.FindAction(quitActionName);
            if (quitAction != null)
            {
                quitAction.performed += QuitApplication;
                quitAction.Enable();
            }
            else
            {
                Debug.LogError("Quit action not found in the action asset!");
            }
        }
        else
        {
            Debug.LogError("InputActionAsset not assigned!");
        }
    }

    private void OnDisable()
    {
        if (quitAction != null)
        {
            quitAction.Disable();
        }
    }

    private void QuitApplication(InputAction.CallbackContext context)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

        Debug.Log("Game is quitting...");
    }
}
