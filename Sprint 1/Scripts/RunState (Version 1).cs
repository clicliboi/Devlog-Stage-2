using UnityEngine;

public class RunState : StateMachine<PlayerMovement>
{
    public override void OnEnter()
    {
        base.OnEnter();

        main.animator.SetFloat("Run_Multiplier", main.StickDirection.magnitude);
        main.animator.SetTrigger("Walking");
    }

    public override void OnExit()
    {
        base.OnExit();

        main.animator.SetBool("Walking", false);
        main.animator.SetBool("Running", false);
    }

    public override void OnUpdate()
    {
        if (main.CurrentSpeed < .05f)
        {
            main.animator.SetFloat("Run_Multiplier", 1);
            main.SwitchState("IdleState");
            return;
        }

        main.animator.SetFloat("Run_Multiplier", main.StickDirection.magnitude);
        if (main.CurrentSpeed/main.MaxSpeed > .55f)
        {
            main.animator.SetBool("Running", true);
            main.animator.SetBool("Walking", false);
        }
        else
        {
            main.animator.SetBool("Walking", true);
            main.animator.SetBool("Running", false);
        }
    }
}
