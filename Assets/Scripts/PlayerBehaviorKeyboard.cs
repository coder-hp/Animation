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

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Walk, -45);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Walk, 45);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Walk, -135);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Walk, 135);
        }
        else if(Input.GetKey(KeyCode.W))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Walk,0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Walk, -90);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Walk, -180);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Walk, 90);
        }

        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.StopWalk);
        }

        if(Input.GetMouseButtonDown(0))
        {
            PlayerScript.s_instance.actionInput(PlayerScript.PlayerBehavior.Attack);
        }
    }
}
