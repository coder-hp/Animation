using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	
	public static CameraScript s_instance;

	GameObject target = null;
	float left_right_speed = 8;
	float up_down_speed = 8;

	void Awake () 
	{
		s_instance = this;

	}

	void Start()
	{
		// 把鼠标锁定到屏幕中间
		//Cursor.lockState = CursorLockMode.Confined;
	}

	public void setTarget(GameObject _target)
	{
		target = _target;
	}

	public float getRotateY()
	{
		return transform.eulerAngles.y;
	}

	void FixedUpdate()
	{
		if (target == null)
		{
			return;
		}

		var mouse_x = Input.GetAxis("Mouse X");//获取鼠标X轴移动
		var mouse_y = -Input.GetAxis("Mouse Y");//获取鼠标Y轴移动

		if(mouse_y > 1)
		{
			mouse_y = 1;
		}
		else if (mouse_y < -1)
		{
			mouse_y = -1;
		}

		if (Input.GetKey(KeyCode.Mouse1))
		{
			transform.Translate(Vector3.left * (mouse_x * 15f) * Time.deltaTime);
			transform.Translate(Vector3.up * (mouse_y * 15f) * Time.deltaTime);
		}

		//if (Input.GetKey(KeyCode.Mouse0))
		{
			// 左右
			transform.RotateAround(target.transform.position, Vector3.up, mouse_x * left_right_speed);

			Vector3 rotation = transform.eulerAngles;

			if (mouse_y < 0)
			{
				// 352为下限
				if ((rotation.x < 0) || (rotation.x >= 352 && rotation.x < 360) || (rotation.x >= 0) && (rotation.x <= 90))
				{
					// 上下
					transform.RotateAround(target.transform.position, transform.right, mouse_y * up_down_speed);
				}
			}
			else if (mouse_y > 0)
			{
				// 50为上限
				if ((rotation.x < 0) || (rotation.x >= 0 && rotation.x < 50) || (rotation.x >= 300) && (rotation.x <= 360))
				{
					// 上下
					transform.RotateAround(target.transform.position, transform.right, mouse_y * up_down_speed);
				}
			}

			TrackGameObjScript.s_instance.refreshOffset();
		}

		// 镜头拉进拉远
		{
			if (Input.GetAxis("Mouse ScrollWheel") > 0)
			{
				transform.Translate(Vector3.forward * 0.5f);
			}

			if (Input.GetAxis("Mouse ScrollWheel") < 0)
			{
				transform.Translate(Vector3.forward * -0.5f);
			}
		}
	}
}
