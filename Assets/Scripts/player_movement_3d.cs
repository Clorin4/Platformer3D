using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_movement_3d : MonoBehaviour
{
    private float ray_lenght;
    public LayerMask ground;
    public bool is_on_ground;
    public bool is_on_deadfloor;

    public float move_speed = 5.0f;
    public float rotation_speed;
    public float jump_force;
    public float vertical_speed;

    public Transform groundCheck;
    public LayerMask groundMask;
    public Transform deadFloorCheck;
    public LayerMask deadFloorMask;

    private CharacterController characterController;
    private Animator animator;

    private player_check_points player_check_points;

    public Transform camera_transform;

    public GameObject PantallaFinal;

    public bool enemigo;
    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        PantallaFinal.SetActive(false);
        characterController = GetComponent<CharacterController>();
        player_check_points = GetComponent<player_check_points>();
        animator = GetComponent<Animator>();
        ray_lenght = 0.3f;
        rotation_speed = 5.0f;
        jump_force = 10;
        is_on_deadfloor = false;
        enemigo = false;


        camera_transform = GameObject.FindGameObjectWithTag("MainCamera").transform;


        if (camera_transform == null)
        {
            Debug.LogError("No se encontró la camara");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Debug.DrawRay(this.transform.position, Vector2.down * ray_lenght, Color.red);
            is_character_on_floor();

            // Player movement
            float move_horizontal = Input.GetAxis("Horizontal");
            float move_vertical = Input.GetAxis("Vertical");

            Vector3 forward = camera_transform.forward;
            Vector3 right = camera_transform.right;
            forward.y = 0.0f;
            right.y = 0.0f;
            forward.Normalize();
            right.Normalize();

            Vector3 move_direction = forward * move_vertical + right * move_horizontal;
            is_character_moving(move_direction);
            is_character_out_map();

            if (!is_on_deadfloor)
            {
                if (move_direction != Vector3.zero)
                {
                    // Calcular la rotación en el eje Y basada en la dirección del movimiento
                    Quaternion targetRotation = Quaternion.LookRotation(move_direction);
                    targetRotation.x = 0; // Mantener la rotación vertical en 0
                    targetRotation.z = 0; // Mantener la rotación horizontal en 0

                    // Rotar el personaje hacia la dirección de movimiento
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotation_speed * Time.deltaTime);
                }

                // Mover el personaje mientras está tocando el piso
                characterController.Move(move_direction * move_speed * Time.deltaTime);
                if (Input.GetButtonDown("Jump") && is_on_ground)
                {
                    vertical_speed = jump_force;
                }

                // Gravedad
                vertical_speed += Physics.gravity.y * 2 * Time.deltaTime;

                // Movimiento vertical
                Vector3 vertical_movement = new Vector3(0.0f, vertical_speed, 0.0f);

                // Mover CharacterController
                characterController.Move(vertical_movement * Time.deltaTime);
            }
            else
            {
                return_player();
            }
        }
        
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        if (hit.collider.CompareTag("Finish"))
        {
            PantallaFinal.SetActive(true);
            canMove = false;
        }
        else
        {
            PantallaFinal.SetActive(false);
            canMove = true;
        }
    }


    public void is_character_on_floor()
    {
        if (Physics.CheckSphere(groundCheck.position, ray_lenght, groundMask))
        {
            animator.SetBool("on_ground", true);
            is_on_ground = true;
        }
        else
        {
            animator.SetBool("on_ground", false);
            is_on_ground = false;
        }
    }

    public void is_character_moving(Vector3 movimiento)
    {
        if (movimiento != Vector3.zero)
        {
            animator.SetBool("on_move", true);
        }
        else
        {
            animator.SetBool("on_move", false);
        }
    }


    public void is_character_out_map()
    {
        
        if (Physics.CheckSphere(deadFloorCheck.position, ray_lenght, deadFloorMask))
        {
            is_on_deadfloor = true;
        }
        else
        {
            is_on_deadfloor = false;
        }
    }

    public void return_player()
    {
        
        player_check_points.move_player_to_current_check();
    }
}
