using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public static FollowPlayer s_instance;
    
    float rotateSpeed = 1.5f;

    Transform target = null;
    Vector3 offSetPostion;          // 镜头位置偏移量

    public new Transform camera;
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

        mouse_x *= rotateSpeed;
        mouse_y *= rotateSpeed;

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

    // 镜头拉近拉远
    public void ChangeCameraDistance(float value)
    {
        // 拉远
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            camera.DOLocalMove(camera.localPosition * 1.2f, 0.2f).SetEase(Ease.Linear);
            //camera.localPosition *= 1.1f;
        }
        // 拉近
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            camera.DOLocalMove(camera.localPosition * 0.8f, 0.2f).SetEase(Ease.Linear);
            //camera.localPosition *= 0.9f;
        }
    }
}