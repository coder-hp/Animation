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

        float cameraAngle_y = CameraScript.s_instance.transform.eulerAngles.y;

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.s_instance.moveIsWalk ? PlayerScript.PlayerBehavior.Walk: PlayerScript.PlayerBehavior.Run, -45 + cameraAngle_y);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.s_instance.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run, 45 + cameraAngle_y);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.s_instance.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run, -135 + cameraAngle_y);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.s_instance.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run, 135 + cameraAngle_y);
        }
        else if(Input.GetKey(KeyCode.W))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.s_instance.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run, 0 + cameraAngle_y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.s_instance.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run, -90 + cameraAngle_y);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.s_instance.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run, -180 + cameraAngle_y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.s_instance.moveIsWalk ? PlayerScript.PlayerBehavior.Walk : PlayerScript.PlayerBehavior.Run, 90 + cameraAngle_y);
        }

        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.StopWalk);
        }

        if(Input.GetMouseButtonDown(0))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Attack);
        }

        // 走路/跑步 切换
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            PlayerScript.s_instance.moveIsWalk = !PlayerScript.s_instance.moveIsWalk;
        }
    }
}
