using System;
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
        Block,              // 格挡 手柄待做
        Dodge_Back,         // 后翻滚 手柄待做
        Dodge_Front,        // 前翻滚 手柄待做
        StrongAttk,
    }

    public Transform weapon;
    public Transform blood_front;

    public Animator animator;
    CharacterController character;

    Vector2 blood_size;
    float fullBlood = 100;
    float curBlood = 100;

    // 可调参数
    float walkSpeed = 1.2f;
    float runSpeed = 4.2f;
    float rollSpeed = 6f;

    public bool moveIsWalk = true;
    public PlayerBehaviorParam playerBehaviorParam = new PlayerBehaviorParam();

    void Awake()
    {
        s_instance = this;
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        animator = transform.GetComponent<Animator>();
        character = transform.GetComponent<CharacterController>();

        //TrackGameObjScript.s_instance.setTargetObj(gameObject);
        //CameraScript.s_instance.setTarget(gameObject);
        FollowPlayer.s_instance.setTarget(gameObject);
        
        moveIsWalk = false;
        blood_size = blood_front.GetComponent<RectTransform>().sizeDelta;
    }
    
    void Update()
    {
        if (!character.isGrounded)
        {
            character.Move(Vector3.down * 10f);        // 10.0f代表重力
        }

        string currentAnimatorName = getCurrentAnimatorName();
        if (currentAnimatorName == "Dodge_Back")
        {
            character.Move(transform.forward * -rollSpeed * Time.deltaTime);
        }
        else if (currentAnimatorName == "Dodge_Front")
        {
            character.Move(transform.forward * rollSpeed * Time.deltaTime);
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
                        character.Move(transform.forward * walkSpeed * Time.deltaTime);
                        transform.localRotation = Quaternion.Euler(0, playerBehaviorParam.float_1, 0);
                    }

                    if (currentAnimatorName == "WalkForward")
                    {
                        character.Move(transform.forward * walkSpeed * Time.deltaTime);
                        transform.localRotation = Quaternion.Euler(0, playerBehaviorParam.float_1, 0);
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
                        character.Move(transform.forward * runSpeed * Time.deltaTime);
                        transform.localRotation = Quaternion.Euler(0, playerBehaviorParam.float_1, 0);
                    }

                    if (currentAnimatorName == "Run_Weapon")
                    {
                        character.Move(transform.forward * runSpeed * Time.deltaTime);
                        transform.localRotation = Quaternion.Euler(0, playerBehaviorParam.float_1, 0);
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
                    animator.Play("LightAttk" + playerBehaviorParam.int_1);
                    break;
                }     

            case PlayerBehavior.Stab:
                {
                    animator.Play("Stab" + playerBehaviorParam.int_1);
                    break;
                }

            case PlayerBehavior.Block:
                {
                    animator.Play("Block_GetHit_Right");
                    break;
                }

            case PlayerBehavior.Dodge_Back:
                {
                    animator.Play("Dodge_Back");
                    break;
                }

            case PlayerBehavior.Dodge_Front:
                {
                    animator.Play("Dodge_Front");
                    break;
                }

            case PlayerBehavior.StrongAttk:
                {
                    animator.Play("StrongAttk" + playerBehaviorParam.int_1);
                    break;
                }
        }
    }

    // 碰撞回调
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
    }

    public void GetHit()
    {
        animator.Play("GetHit_Up",0,0);
        changeBlood(-10);
    }

    void changeBlood(float value)
    {
        curBlood += value;
        curBlood = curBlood < 0 ? 0 : curBlood;
        curBlood = curBlood > fullBlood ? fullBlood : curBlood;
        blood_front.GetComponent<RectTransform>().sizeDelta = new Vector2((curBlood / fullBlood) * blood_size.x,blood_size.y);

        if(curBlood <= 0)
        {
            animator.Play("Dead1");
        }
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

    public bool checkIsCanAttack(string curActionName)
    {
        //List<string> list = new List<string>() { "StandIdle", "WalkForward", "WalkForward_Stop", "Run_Weapon", "RunForward", "RunForward_Stop"};
        //for (int i = 0; i < list.Count; i++)
        //{
        //    if(list[i] == curActionName)
        //    {
        //        return true;
        //    }
        //}

        //return false;

        return true;
    }

    public bool checkIsAttackSuccess()
    {
        float distance =  CommonUtil.twoObjDistance_3D(gameObject, EnemyPig.s_instance.gameObject);
        if(distance <= 1.4f)
        {
            return true;
        }

        return false;
    }

    public bool isGeDang()
    {
        if(getCurrentAnimatorName() == "Block_GetHit_Right")
        {
            return true;
        }
        return false;
    }
}
