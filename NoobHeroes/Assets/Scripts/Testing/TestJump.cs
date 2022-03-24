using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestJump : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public float speed = 2.0f;
    float horizontal;
    float vertical;
    Vector2 lookDirection = new Vector2(1, 0);
    Vector2 currentInput;

    PlayerInputActions playerInputActions;

    public GameObject pauseGame;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        //playerInputActions.Player.Launch.performed += LaunchProjectile;
        playerInputActions.Player.Movement.performed += OnMovement;
        playerInputActions.Player.Movement.canceled += OnMovement;
        //playerInputActions.Player.ChangeWeapon.performed += ChangeWeapon;

        //playerInputActions.Player.Talk.performed += TalkingWithJambi;
        playerInputActions.Player.Pause.performed += OnPauseGame;

        if (MyManager.Instance.isNewGame == false)
        {
            //MyManager.Instance.LoadGame(this);
        }

        pauseGame.SetActive(false);

        //pauseGame.SetActive(false);
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            rigidbody2d.velocity = Vector2.up * 2;
            
        }

        Vector2 moveDirection = transform.position;
        moveDirection.x = moveDirection.x + 2f * horizontal * Time.deltaTime * speed;


        //Flip character when moving right/left
        if (horizontal > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontal < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        rigidbody2d.MovePosition(moveDirection);

        Vector2 move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
    }

    void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentInput = context.ReadValue<Vector2>();
            Debug.Log("Moving");
        }
        if (context.canceled)
        {
            currentInput = Vector2.zero;
        }
 
    }

    void OnPauseGame(InputAction.CallbackContext context)
    {
        MyManager.Instance.PauseGame();

        //Show menu
        //pauseGame.SetActive(true);
        pauseGame.SetActive(true);
    }
}

