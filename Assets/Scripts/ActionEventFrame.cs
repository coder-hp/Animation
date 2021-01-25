using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEventFrame : MonoBehaviour
{
    public enum ComboState
    {
        Disable,
        WaitInput,
        InputSuccess,
        InputFail,
    }

    public static ActionEventFrame s_instance = null;

    PlayerScript playerScript = null;
    public long actionEndTime = 0;
    public string actionEndName = "";

    void Awake()
    {
        s_instance = this;
    }

    void Start()
    {
        playerScript = PlayerScript.s_instance;
    }

    public void walkSound()
    {
        AudioScript.getInstance().playSound("Audios/walk");
    }

    public void rotateWeaopn(int i)
    {
        playerScript.weapon.localScale = new Vector3(1,1,i);
    }

    public void LightAttk_Hit(int i)
    {
        if (playerScript.checkIsAttackSuccess())
        {
            AudioScript.getInstance().playSound("Audios/atk");
            EnemyPig.s_instance.setState(EnemyScript.EnemyState.GetHit);
        }
        else
        {
            AudioScript.getInstance().playSound("Audios/huidao");
        }
    }

    public void LightAttk_End(int i)
    {
        actionEndTime = CommonUtil.getTimeStamp_Millisecond();
        actionEndName = "LightAttk" + i;
    }

    public void StrongAttk_Hit(int i)
    {
        if (playerScript.checkIsAttackSuccess())
        {
            AudioScript.getInstance().playSound("Audios/atk");
            EnemyPig.s_instance.setState(EnemyScript.EnemyState.GetHit);
        }
        else
        {
            AudioScript.getInstance().playSound("Audios/huidao");
        }
    }

    public void StrongAttk_End(int i)
    {
        actionEndTime = CommonUtil.getTimeStamp_Millisecond();
        actionEndName = "StrongAttk" + i;
    }

    public void Stab_End(int i)
    {
        actionEndTime = CommonUtil.getTimeStamp_Millisecond();
        actionEndName = "Stab" + i;
    }
}
