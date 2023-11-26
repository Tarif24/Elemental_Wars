using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private const string WALK_ANIMATION_BOOL = "Walk";
    private const string SPELL_ANIMATION_BOOL = "Spell";
    private const string DEATH_ANIMATION_BOOL = "Death";
    private const string ATTACK_ANIMATION_BOOL = "Attack";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void AnimateWalk() 
    {
        Animate(WALK_ANIMATION_BOOL);
    }

    public void AnimateSpell()
    {
        Animate(SPELL_ANIMATION_BOOL);
    }

    public void AnimateDeath()
    {
        Animate(DEATH_ANIMATION_BOOL);
    }

    public void AnimateAttack()
    {
        Animate(ATTACK_ANIMATION_BOOL);
    }

    private void Animate(string boolName) 
    {
        DisableOtherAnimations(animator, boolName);

        animator.SetBool(boolName, true);
    }

    private void DisableOtherAnimations(Animator animator, string animation) 
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters) 
        {
            if (parameter.name != animation) 
            {
                animator.SetBool(parameter.name,false);
            }
        }
    }

}

