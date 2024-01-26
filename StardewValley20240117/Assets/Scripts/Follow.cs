using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follown : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //transform.position = offset + player.position;
        transform.position = Vector3.Lerp(transform.position, offset+player.position, Time.deltaTime);
    }
}
