using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Referencia a la pos del jugador.
    public float rotationSpeed = 2f;
    private float mouseX; // Movimiento Horizontal del mouse;
    private float mouseY; // Movimiento Vertical del mouse;

    public float distance = 3f; // Distancia de la c�mara al jugador
    public float height = 2f; // Altura de la c�mara sobre el jugador

    private void Start()
    {
        // Establecer la posici�n inicial de la c�mara en relaci�n al jugador
        Vector3 angles = transform.eulerAngles;
        mouseX = angles.y;
        mouseY = angles.x;

        // Asegurarse de que la c�mara mire al jugador desde el inicio
        RotateCamera();
    }

    private void LateUpdate()
    {
        // Capturar la entrada del mouse para rotar la c�mara
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Limitar la rotaci�n vertical de la c�mara
        mouseY = Mathf.Clamp(mouseY, -45f, 45f);

        // Rotar la c�mara en funci�n de la entrada del mouse
        RotateCamera();
    }

    private void RotateCamera()
    {
        // Calcular la rotaci�n de la c�mara
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0f);

        // Calcular la posici�n de la c�mara en funci�n de la rotaci�n y la distancia
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = player.position + rotation * negDistance;

        // Ajustar la altura de la c�mara
        position.y = player.position.y + height;

        // Aplicar la rotaci�n y la posici�n calculadas
        transform.rotation = rotation;
        transform.position = position;
    }
}
