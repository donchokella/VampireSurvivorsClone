using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = FindObjectOfType<PlayerController>().transform; // Normally, it is not a good option to use "FindObj..." or something similar. But, here this function runs only once. Thats why it's not a big deal.
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
