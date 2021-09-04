using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Transform transformTarget;
    public GameObject gameObjectTarget;
    private Vector3 height = new Vector3(0f, 1f, 0f)*2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.LookAt(transformTarget);
        var vec = gameObjectTarget.GetComponent<CubeScript>().distVec;
        var dist = (vec.magnitude < 5f) ? 5f : vec.magnitude;
        var crossVec = Vector3.Cross(vec, Vector3.up).normalized;
        
        var velocity = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, gameObjectTarget.GetComponent<CubeScript>().pos + crossVec * dist + height, ref velocity, 0.1f);
    }
}
