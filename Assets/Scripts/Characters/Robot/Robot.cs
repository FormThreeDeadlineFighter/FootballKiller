using UnityEngine;
using UnityEngine.InputSystem;
public class Robot : MonoBehaviour
{
    [SerializeField] private Transform _robotPosition;
    [SerializeField] private Transform _blockPosition;
    InputAction _blockAction;
    Animator _animator;
    void Start()
    {
        _blockAction = InputSystem.actions.FindAction("Block");
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (_blockAction.IsPressed())
        {
            OnBlock();
        }
        else
        {
            OnMove();
        }
    }
    void OnMove()
    {
        _animator.SetBool("defense", false);
        this.gameObject.transform.position = _robotPosition.transform.position;
        this.gameObject.transform.rotation = _robotPosition.transform.rotation * Quaternion.Euler(0, 0, 0);
    }
    void OnBlock()
    {
        _animator.SetBool("defense", true);
        this.gameObject.transform.position = _blockPosition.transform.position;
        this.gameObject.transform.rotation = _blockPosition.transform.rotation * Quaternion.Euler(0, 0, 0);
    }
}
