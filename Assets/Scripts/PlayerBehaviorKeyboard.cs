using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorKeyboard : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        if(PlayerScript.s_instance == null)
        {
            return;
        }

        // 镜头跟踪
        {
            float mouse_x = Input.GetAxis("Mouse X");   //  获取鼠标X轴移动
            float mouse_y = Input.GetAxis("Mouse Y");   //  获取鼠标X轴移动

            if (mouse_x != 0 || mouse_y != 0)
            {
                FollowPlayer.s_instance.RotateView(mouse_x, mouse_y);
            }
        }

        // 镜头远近
        {
            float value = Input.GetAxis("Mouse ScrollWheel");
            if (value != 0)
            {
                FollowPlayer.s_instance.ScrollView(value);
            }
        }

        // 按键
        {
            // 人物移动相关
            {
                float cameraAngle_y = CameraScript.s_instance.transform.eulerAngles.y;

                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
                {
                    PlayerScript.s_instance.playerBehaviorParam.float_1 = -45 + cameraAngle_y;
                    PlayerScript.s_instance.actionInput(PlayerScript.s_instance.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }
                else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
                {
                    PlayerScript.s_instance.playerBehaviorParam.float_1 = 45 + cameraAngle_y;
                    PlayerScript.s_instance.actionInput(PlayerScript.s_instance.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }
                else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
                {
                    PlayerScript.s_instance.playerBehaviorParam.float_1 = -135 + cameraAngle_y;
                    PlayerScript.s_instance.actionInput(PlayerScript.s_instance.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }
                else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
                {
                    PlayerScript.s_instance.playerBehaviorParam.float_1 = 135 + cameraAngle_y;
                    PlayerScript.s_instance.actionInput(PlayerScript.s_instance.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    PlayerScript.s_instance.playerBehaviorParam.float_1 = 0 + cameraAngle_y;
                    PlayerScript.s_instance.actionInput(PlayerScript.s_instance.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    PlayerScript.s_instance.playerBehaviorParam.float_1 = -90 + cameraAngle_y;
                    PlayerScript.s_instance.actionInput(PlayerScript.s_instance.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    PlayerScript.s_instance.playerBehaviorParam.float_1 = -180 + cameraAngle_y;
                    PlayerScript.s_instance.actionInput(PlayerScript.s_instance.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    PlayerScript.s_instance.playerBehaviorParam.float_1 = 90 + cameraAngle_y;
                    PlayerScript.s_instance.actionInput(PlayerScript.s_instance.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }

                if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
                {
                    PlayerScript.s_instance.actionInput(PlayerScript.s_instance.moveIsWalk ? PlayerScript.PlayerBehavior.StopWalk : PlayerScript.PlayerBehavior.StopRun);
                }

                // 走路/跑步 切换
                if (Input.GetKeyUp(KeyCode.LeftControl))
                {
                    PlayerScript.s_instance.moveIsWalk = !PlayerScript.s_instance.moveIsWalk;
                }
            }
        }

        // 鼠标
        {
            bool isMouseButton0 = Input.GetMouseButtonDown(0);
            bool isMouseButton1 = Input.GetMouseButtonDown(1);
            bool isMouseButton2 = Input.GetMouseButtonDown(2);
            if (isMouseButton0 || isMouseButton1)
            {
                string currentAnimatorName = PlayerScript.s_instance.getCurrentAnimatorName();

                if (currentAnimatorName == "LightAttk1")
                {
                    if (ActionEventFrame.s_instance.LightAttk1 == ActionEventFrame.ComboState.WaitInput)
                    {
                        ActionEventFrame.s_instance.LightAttk1 = isMouseButton0 ? ActionEventFrame.ComboState.InputSuccess : ActionEventFrame.ComboState.InputFail;
                    }
                }
                else if (currentAnimatorName == "LightAttk2")
                {
                    if (ActionEventFrame.s_instance.LightAttk2 == ActionEventFrame.ComboState.WaitInput)
                    {
                        ActionEventFrame.s_instance.LightAttk2 = isMouseButton0 ? ActionEventFrame.ComboState.InputSuccess : ActionEventFrame.ComboState.InputFail;
                    }
                }
                else if (currentAnimatorName == "LightAttk3")
                {
                    if (ActionEventFrame.s_instance.LightAttk3 == ActionEventFrame.ComboState.WaitInput)
                    {
                        ActionEventFrame.s_instance.LightAttk3 = isMouseButton0 ? ActionEventFrame.ComboState.InputSuccess : ActionEventFrame.ComboState.InputFail;
                    }
                }
                else if (currentAnimatorName == "LightAttk4")
                {
                }
                else if (currentAnimatorName == "Stab1")
                {
                    if (ActionEventFrame.s_instance.Stab1 == ActionEventFrame.ComboState.WaitInput)
                    {
                        ActionEventFrame.s_instance.Stab1 = isMouseButton1 ? ActionEventFrame.ComboState.InputSuccess : ActionEventFrame.ComboState.InputFail;
                    }
                }
                else if (currentAnimatorName == "Stab2")
                {
                    if (ActionEventFrame.s_instance.Stab2 == ActionEventFrame.ComboState.WaitInput)
                    {
                        ActionEventFrame.s_instance.Stab2 = isMouseButton1 ? ActionEventFrame.ComboState.InputSuccess : ActionEventFrame.ComboState.InputFail;
                    }
                }
                else if (currentAnimatorName == "Stab3")
                {
                }
                else
                {
                    if (isMouseButton0)
                    {
                        PlayerScript.s_instance.playerBehaviorParam.int_1 = 1;
                        PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.LightAttk);
                    }
                    else if (isMouseButton1)
                    {
                        PlayerScript.s_instance.playerBehaviorParam.int_1 = 1;
                        PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Stab);
                    }
                }
            }
            // 格挡
            else if(isMouseButton2)
            {
                PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Block);
            }
        }
    }
}
