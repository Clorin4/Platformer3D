using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform_cylinder_movement : MonoBehaviour
{
    public Transform center;
    public Transform initial;

    public float delay;

    private float movement_speed; 
    private bool direction;
    private Transform dir_from;
    private Transform dir_to;

    private bool moving;
    // Start is called before the first frame update
    void Start()
    {
        //initial = transform;
        movement_speed = 1.5f;

        dir_from = initial;
        dir_to = center;
        direction = true;
        moving = false;
        Invoke("start_moving", delay);
    }

    private void Update()
    {
        if (moving)
        {
            Transform A = transform;
            Transform B = dir_to;

            float step = movement_speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(
                new Vector3(A.transform.position.x, A.transform.position.y, A.transform.position.z),
                new Vector3(B.transform.position.x, A.transform.position.y, B.transform.position.z),
                step
                );

            if (
                    (
                        Mathf.Abs(transform.position.x - dir_to.position.x) +
                        Mathf.Abs(transform.position.z - dir_to.position.z)
                    )
                    < 0.5f
                )
            {
                toggle_direction();
            }
        }
    }

    private void toggle_direction()
    {
        // If going into center
        if (direction)
        {
            direction = false;
            dir_from = center;
            dir_to = initial;
        }
        else
        {
            direction = true;
            dir_from = initial;
            dir_to = center;
        }
    }

    private void start_moving()
    {
        moving = true;
    }


}
