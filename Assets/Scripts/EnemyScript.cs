using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Walk,
        Run,
        GetHit,
        Attack,
        Die,
    }

    public Animator animator;
    public EnemyState enemyState = EnemyState.Idle;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public virtual void setState(EnemyState _enemyState)
    {
    }
}
