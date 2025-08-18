using UnityEngine;
using UnityEngine.InputSystem;

public class EasyAnimator : MonoBehaviour
{
    public Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _animator.SetTrigger("JumpTrigger");
        }
    }
}
