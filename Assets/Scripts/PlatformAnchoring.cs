using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAnchoring : MonoBehaviour
{
    private Vector3 lastPlatformPosition;
    private Transform player;

    private void Start()
    {
        lastPlatformPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto que colisiona tiene el tag "Player".
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Verifica si el objeto que deja de colisionar tiene el tag "Player".
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 platformDelta = transform.position - lastPlatformPosition;
            player.position += platformDelta;
            lastPlatformPosition = transform.position;
        }
    }
}
//ola