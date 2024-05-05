using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform targetObj;

    private Vector3 offset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        offset = this.transform.localPosition - targetObj.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        FollowTheObj();
    }

    private void FollowTheObj()
    {
        Vector3 targetPos = targetObj.transform.localPosition + offset;
        this.transform.localPosition = targetPos;
    }
}
