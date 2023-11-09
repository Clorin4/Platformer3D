using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike_movement : MonoBehaviour
{
    public Transform end;
    public Transform start;

    public float delay;
    public int movement_type;

    public float movement_speed = 20f;
    private bool direction;
    private bool moving;

    private Vector3 initialPosition;
    private Transform dir_to;

    void Start()
    {
        

        dir_to = end;
        direction = true;
        moving = false;

        // Guarda la posición inicial
        initialPosition = transform.position;

        Invoke("start_moving", delay);
    }

    void Update()
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
            float[] angle_values = get_rotation_numbers(movement_type);
            transform.Rotate(angle_values[0], angle_values[1], angle_values[2], Space.World);

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

    private void start_moving()
    {
        moving = true;
    }

    private float[] get_rotation_numbers(int type)
    {
        float[] values = new float[3];
        switch (type)
        {
            case 1:
                values[0] = 0.0f;
                values[1] = 0.2f;
                values[2] = 0.0f;
                break;
            case 2:
                values[0] = 0.5f;
                values[1] = 0.5f;
                values[2] = 0.5f;
                break;
            case 3:
                values[0] = Random.Range(0.0f, 1.0f);
                values[1] = Random.Range(0.0f, 1.0f);
                values[2] = Random.Range(0.0f, 1.0f);
                break;
        }
        return values;
    }

    private void toggle_direction()
    {
        if (direction)
        {
            direction = false;
            dir_to = start;
        }
        else
        {
            direction = true;
            dir_to = end;

            // respaunn
            transform.position = initialPosition;
        }
    }
}
