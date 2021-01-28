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

    public void RotateView(float mouse_x, float mouse_y)
    {
        if (isCanRotate_h)
        {
            if (mouse_x != 0)
            {
                transform.RotateAround(target.position, target.up, rotateSpeed * mouse_x);
            }
        }

        if (isCanRotate_v)
        {
            if (mouse_y != 0)
            {
                Vector3 originalPos = transform.position;
                Quaternion originalRotation = transform.rotation;

                transform.RotateAround(target.position, transform.right, -rotateSpeed * mouse_y);
                float x = transform.eulerAngles.x;
                if (x > 60 || x < -10)
                {
                    transform.position = originalPos;
                    transform.rotation = originalRotation;
                }
            }
        }

        offSetPostion = transform.position - target.position;
        transform.position = offSetPostion + target.position;
    }

    public void ScrollView(float value)
    {
        distance = offSetPostion.magnitude;                             // 返回向量的长度
        distance -= value * scrollSpeed;                                // 返回虚拟轴的值Mouse ScrollWheel：鼠标滑轮
        distance = Mathf.Clamp(distance, 2, 18);                        // 限制镜头最近和最远距离
        offSetPostion = offSetPostion.normalized * distance;
    }
}