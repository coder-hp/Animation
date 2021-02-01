using DG.Tweening;
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
    public Transform lookTarget = null;
    public Transform lockTargetUI = null;

    public Animator animator;
    CharacterController character;

    Vector2 blood_size;
    float fullBlood = 100;
    float curBlood = 100;

    // 可调参数
    float walkSpeed = 1.2f;
    float runSpeed = 4.2f;
    float rollSpeed = 6f;
    float fallSpeed = 1.2f;
    float actionCrossFadeTime = 0.2f;

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

        FollowPlayer.s_instance.setTarget(gameObject);
        
        moveIsWalk = false;
        blood_size = blood_front.GetComponent<RectTransform>().sizeDelta;
    }
    
    void Update()
    {
        if (!character.isGrounded)
        {
            character.Move(Vector3.down * fallSpeed * Time.deltaTime);  
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

        lookEnemy();
        FollowPlayer.s_instance.refreash();
    }

    public void actionInput(PlayerBehavior playerBehavior)
    {
        string currentAnimatorName = getCurrentAnimatorName();
        switch (playerBehavior)
        {
            case PlayerBehavior.Idle:
                {
                    animator.CrossFadeInFixedTime("StandIdle", actionCrossFadeTime);
                    break;
                }

            case PlayerBehavior.Walk:
                {
                    if (currentAnimatorName == "StandIdle" || currentAnimatorName == "Run_Weapon" || currentAnimatorName == "WalkForward_Stop")
                    {
                        animator.CrossFadeInFixedTime("WalkForward", actionCrossFadeTime * 0.5f);
                        character.Move(transform.forward * walkSpeed * Time.deltaTime);
                        transform.localRotation = Quaternion.Euler(0, playerBehaviorParam.float_1, 0);
                        //lookEnemy();
                        FollowPlayer.s_instance.refreash();
                    }

                    if (currentAnimatorName == "WalkForward")
                    {
                        character.Move(transform.forward * walkSpeed * Time.deltaTime);
                        transform.localRotation = Quaternion.Euler(0, playerBehaviorParam.float_1, 0);
                        //lookEnemy();
                        FollowPlayer.s_instance.refreash();
                    }
                    break;
                }

            case PlayerBehavior.StopWalk:
                {
                    if (currentAnimatorName == "WalkForward")
                    {
                        animator.CrossFadeInFixedTime("StandIdle", actionCrossFadeTime);
                    }
                    break;
                }

            case PlayerBehavior.Run:
                {
                    if (currentAnimatorName == "StandIdle" || currentAnimatorName == "WalkForward" || currentAnimatorName == "RunForward_Stop")
                    {
                        animator.CrossFadeInFixedTime("Run_Weapon", actionCrossFadeTime * 0.5f);
                        transform.localRotation = Quaternion.Euler(0, playerBehaviorParam.float_1, 0);
                        character.Move(transform.forward * runSpeed * Time.deltaTime);
                        //lookEnemy();
                        FollowPlayer.s_instance.refreash();
                    }

                    if (currentAnimatorName == "Run_Weapon")
                    {
                        transform.localRotation = Quaternion.Euler(0, playerBehaviorParam.float_1, 0);
                        character.Move(transform.forward * runSpeed * Time.deltaTime);
                        //lookEnemy();
                        FollowPlayer.s_instance.refreash();
                    }
                    
                    break;
                }

            case PlayerBehavior.StopRun:
                {
                    if (currentAnimatorName == "Run_Weapon")
                    {
                        animator.CrossFadeInFixedTime("StandIdle", actionCrossFadeTime);
                    }
                    break;
                }

            case PlayerBehavior.LightAttk:
                {
                    LightAttkEvent();
                    break;
                }     

            case PlayerBehavior.Stab:
                {
                    animator.Play("Stab" + playerBehaviorParam.int_1);
                    break;
                }

            case PlayerBehavior.Block:
                {
                    animator.CrossFadeInFixedTime("Block_GetHit_Right", 0.1f);
                    break;
                }

            case PlayerBehavior.Dodge_Back:
                {
                    animator.CrossFadeInFixedTime("Dodge_Back", actionCrossFadeTime);
                    break;
                }

            case PlayerBehavior.Dodge_Front:
                {
                    animator.CrossFadeInFixedTime("Dodge_Front", actionCrossFadeTime);
                    break;
                }

            case PlayerBehavior.StrongAttk:
                {
                    StrongAttkEvent();
                    break;
                }
        }
    }

    void lookEnemy()
    {
        if(lookTarget == null)
        {
            return;
        }

        Vector3 playerPos = transform.position;
        Vector3 enemyPos = lookTarget.position;

        float k = (playerPos.x - enemyPos.x) / (playerPos.z - enemyPos.z);
        float angle_y = Mathf.Atan(k) * Mathf.Rad2Deg;

        if (playerPos.z >= enemyPos.z)
        {
            angle_y -= 180;
        }

        Vector3 enemyNowAngle = FollowPlayer.s_instance.transform.eulerAngles;
        FollowPlayer.s_instance.transform.rotation = Quaternion.Euler(enemyNowAngle.x, angle_y, enemyNowAngle.z);
        lockTargetUI.localPosition = WorldToCanvasPoint(GameObject.Find("Canvas").GetComponent<Canvas>(), lookTarget.Find("Bip01/Bip01_Pelvis/lockPos").position);
    }

    public Vector2 WorldToCanvasPoint(Canvas canvas, Vector3 worldPos)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
            Camera.main.WorldToScreenPoint(worldPos), canvas.worldCamera, out pos);
        return pos;
    }

    // 碰撞回调
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
    }

    void LightAttkEvent()
    {
        string currentAnimatorName = getCurrentAnimatorName();
        if (checkIsCanAttack(currentAnimatorName))
        {
            if (currentAnimatorName == "LightAttk1")
            {
                if (ActionEventFrame.s_instance.LightAttkState_list[0] == ActionEventFrame.ComboState.WaitInput)
                {
                    ActionEventFrame.s_instance.LightAttkState_list[0] = ActionEventFrame.ComboState.InputSuccess;
                }
            }
            else if (currentAnimatorName == "LightAttk2")
            {
                if (ActionEventFrame.s_instance.LightAttkState_list[1] == ActionEventFrame.ComboState.WaitInput)
                {
                    ActionEventFrame.s_instance.LightAttkState_list[1] = ActionEventFrame.ComboState.InputSuccess;
                }
            }
            else if (currentAnimatorName == "LightAttk3")
            {
                if (ActionEventFrame.s_instance.LightAttkState_list[2] == ActionEventFrame.ComboState.WaitInput)
                {
                    ActionEventFrame.s_instance.LightAttkState_list[2] = ActionEventFrame.ComboState.InputSuccess;
                }
            }
            else if (currentAnimatorName == "LightAttk4")
            {
            }
            else
            {
                LightAttk(1);
            }
        }
    }

    public void LightAttk(int action)
    {
        animator.CrossFadeInFixedTime("LightAttk" + action, actionCrossFadeTime);
    }

    void StrongAttkEvent()
    {
        string currentAnimatorName = getCurrentAnimatorName();
        if (checkIsCanAttack(currentAnimatorName))
        {
            if (currentAnimatorName == "StrongAttk1")
            {
                if (ActionEventFrame.s_instance.StrongAttkState_list[0] == ActionEventFrame.ComboState.WaitInput)
                {
                    ActionEventFrame.s_instance.StrongAttkState_list[0] = ActionEventFrame.ComboState.InputSuccess;
                }
            }
            else if (currentAnimatorName == "StrongAttk2")
            {
                if (ActionEventFrame.s_instance.StrongAttkState_list[1] == ActionEventFrame.ComboState.WaitInput)
                {
                    ActionEventFrame.s_instance.StrongAttkState_list[1] = ActionEventFrame.ComboState.InputSuccess;
                }
            }
            else if (currentAnimatorName == "StrongAttk3")
            {
                if (ActionEventFrame.s_instance.StrongAttkState_list[2] == ActionEventFrame.ComboState.WaitInput)
                {
                    ActionEventFrame.s_instance.StrongAttkState_list[2] = ActionEventFrame.ComboState.InputSuccess;
                }
            }
            else if (currentAnimatorName == "StrongAttk4")
            {
            }
            else
            {
                StrongAttk(1);
            }
        }
    }

    public void StrongAttk(int action)
    {
        animator.CrossFadeInFixedTime("StrongAttk" + action, actionCrossFadeTime);
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

    public void setLookTarget(Transform trans)
    {
        lookTarget = trans;
        lockTargetUI.gameObject.SetActive(lookTarget ? true : false);
        if (lookTarget != null)
        {
            lookEnemy();
        }
    }
}
