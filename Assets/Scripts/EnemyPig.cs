using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPig : EnemyScript
{
    public static EnemyScript s_instance = null;

    PlayerScript playerScript = null;

    public bool isStartFight = false;

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
        // 攻击倒计时
        if(isStartFight)
        {
            restAttackTime -= Time.deltaTime;
            if (restAttackTime <= 0)
            {
                restAttackTime = RandomUtil.getRandom(1,4);
                setState(EnemyState.Attack);
            }
        }
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
        // 格挡
        if(playerScript.isGeDang())
        {
            AudioScript.getInstance().playSound("Audios/gedang");
            return false;
        }

        float distance = CommonUtil.twoObjDistance_3D(gameObject, playerScript.gameObject);
        if (distance <= 2f)
        {
            return true;
        }

        return false;
    }
}
