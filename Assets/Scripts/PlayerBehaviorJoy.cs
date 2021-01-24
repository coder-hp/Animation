using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorJoy : MonoBehaviour
{
    PlayerScript playerScript = null;

    bool isCanClick_A = true;
    bool isCanClick_B = true;
    bool isCanClick_X = true;
    bool isCanClick_Y = true;

    void Start()
    {
        playerScript = PlayerScript.s_instance;
    }

    void Update()
    {
        if (playerScript == null)
        {
            return;
        }

        string currentAnimatorName = PlayerScript.s_instance.getCurrentAnimatorName();

        // 镜头移动
        {
            float mouse_x = Input.GetAxis("Right_X");
            float mouse_y = Input.GetAxis("Right_Y");

            if (mouse_x != 0 || mouse_y != 0)
            {
                FollowPlayer.s_instance.RotateView(mouse_x, -mouse_y);
            }
        }

        // 人物移动
        {
            float cameraAngle_y = CameraScript.s_instance.transform.eulerAngles.y;

            float x = Input.GetAxis("Left_X");
            float y = Input.GetAxis("Left_Y");

            if (x != 0 && y != 0)
            {
                float angle = Mathf.Atan(x / y) * Mathf.Rad2Deg;

                // 右上
                if (x > 0 && y < 0)
                {
                    playerScript.playerBehaviorParam.float_1 = -angle + cameraAngle_y;
                }
                // 右下
                else if (x > 0 && y > 0)
                {
                    playerScript.playerBehaviorParam.float_1 = (180 - angle) + cameraAngle_y;
                }
                // 左下
                else if (x < 0 && y > 0)
                {
                    playerScript.playerBehaviorParam.float_1 = (180 - angle) + cameraAngle_y;
                }
                // 左上
                else if (x < 0 && y < 0)
                {
                    playerScript.playerBehaviorParam.float_1 = -angle + cameraAngle_y;
                }

                playerScript.actionInput(playerScript.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
            }
            else if (x == 0 && y == 0)
            {
                playerScript.actionInput(playerScript.moveIsWalk ? PlayerScript.PlayerBehavior.StopWalk : PlayerScript.PlayerBehavior.StopRun);
            }
            else if (x == 0)
            {
                // 上
                if (y < 0)
                {
                    playerScript.playerBehaviorParam.float_1 = 0 + cameraAngle_y;
                    playerScript.actionInput(playerScript.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }
                // 下
                else if (y > 0)
                {
                    playerScript.playerBehaviorParam.float_1 = 180 + cameraAngle_y;
                    playerScript.actionInput(playerScript.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }
            }
            else if (y == 0)
            {
                // 左
                if (x < 0)
                {
                    playerScript.playerBehaviorParam.float_1 = 270 + cameraAngle_y;
                    playerScript.actionInput(playerScript.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }
                // 右
                else if (x > 0)
                {
                    playerScript.playerBehaviorParam.float_1 = 90 + cameraAngle_y;
                    playerScript.actionInput(playerScript.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }
            }
        }
        

        // ABXY
        //{
        //    if(Input.GetAxis("Joy_A") > 0)
        //    {
        //        if (isCanClick_A)
        //        {
        //            isCanClick_A = false;

        //            if (currentAnimatorName == "LightAttk1")
        //            {
        //                if (ActionEventFrame.s_instance.LightAttk1 == ActionEventFrame.ComboState.WaitInput)
        //                {
        //                    ActionEventFrame.s_instance.LightAttk1 = ActionEventFrame.ComboState.InputSuccess;
        //                }
        //            }
        //            else if (currentAnimatorName == "LightAttk2")
        //            {
        //                if (ActionEventFrame.s_instance.LightAttk2 == ActionEventFrame.ComboState.WaitInput)
        //                {
        //                    ActionEventFrame.s_instance.LightAttk2 = ActionEventFrame.ComboState.InputSuccess;
        //                }
        //            }
        //            else if (currentAnimatorName == "LightAttk3")
        //            {
        //                if (ActionEventFrame.s_instance.LightAttk3 == ActionEventFrame.ComboState.WaitInput)
        //                {
        //                    ActionEventFrame.s_instance.LightAttk3 = ActionEventFrame.ComboState.InputSuccess;
        //                }
        //            }
        //            else if (currentAnimatorName == "LightAttk4")
        //            {
        //            }
        //            else
        //            {
        //                PlayerScript.s_instance.playerBehaviorParam.int_1 = 1;
        //                PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.LightAttk);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        isCanClick_A = true;
        //    }

        //    if (Input.GetAxis("Joy_B") > 0)
        //    {
        //        if (isCanClick_B)
        //        {
        //            isCanClick_B = false;

        //            if (currentAnimatorName == "Stab1")
        //            {
        //                if (ActionEventFrame.s_instance.Stab1 == ActionEventFrame.ComboState.WaitInput)
        //                {
        //                    ActionEventFrame.s_instance.Stab1 = ActionEventFrame.ComboState.InputSuccess;
        //                }
        //            }
        //            else if (currentAnimatorName == "Stab2")
        //            {
        //                if (ActionEventFrame.s_instance.Stab2 == ActionEventFrame.ComboState.WaitInput)
        //                {
        //                    ActionEventFrame.s_instance.Stab2 = ActionEventFrame.ComboState.InputSuccess;
        //                }
        //            }
        //            else if (currentAnimatorName == "Stab3")
        //            {
        //            }
        //            else
        //            {
        //                PlayerScript.s_instance.playerBehaviorParam.int_1 = 1;
        //                PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Stab);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        isCanClick_B = true;
        //    }
        //}
    }
}
