using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class JumpState : StateMachine<PlayerMovement>
{
    private bool _canExit = true;
    public override void Listener()
    {
        var PlayerMap = main.PlayerMap;

        var jump = PlayerMap.FindAction("Jump");
        jump.started += ctx => {
            if (main.controller.isGrounded && !main.InAir)
            {
                main.InAir = true;
                main.SwitchState("JumpState");
            }
        };

        jump.canceled += ctx => main.InAir = false;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        _canExit = false;

        main.animator.SetBool("Jumping", true);
        main.StartCoroutine(Jump());
    }

    public override void OnExit()
    {
        base.OnExit();

        //main.StartCoroutine(Cooldown());
        main.InAir = false;
        main.animator.SetBool("Jumping", false);
    }

    //private IEnumerator Cooldown()
    //{
    //    yield return new WaitForSeconds(main.JumpCooldown);

    //    main.InCooldown = false;
    //}

    private IEnumerator Jump()
    {
        if (main.InAir)
        {
            var elapsedTime = 0f;

            var initialPower = main.JumpPower * .2f;

            main._velocity = initialPower;

            while (elapsedTime < (main.HoldDuration * .8) && main.InAir)
            {
                elapsedTime += Time.deltaTime;

                main._velocity += main.JumpPower * (1 - elapsedTime/(main.HoldDuration*.8f)) * Time.deltaTime;
                yield return null;
            }

            main.ApplyExtraGravity = true;
            _canExit = true;
        }
    }

    public override void OnUpdate()
    {
        if (!_canExit) return;
        
        if (main.controller.isGrounded)
        {
            if (main.CurrentSpeed > 0.05f)
            {
                main.SwitchState("RunState");
            }
            else
            {
                main.SwitchState("IdleState");
            }
        }
    }
}
