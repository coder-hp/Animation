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

    public List<ComboState> LightAttkState_list = new List<ComboState>() { ComboState.Disable, ComboState.Disable, ComboState.Disable, ComboState.Disable };
    public List<ComboState> StrongAttkState_list = new List<ComboState>() { ComboState.Disable, ComboState.Disable, ComboState.Disable, ComboState.Disable };

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

    public void LightAttk_Wait_Combo(int i)
    {
        LightAttkState_list[i - 1] = ComboState.WaitInput;
    }

    public void LightAttk_End(int i)
    {
        if (LightAttkState_list[i - 1] == ComboState.InputSuccess)
        {
            PlayerScript.s_instance.LightAttk(i + 1);
        }
        else
        {
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Idle);
        }
        LightAttkState_list[i - 1] = ComboState.Disable;
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

    public void StrongAttk_Wait_Combo(int i)
    {
        StrongAttkState_list[i - 1] = ComboState.WaitInput;
    }

    public void StrongAttk_End(int i)
    {
        if (StrongAttkState_list[i - 1] == ComboState.InputSuccess)
        {
            PlayerScript.s_instance.StrongAttk(i + 1);
        }
        else
        {
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Idle);
        }
        StrongAttkState_list[i - 1] = ComboState.Disable;
    }

    public void Stab_End(int i)
    {
        actionEndTime = CommonUtil.getTimeStamp_Millisecond();
        actionEndName = "Stab" + i;
    }
}
