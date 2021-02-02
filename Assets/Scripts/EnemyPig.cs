using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPig : EnemyScript
{
    public static EnemyScript s_instance = null;

    PlayerScript playerScript = null;

    public bool isStartFight = true;
    public GameObject skill;

    float restAttackTime = 2;

    void Start()
    {
        s_instance = this; 
        playerScript = PlayerScript.s_instance;
        animator = transform.GetComponent<Animator>(); 
        blood_size = blood_front.GetComponent<RectTransform>().sizeDelta;

        fullBlood = 100;
        curBlood = fullBlood;
    }

    void Update()
    {
        //Transform enemy = findEnemy();
        //transform.DOMove(enemy.position,5).SetEase(Ease.Linear);

        // 攻击倒计时
        //if (isStartFight)
        //{
        //    restAttackTime -= Time.deltaTime;
        //    if (restAttackTime <= 0)
        //    {
        //        // 普攻
        //        restAttackTime = RandomUtil.getRandom(3, 5);
        //        setState(EnemyState.Attack);

        //        // 技能
        //        //restAttackTime = RandomUtil.getRandom(10, 15);
        //        //setState(EnemyState.Skill);
        //    }
        //}
    }

    public override void setState(EnemyState _enemyState)
    {
        switch (_enemyState)
        {
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
