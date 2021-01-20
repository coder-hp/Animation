using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorParam
{
    public float float_1 = 0;
    public int int_1 = 1;
}

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript s_instance = null;

    public enum PlayerBehavior
    {
        Idle,
        Walk,
        StopWalk,
        Run,
        StopRun,
        LightAttk,
        Stab,
    }

    public Transform weapon;

    Animator animator;
    CharacterController character;

    // 参数
    float walkSpeed = 0.02f;
    float runSpeed = 0.07f;

    public bool moveIsWalk = true;
    public PlayerBehaviorParam playerBehaviorParam = new PlayerBehaviorParam();

    void Start()
    {
        Application.targetFrameRate = 60;
        s_instance = this;
        animator = transform.GetComponent<Animator>();
        character = transform.GetComponent<CharacterController>();

        //TrackGameObjScript.s_instance.setTargetObj(gameObject);
        //CameraScript.s_instance.setTarget(gameObject);
        FollowPlayer.s_instance.setTarget(gameObject);
    }
    
    void Update()
    {
        if (!character.isGrounded)
        {
            character.Move(Vector3.down * 10f);        // 10.0f代表重力
        }
    }

    public void actionInput(PlayerBehavior playerBehavior)
    {
        string currentAnimatorName = getCurrentAnimatorName();
        switch (playerBehavior)
        {
            case PlayerBehavior.Idle:
                {
                    animator.Play("StandIdle");
                    break;
                }

            case PlayerBehavior.Walk:
                {
                    if (currentAnimatorName == "StandIdle" || currentAnimatorName == "Run_Weapon" || currentAnimatorName == "WalkForward_Stop")
                    {
                        animator.Play("WalkForward");
                        transform.localRotation = Quaternion.Euler(0, playerBehaviorParam.float_1, 0);
                        character.Move(transform.forward * walkSpeed * getFpsXiShu());
                    }

                    if (currentAnimatorName == "WalkForward")
                    {
                        transform.localRotation = Quaternion.Euler(0, playerBehaviorParam.float_1, 0);
                        character.Move(transform.forward * walkSpeed * getFpsXiShu());
                    }
                    break;
                }

            case PlayerBehavior.StopWalk:
                {
                    if (currentAnimatorName == "WalkForward")
                    {
                        animator.Play("WalkForward_Stop");
                    }
                    break;
                }

            case PlayerBehavior.Run:
                {
                    if (currentAnimatorName == "StandIdle" || currentAnimatorName == "WalkForward" || currentAnimatorName == "RunForward_Stop")
                    {
                        animator.Play("Run_Weapon");
                        transform.localRotation = Quaternion.Euler(0, playerBehaviorParam.float_1, 0);
                        character.Move(transform.forward * runSpeed * getFpsXiShu());
                    }

                    if (currentAnimatorName == "Run_Weapon")
                    {
                        transform.localRotation = Quaternion.Euler(0, playerBehaviorParam.float_1, 0);
                        character.Move(transform.forward * runSpeed * getFpsXiShu());
                    }
                    break;
                }

            case PlayerBehavior.StopRun:
                {
                    if (currentAnimatorName == "Run_Weapon")
                    {
                        animator.Play("RunForward_Stop");
                    }
                    break;
                }

            case PlayerBehavior.LightAttk:
                {
                    AudioScript.getInstance().playSound("Audios/atk");
                    animator.Play("LightAttk" + playerBehaviorParam.int_1);
                    break;
                }

            case PlayerBehavior.Stab:
                {
                    animator.Play("Stab" + playerBehaviorParam.int_1);
                    break;
                }
        }
    }

    // 碰撞回调
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
    }

    // 获取当前播放动画名称
    public string getCurrentAnimatorName()
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
