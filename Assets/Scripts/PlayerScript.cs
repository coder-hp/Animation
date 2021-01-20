using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public enum PlayerBehavior
    {
        Walk,
        StopWalk,
        Run,
        StopRun,
        Attack,
    }

    public static PlayerScript s_instance = null;
    Animator animator;
    CharacterController character;

    // 参数
    float walkSpeed = 0.015f;
    float runSpeed = 0.05f;

    void Start()
    {
        s_instance = this;
        animator = transform.GetComponent<Animator>();
        character = transform.GetComponent<CharacterController>();
    }
    
    void Update()
    {
        if (!character.isGrounded)
        {
            character.Move(Vector3.down * 10.0f);        // 10.0f代表重力
        }
    }

    public void actionInput(PlayerBehavior playerBehavior,float angle = 0)
    {
        string currentAnimatorName = getCurrentAnimatorName();
        switch (playerBehavior)
        {
            case PlayerBehavior.Walk:
                {
                    animator.Play("WalkForward");
                    transform.localRotation = Quaternion.Euler(0,angle,0);
                    character.Move(transform.forward * walkSpeed);
                    break;
                }

            case PlayerBehavior.StopWalk:
                {
                    animator.Play("WalkForward_Stop");
                    break;
                }

            case PlayerBehavior.Run:
                {
                    animator.Play("Sprint");
                    transform.localRotation = Quaternion.Euler(0, angle, 0);
                    character.Move(transform.forward * runSpeed);
                    break;
                }

            case PlayerBehavior.StopRun:
                {
                    animator.Play("Sprint_Stop");
                    break;
                }

            case PlayerBehavior.Attack:
                {
                    animator.Play("LightAttk1");
                    break;
                }
        }
    }

    // 碰撞回调
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
    }

    string getCurrentAnimatorName()
    {
        if (animator.GetCurrentAnimatorClipInfo(0).Length > 0)
        {
            return animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        }

        return "";
    }
}
