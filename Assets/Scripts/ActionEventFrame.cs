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

    public ComboState LightAttk1 = ComboState.Disable;
    public ComboState LightAttk2 = ComboState.Disable;
    public ComboState LightAttk3 = ComboState.Disable;

    public ComboState Stab1 = ComboState.Disable;
    public ComboState Stab2 = ComboState.Disable;

    void Awake()
    {
        s_instance = this;
    }

    public void fanWeaopn()
    {
        PlayerScript.s_instance.weapon.localScale = new Vector3(1,1,-1);
    }

    public void zhengWeaopn()
    {
        PlayerScript.s_instance.weapon.localScale = new Vector3(1, 1, 1);
    }

    public void LightAttk1_Wait_Combo()
    {
        LightAttk1 = ComboState.WaitInput;
    }

    public void LightAttk1_End()
    {
        if (LightAttk1 == ComboState.InputSuccess)
        {
            PlayerScript.s_instance.playerBehaviorParam.int_1 = 2;
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.LightAttk);
        }
        else
        {

            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Idle);
        }
        LightAttk1 = ComboState.Disable;
    }

    public void LightAttk2_Wait_Combo()
    {
        LightAttk2 = ComboState.WaitInput;
    }

    public void LightAttk2_End()
    {
        if (LightAttk2 == ComboState.InputSuccess)
        {
            PlayerScript.s_instance.playerBehaviorParam.int_1 = 3;
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.LightAttk);
        }
        else
        {
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Idle);
        }
        LightAttk2 = ComboState.Disable;
    }

    public void LightAttk3_Wait_Combo()
    {
        LightAttk3 = ComboState.WaitInput;
    }

    public void LightAttk3_End()
    {
        if (LightAttk3 == ComboState.InputSuccess)
        {
            PlayerScript.s_instance.playerBehaviorParam.int_1 = 4;
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.LightAttk);
        }
        else
        {
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Idle);
        }
        LightAttk3 = ComboState.Disable;
    }

    public void Stab1_Wait_Combo()
    {
        Stab1 = ComboState.WaitInput;
    }

    public void Stab1_End()
    {
        if (Stab1 == ComboState.InputSuccess)
        {
            PlayerScript.s_instance.playerBehaviorParam.int_1 = 2;
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Stab);
        }
        else
        {
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Idle);
        }
        Stab1 = ComboState.Disable;
    }

    public void Stab2_Wait_Combo()
    {
        Stab2 = ComboState.WaitInput;
    }

    public void Stab2_End()
    {
        if (Stab2 == ComboState.InputSuccess)
        {
            PlayerScript.s_instance.playerBehaviorParam.int_1 = 3;
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Stab);
        }
        else
        {
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Idle);
        }
        Stab2 = ComboState.Disable;
    }
}
