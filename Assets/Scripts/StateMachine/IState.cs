using UnityEngine;

public interface IState 
{ 
    public virtual void EnterState() { }

    public virtual void ExitState() { }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }


}
