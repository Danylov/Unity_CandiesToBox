using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartTopMove : MonoBehaviour
{
    private GameObject heartBottom;
    private float heartBottomHeight;
    private Rigidbody heartTopRb;
    private Renderer heartTopRn;
    private Collider heartTopCol;
    private float speed = 0.001f;
    private int closed;
    private float oldDistance = 1000000;
    
    void Start()
    {
        heartBottom = GameObject.Find("HeartBottom");
        heartTopRb = GetComponent<Rigidbody>();
        heartTopRn = GetComponent<Renderer>();
        heartTopCol = GetComponent<Collider>();
        heartBottomHeight = heartBottom.GetComponent<MeshRenderer>().bounds.size.y;
        heartTopRn.enabled = false;
    }

    void Update()
    {
        if (heartTopRn.enabled)
        {
            var temp = new Vector3(0,heartBottomHeight,0);
            var heartBotModPos = heartBottom.transform.position + temp;
            var distance = Vector3.Distance(heartBotModPos, transform.position);
            if (0.025f < distance) 
            {
                if (oldDistance < distance) heartTopRb.velocity = Vector3.zero;
                heartTopRb.AddForce((heartBotModPos - transform.position) * speed, ForceMode.Impulse);
            }
            else
            {
                if (closed == 0)
                {
                    heartTopRb.velocity = Vector3.zero;
                    heartTopRb.Sleep();
                    heartTopCol.isTrigger = false;
                    heartTopRb.useGravity = true;
                    closed = 1;
                }   
            }
            oldDistance = distance;
        }
    }

    public void MoveBoxCover()
    {
        heartTopRn.enabled = true;
        heartTopRb.useGravity = false;
        heartTopCol.isTrigger = true;
        var temp = new Vector3(0,heartBottomHeight,0);
        transform.position += temp;
    }
}