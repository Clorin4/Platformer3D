using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swords : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Comprueba si la colisión involucra al jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtén una referencia al script del jugador
            player_movement_3d playerScript = collision.gameObject.GetComponent<player_movement_3d>();

            // Verifica si se encontró el script en el jugador
            if (playerScript != null)
            {
                // Activa la función return_player en el jugador
                playerScript.return_player();
            }
        }
    }
}
