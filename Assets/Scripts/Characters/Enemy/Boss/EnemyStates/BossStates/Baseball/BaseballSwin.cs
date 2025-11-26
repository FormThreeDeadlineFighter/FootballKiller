using System.Collections;
using UnityEngine;

[System.Serializable]
public class BaseballSwin : StateMachineBehaviour
{
    [SerializeField] GameObject _bullet;
    [SerializeField] AttackData _attackData; 
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    // 
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Rigidbody rb = animator.GetComponentInParent<Rigidbody>();
        EnemyController enemy = animator.GetComponentInParent<EnemyController>();

        enemy.FaceToPlayer();

        foreach(BulletArrayData bulletsArray in _attackData._bulletsArray)
        {
            foreach(BulletData bullet in bulletsArray._bullets)
            {
                Vector3 lookDir = Quaternion.Euler(bullet._angle.y, bullet._angle.x, 0) * rb.transform.forward;
                Quaternion toRotation = Quaternion.LookRotation(lookDir);
                Instantiate(this._bullet, rb.transform.position + bullet._position, toRotation);
            }
        }
        
        Debug.Log("boss swin attack");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
