using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorKeyboard : MonoBehaviour
{
    PlayerScript playerScript = null;

    void Start()
    {
        playerScript = PlayerScript.s_instance;
    }
    
    void Update()
    {
        if(playerScript == null)
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
                FollowPlayer.s_instance.ChangeCameraDistance(value);
            }
        }

        // 按键
        {
            // 人物移动相关
            {
                float cameraAngle_y = FollowPlayer.s_instance.transform.eulerAngles.y;

                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
                {
                    playerScript.playerBehaviorParam.float_1 = -45 + cameraAngle_y;
                    playerScript.actionInput(playerScript.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }
                else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
                {
                    playerScript.playerBehaviorParam.float_1 = 45 + cameraAngle_y;
                    playerScript.actionInput(playerScript.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }
                else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
                {
                    playerScript.playerBehaviorParam.float_1 = -135 + cameraAngle_y;
                    playerScript.actionInput(playerScript.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }
                else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
                {
                    playerScript.playerBehaviorParam.float_1 = 135 + cameraAngle_y;
                    playerScript.actionInput(playerScript.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    playerScript.playerBehaviorParam.float_1 = 0 + cameraAngle_y;
                    playerScript.actionInput(playerScript.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    playerScript.playerBehaviorParam.float_1 = -90 + cameraAngle_y;
                    playerScript.actionInput(playerScript.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    playerScript.playerBehaviorParam.float_1 = -180 + cameraAngle_y;
                    playerScript.actionInput(playerScript.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    playerScript.playerBehaviorParam.float_1 = 90 + cameraAngle_y;
                    playerScript.actionInput(playerScript.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run);
                }

                if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
                {
                    playerScript.actionInput(playerScript.moveIsWalk ? PlayerScript.PlayerBehavior.StopWalk : PlayerScript.PlayerBehavior.StopRun);
                }

                // 走路/跑步 切换
                if (Input.GetKeyUp(KeyCode.LeftControl))
                {
                    playerScript.moveIsWalk = !playerScript.moveIsWalk;
                }
                // 翻滚
                else if (Input.GetKeyUp(KeyCode.Space))
                {
                    playerScript.actionInput(PlayerScript.PlayerBehavior.Dodge_Front);
                }
                // 视角旋转开关
                else if(Input.GetKeyUp(KeyCode.LeftAlt))
                {
                    FollowPlayer.s_instance.isRotate = !FollowPlayer.s_instance.isRotate;
                }
                // 锁定敌人开关
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    if (playerScript.lookTarget == null)
                    {
                        playerScript.setLookTarget(EnemyPig.s_instance.transform);
                    }
                    else
                    {
                        playerScript.setLookTarget(null);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.F))
                {
                    // 格挡
                    playerScript.actionInput(PlayerScript.PlayerBehavior.Block);
                }
            }
        }

        // 鼠标
        {
            bool isMouseButton0 = Input.GetMouseButtonDown(0);
            bool isMouseButton1 = Input.GetMouseButtonDown(1);
            bool isMouseButton2 = Input.GetMouseButtonDown(2);

            // 左键
            if (isMouseButton0)
            {
                PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.LightAttk);
            }
            // 右键
            else if (isMouseButton1)
            {
                PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.StrongAttk);
            }
        }
    }
}
