using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPig : EnemyScript
{
    public static EnemyScript s_instance = null;

    void Start()
    {
        s_instance = this;
        animator = transform.GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public override void setState(EnemyState _enemyState)
    {
        switch (_enemyState)
        {
            case EnemyState.GetHit:
                {
                    //Beaten
                    animator.Play("Beaten");
                    break;
                }
        }
    }

    public void GetHitEnd()
    {
        Debug.Log("Beaten");
    }
}
