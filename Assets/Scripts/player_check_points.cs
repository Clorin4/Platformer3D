using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_check_points : MonoBehaviour
{
    public GameObject player;
    public GameObject checkpoints;

    public Transform[] puntos;
    public Transform current_check;
    public int current_check_index;

    void Start()
    {
        // Load and move character into the desired check-point
        int initial_position = 0;
        

        puntos = load_checkpoints();
        current_check = puntos[initial_position];
        current_check_index = initial_position;
        player.transform.position = current_check.transform.position;
    }

    private Transform[] load_checkpoints()
    {
        int numero_checks = checkpoints.transform.childCount;
        Transform[] puntos = new Transform[numero_checks];
        for (int i = 0; i < numero_checks; i++)
        {
            puntos[i] = checkpoints.transform.GetChild(i); 
        }
        return puntos;
    }

    public void update_current_check(int nuevo_check)
    {
        current_check = puntos[nuevo_check];
        current_check_index = nuevo_check;
    }

    public void move_player_to_current_check()
    {
        //Debug.Log(current_check);
        player.transform.position = current_check.transform.position;
    }

}
