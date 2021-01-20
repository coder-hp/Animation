using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGameObjScript : MonoBehaviour
{
    public static TrackGameObjScript s_instance = null;

    GameObject target = null;
    public float damping = 0;
    Vector3 offset;
    bool isTrack = true;

    private void Awake()
    {
        s_instance = this;
    }

    void Start()
    {
    }

    public void setTargetObj(GameObject _target)
    {
        target = _target;
        offset = transform.position - target.transform.position;
    }

    public void refreshOffset()
    {
        offset = transform.position - target.transform.position;
    }

    public void setTrackEnable(bool enable)
    {
        isTrack = enable;
    }

    void LateUpdate()
    {
        if(isTrack && (target != null))
        {
            if (damping > 0)
            {
                Vector3 desiredPosition = target.transform.position + offset;
                Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
                transform.position = position;

                transform.LookAt(target.transform.position);
            }
            else
            {
                Vector3 desiredPosition = target.transform.position + offset;
                transform.position = desiredPosition;
            }
        }
    }
}
