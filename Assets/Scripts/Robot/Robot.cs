using UnityEngine;
using UnityEngine.InputSystem;
public class Robot : MonoBehaviour
{
    [SerializeField] private Transform _robotPosition;
    [SerializeField] private Transform _blockPosition;
    InputAction _blockAction;
    void Start()
    {
        _blockAction = InputSystem.actions.FindAction("Block");
    }
    void Update()
    {
        if (_blockAction.IsPressed())
        {
            onBlock();
        }
        else
        {
            onMove();
        }
    }
    void onMove()
    {
        this.gameObject.transform.position = _robotPosition.transform.position;
        this.gameObject.transform.rotation = _robotPosition.transform.rotation * Quaternion.Euler(0, 180, 0);
    }
    void onBlock()
    {
        this.gameObject.transform.position = _blockPosition.transform.position;
        this.gameObject.transform.rotation = _blockPosition.transform.rotation * Quaternion.Euler(0, 180, 0);
    }
}
