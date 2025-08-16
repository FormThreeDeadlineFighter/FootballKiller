using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public bool Navigate { get; private set; }
    private PlayerControl _playerControl;
    private InputAction _navigateAction;
    private void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
        _playerControl = GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
