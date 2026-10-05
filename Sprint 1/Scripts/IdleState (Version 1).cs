using UnityEngine;

public class IdleState : StateMachine<PlayerMovement>
{
    public override void OnEnter()
    {
        base.OnEnter();

        main.animator.SetBool("Idling", true);
    }

    public override void OnExit()
    {
        base.OnExit();

        main.animator.SetBool("Idling", false);
    }

    public override void OnUpdate()
    {
        if (main.CurrentSpeed > 0.05f)
        {
            main.SwitchState("RunState");
        }
    }
}
