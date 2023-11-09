using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_save_areas : MonoBehaviour
{
    public GameObject player;
    public int value;

    public player_check_points player_check_points;
    // Start is called before the first frame update
    void Start()
    {
        player_check_points = player.GetComponent<player_check_points>();
    }

    private void OnTriggerEnter(Collider other)
    {
        update_global_checkpoint();
    }

    private void update_global_checkpoint()
    {
        player_check_points.update_current_check(value);
    }
    
}
