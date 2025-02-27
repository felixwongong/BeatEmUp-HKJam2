using System;
using UnityEngine;

public class DeadState: CharacterState
{
    public override CharacterStateId[] stateBlacklist => (CharacterStateId[])Enum.GetValues(typeof(CharacterStateId));
    public override CharacterStateId Id => CharacterStateId.Dead;
    protected internal override void StartContext(CharacterStateMachine sm, StateParam param)
    {
        sm.Controller.SetVelocity(Vector2.zero);
        sm.Controller.Animation.Play(AnimationName.GetDirectional(AnimationName.Death, sm.Controller.LastFaceDirection));
    }
}