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

    public long actionEndTime = 0;
    public string actionEndName = "";

    void Awake()
    {
        s_instance = this;
    }

    public void walkSound()
    {
        AudioScript.getInstance().playSound("Audios/walk");
    }

    public void fanWeaopn()
    {
        PlayerScript.s_instance.weapon.localScale = new Vector3(1,1,-1);
    }

    public void zhengWeaopn()
    {
        PlayerScript.s_instance.weapon.localScale = new Vector3(1, 1, 1);
    }

    public void LightAttk1_End()
    {
        actionEndTime = CommonUtil.getTimeStamp_Millisecond();
        actionEndName = "LightAttk1";
    }

    public void LightAttk2_End()
    {
        actionEndTime = CommonUtil.getTimeStamp_Millisecond();
        actionEndName = "LightAttk2";
    }

    public void LightAttk3_End()
    {
        actionEndTime = CommonUtil.getTimeStamp_Millisecond();
        actionEndName = "LightAttk3";
    }

    public void LightAttk4_End()
    {
        actionEndTime = CommonUtil.getTimeStamp_Millisecond();
        actionEndName = "LightAttk4";
    }

    public void Stab1_End()
    {
        actionEndTime = CommonUtil.getTimeStamp_Millisecond();
        actionEndName = "Stab1";
    }

    public void Stab2_End()
    {
        actionEndTime = CommonUtil.getTimeStamp_Millisecond();
        actionEndName = "Stab2";
    }

    public void Stab3_End()
    {
        actionEndTime = CommonUtil.getTimeStamp_Millisecond();
        actionEndName = "Stab3";
    }
}
