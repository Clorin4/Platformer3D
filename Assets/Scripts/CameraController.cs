using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Referencia a la pos del jugador.
    public float rotationSpeed = 2f;
    private float mouseX; // Movimiento Horizontal del mouse;
    private float mouseY; // Movimiento Vertical del mouse;

    public float distance = 3f; // Distancia de la cámara al jugador
    public float height = 2f; // Altura de la cámara sobre el jugador

    private void Start()
    {
        // Establecer la posición inicial de la cámara en relación al jugador
        Vector3 angles = transform.eulerAngles;
        mouseX = angles.y;
        mouseY = angles.x;

        // Asegurarse de que la cámara mire al jugador desde el inicio
        RotateCamera();
    }

    private void LateUpdate()
    {
        // Capturar la entrada del mouse para rotar la cámara
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Limitar la rotación vertical de la cámara
        mouseY = Mathf.Clamp(mouseY, -45f, 45f);

        // Rotar la cámara en función de la entrada del mouse
        RotateCamera();
    }

    private void RotateCamera()
    {
        // Calcular la rotación de la cámara
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0f);

        // Calcular la posición de la cámara en función de la rotación y la distancia
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = player.position + rotation * negDistance;

        // Ajustar la altura de la cámara
        position.y = player.position.y + height;

        // Aplicar la rotación y la posición calculadas
        transform.rotation = rotation;
        transform.position = position;
    }
}
