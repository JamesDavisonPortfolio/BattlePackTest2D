using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    public PauseMenu pause;

    Rigidbody2D rb;
    float vert, horz = 0;
    Vector2 moveDir;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.P))
        {
            pause.PauseGame();
        }
    }

    void FixedUpdate()
    {
        float time = Time.fixedDeltaTime * 50;
        rb.velocity = new Vector2(moveDir.x * time * speed, moveDir.y * time * speed);
    }

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.W))
            vert++;
        else if (Input.GetKeyUp(KeyCode.W))
            vert--;

        if (Input.GetKeyDown(KeyCode.S))
            vert--;
        else if (Input.GetKeyUp(KeyCode.S))
            vert++;

        if (Input.GetKeyDown(KeyCode.A))
            horz--;
        else if (Input.GetKeyUp(KeyCode.A))
            horz++;

        if (Input.GetKeyDown(KeyCode.D))
            horz++;
        else if (Input.GetKeyUp(KeyCode.D))
            horz--;

        moveDir = new Vector2(horz, vert);
        moveDir.Normalize();
    }
}
