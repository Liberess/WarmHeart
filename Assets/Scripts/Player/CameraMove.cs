using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform Target;
    public float CameraZ = -10;

    private void Start()
    {
        if (Target == null)
            Target = FindObjectOfType<PlayerControl>().gameObject.transform;
    }

    private void FixedUpdate()
    {
        Vector3 TargetPos = new Vector3(Target.position.x, Target.position.y, CameraZ);
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * 5f);
    }
}
