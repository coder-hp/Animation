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
        Skill,
        Wait,
    }

    public Vector2 blood_size;
    public float fullBlood = 100;
    public float curBlood = 100;

    public Transform blood_front;
    public Animator animator;
    public CharacterController character;
    public EnemyState enemyState = EnemyState.Idle;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public virtual void setState(EnemyState _enemyState, float param = 0)
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
}
