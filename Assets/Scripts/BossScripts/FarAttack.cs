using UnityEngine;

public class FarAttack : State
{
    public JumpAttack jumpAttackState;
    public DashAttack dashAttackState;

    public override State RunCurrentState()
    {
        float randNum = Random.Range(0.0f, 1.0f);
        if(randNum > 0.5f)
        {
            BossAnimationManager.instance.SetDistanceType(2);
            BossAnimationManager.instance.SetAttackType(1);
            BossAnimationManager.instance.SetTrigger("WalkToAttack");
            jumpAttackState.SetJumpVelocity();
            return jumpAttackState;
        }
        else
        {
            BossAnimationManager.instance.SetDistanceType(2);
            BossAnimationManager.instance.SetAttackType(0);
            BossAnimationManager.instance.SetTrigger("WalkToAttack");
            dashAttackState.SetDashPosition(BossLocomotion.instance.target.transform.position);
            return dashAttackState;
        }
    }
}
