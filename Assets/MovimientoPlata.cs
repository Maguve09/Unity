using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlata : MonoBehaviour
{
    public Transform[] objects;
    public Transform[] waypoints;
    public float speed = 2.0f;

    private bool[] isMoving;
    bool isTrigger = false;

    void Start()
    {
        isMoving = new bool[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            isMoving[i] = false;
        }
    }

    void Update()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (isMoving[i])
            {
                objects[i].position = Vector3.MoveTowards(objects[i].position, waypoints[i].position, speed * Time.deltaTime);
            }

            if (Vector3.Distance(objects[i].position, waypoints[i].position) < 0.1f)
            {
                isMoving[i] = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && isTrigger == true)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                isMoving[i] = true;
            }
        }
    }

    void OnTriggerStay(Collider Player)
    { 
      isTrigger = true;
    }
 
}
