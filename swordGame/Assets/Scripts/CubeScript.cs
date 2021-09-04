using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{

    public Transform player1;
    public Transform player2;
    public Vector3 pos;
    public Vector3 distVec;
    private Vector3 height = new Vector3(0f, 1f, 0f) * 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        pos = (player1.position + player2.position)/2;
        distVec = player1.position - player2.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player2 != null && player1 != null)
        {
            distVec = player1.position - player2.position;
            pos = (player1.position + player2.position) / 2;
            transform.position = pos + height;
        }
       
    }
}
