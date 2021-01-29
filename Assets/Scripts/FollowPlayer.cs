using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public static FollowPlayer s_instance;

    float distance = 0;
    float scrollSpeed = 15;
    float rotateSpeed = 2;

    Transform target = null;
    Vector3 offSetPostion;          // 镜头位置偏移量

    public bool isRotate = true;
    bool isCanRotate_h = true;      // 鼠标左右滑动
    bool isCanRotate_v = true;      // 鼠标上下滑动

    void Awake()
    {
        s_instance = this;
    }

    public void setTarget(GameObject _target)
    {
        target = _target.transform;
        offSetPostion = transform.position - target.position;
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

        transform.position = offSetPostion + target.position;
    }

    public void refreash()
    {
        if (target == null)
        {
            return;
        }

        transform.position = offSetPostion + target.position;
    }

    public void RotateView(float mouse_x, float mouse_y)
    {
        if (!isRotate)
        {
            return;
        }

        Vector3 angle = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(angle.x - mouse_y, angle.y + mouse_x, angle.z);

        Vector3 rotate = transform.eulerAngles;
        if(rotate.x < 0 || rotate.x > 350)
        {
            transform.rotation = Quaternion.Euler(0, rotate.y, rotate.z);
        }
        else if(rotate.x > 50)
        {
            transform.rotation = Quaternion.Euler(50, rotate.y, rotate.z);
        }
    }

    public void ScrollView(float value)
    {
        distance = offSetPostion.magnitude;                             // 返回向量的长度
        distance -= value * scrollSpeed;                                // 返回虚拟轴的值Mouse ScrollWheel：鼠标滑轮
        distance = Mathf.Clamp(distance, 2, 18);                        // 限制镜头最近和最远距离
        offSetPostion = offSetPostion.normalized * distance;
    }
}