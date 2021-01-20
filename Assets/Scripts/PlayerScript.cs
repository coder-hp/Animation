using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript s_instance = null;

    public enum PlayerBehavior
    {
        Walk,
        StopWalk,
        Run,
        StopRun,
        Attack,
    }

    Animator animator;
    CharacterController character;

    // 参数
    float walkSpeed = 0.02f;
    float runSpeed = 0.07f;

    public bool moveIsWalk = true;

    void Start()
    {
        Application.targetFrameRate = 60;
        s_instance = this;
        animator = transform.GetComponent<Animator>();
        character = transform.GetComponent<CharacterController>();

        TrackGameObjScript.s_instance.setTargetObj(gameObject);
        CameraScript.s_instance.setTarget(gameObject);
    }
    
    void Update()
    {
        if (!character.isGrounded)
        {
            character.Move(Vector3.down * 10f);        // 10.0f代表重力
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
                    character.Move(transform.forward * walkSpeed * getFpsXiShu());
                    break;
                }

            case PlayerBehavior.StopWalk:
                {
                    animator.Play("WalkForward_Stop");
                    break;
                }

            case PlayerBehavior.Run:
                {
                    animator.Play("Run_Weapon");
                    transform.localRotation = Quaternion.Euler(0, angle, 0);
                    character.Move(transform.forward * runSpeed * getFpsXiShu());
                    break;
                }

            case PlayerBehavior.StopRun:
                {
                    animator.Play("RunForward_Stop");
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

    // 获取当前播放动画名称
    string getCurrentAnimatorName()
    {
        if (animator.GetCurrentAnimatorClipInfo(0).Length > 0)
        {
            return animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        }

        return "";
    }

    float getFpsXiShu()
    {
        return Time.deltaTime / (1f / 60f);
    }
}
