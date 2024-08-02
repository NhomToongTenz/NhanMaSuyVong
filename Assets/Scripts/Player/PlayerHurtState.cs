using UnityEngine;

public class PlayerHurtState : PlayerState
{
    public PlayerHurtState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) :
        base(_player, _stateMachine, _animBoolName) {
    }

    public override void Update() {
        base.Update();

        //Debug.Log(player.fx.takeDmg);
        if(triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}

