using System;
using UnityEngine;
using UnityEngine.Assertions;

public class HurtState : CharacterState
{
    public override CharacterStateId Id => CharacterStateId.Hurt;
    protected internal override void StartContext(CharacterStateMachine sm, StateParam param)
    {
        string animationName = String.Empty;
        if (sm.Controller.LastHorizontalDirection >= 0)
        {
            animationName = AnimationName.HurtRight;
        }
        else if (sm.Controller.LastHorizontalDirection < 0)
        {
            animationName = AnimationName.HurtLeft;
        }

        sm.Controller.Animation.playSpriteSwapAnimation(animationName, true);
        sm.Controller.Rigidbody.linearVelocity = Vector2.zero;
    }
}