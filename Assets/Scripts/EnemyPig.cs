using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPig : EnemyScript
{
    public static EnemyScript s_instance = null;

    PlayerScript playerScript = null;

    public GameObject skill;

    float waitTime = 2;
    float walkSpeed = 1f;

    void Start()
    {
        s_instance = this; 
        playerScript = PlayerScript.s_instance;
        animator = transform.GetComponent<Animator>();
        character = transform.GetComponent<CharacterController>();
        blood_size = blood_front.GetComponent<RectTransform>().sizeDelta;

        fullBlood = 100;
        curBlood = fullBlood;
    }

    void Update()
    {
        string curAniName = getCurrentAnimatorName();
        float distance = CommonUtil.twoObjDistance_3D(gameObject, playerScript.gameObject);

        // 攻击范围内
        if (distance <= 1.5f)
        {
            switch (enemyState)
            {
                case EnemyState.Walk:
                    {
                        setState(EnemyState.Idle);
                        break;
                    }

                case EnemyState.Idle:
                    {
                        setState(EnemyState.Wait,3);
                        break;
                    }

                case EnemyState.Wait:
                    {
                        waitTime -= Time.deltaTime;
                        if (waitTime <= 0)
                        {
                            // 普攻
                            waitTime = 3;
                            setState(EnemyState.Attack);

                            // 技能
                            //waitTime = RandomUtil.getRandom(10, 15);
                            //setState(EnemyState.Skill);
                        }
                        break;
                    }

                case EnemyState.Attack:
                    {
                        if(curAniName == "Standby")
                        {
                            setState(EnemyState.Wait, 3);
                        }
                        break;
                    }

                case EnemyState.GetHit:
                    {
                        if (curAniName == "Standby")
                        {
                            setState(EnemyState.Wait, 3);
                        }
                        break;
                    }
            }
        }
        // 跟踪范围内
        else if (distance <= 8f)
        {
            // 朝向敌人
            {
                float x = playerScript.transform.position.x - transform.position.x;
                float z = playerScript.transform.position.z - transform.position.z;

                if (x == 0 && z < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else if (x < 0 && z < 0)
                {
                    transform.rotation = Quaternion.Euler(0, -Mathf.Atan(z / x) * Mathf.Rad2Deg - 90, 0);
                }
                else if (x > 0 && z < 0)
                {
                    transform.rotation = Quaternion.Euler(0, -Mathf.Atan(z / x) * Mathf.Rad2Deg + 90, 0);
                }
                else if (x > 0 && z > 0)
                {
                    transform.rotation = Quaternion.Euler(0, -Mathf.Atan(z / x) * Mathf.Rad2Deg + 90, 0);
                }
                else if (x < 0 && z > 0)
                {
                    transform.rotation = Quaternion.Euler(0, -Mathf.Atan(z / x) * Mathf.Rad2Deg - 90, 0);
                }
            }

            setState(EnemyState.Walk);
            character.Move(transform.forward * walkSpeed * Time.deltaTime);
        }
        else
        {
            setState(EnemyState.Idle);
        }
    }

    public override void setState(EnemyState _enemyState,float param = 0)
    {
        enemyState = _enemyState;
        string curAniName = getCurrentAnimatorName();
        switch (_enemyState)
        {
            case EnemyState.Idle:
                {
                    if (curAniName != "Standby")
                    {
                        animator.Play("Standby");
                    }
                    break;
                }

            case EnemyState.Wait:
                {
                    waitTime = param;
                    if (curAniName != "Standby")
                    {
                        animator.Play("Standby");
                    }
                    break;
                }

            case EnemyState.Walk:
                {
                    if (curAniName != "walk")
                    {
                        animator.Play("walk");
                    }
                    break;
                }

            case EnemyState.Attack:
                {
                    animator.Play("Attack" + RandomUtil.getRandom(1,3));
                    break;
                }

            case EnemyState.GetHit:
                {
                    animator.Play("Beaten");
                    changeBlood(-10);
                    break;
                }

            case EnemyState.Die:
                {
                    animator.Play("Death");
                    break;
                }

            case EnemyState.Skill:
                {
                    animator.Play("Roar");
                    break;
                }            
        }
    }

    public void Attack_Hit(int i)
    {
        if (checkIsAttackSuccess())
        {
            AudioScript.getInstance().playSound("Audios/atk");
            playerScript.GetHit();
        }
        else
        {
            AudioScript.getInstance().playSound("Audios/huidao");
        }
    }

    public void Skill_Hit(int i)
    {
        skill.SetActive(false);
        skill.SetActive(true);
    }

    public void GetHitEnd()
    {
    }

    void changeBlood(float value)
    {
        curBlood += value;
        curBlood = curBlood < 0 ? 0 : curBlood;
        curBlood = curBlood > fullBlood ? fullBlood : curBlood;
        blood_front.GetComponent<RectTransform>().sizeDelta = new Vector2((curBlood / fullBlood) * blood_size.x, blood_size.y);

        if (curBlood <= 0)
        {
            setState(EnemyState.Die);
        }
    }

    public bool checkIsAttackSuccess()
    {
        float distance = CommonUtil.twoObjDistance_3D(gameObject, playerScript.gameObject);
        if (distance <= 2f)
        {
            // 格挡
            if (playerScript.isGeDang())
            {
                AudioScript.getInstance().playSound("Audios/gedang");
                return false;
            }
            return true;
        }

        return false;
    }

    Transform findEnemy()
    {
        return playerScript.transform;
    }
}
