using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // PlayerComponents
    public Rigidbody2D m_Rigidbody2D;

    // Animation Variables
    public Animator m_Animator;

    // Inputs
    public float m_HorizontalInput;
    public bool m_SprintInput;

    // Movement Variables
    public float m_PlayerSpeed = 100.0f;

    private bool m_GoingRight;

    playerController m_PlayerController;

    public bool GoingRight
    {
        get
        {
            return m_GoingRight;
        }
        set
        {
            if (m_GoingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            m_GoingRight = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>(); // We take the Rigidbody in the player
        m_Animator = GetComponent<Animator>(); // We take the Animator in the player
        m_PlayerController = GetComponent<playerController>();
        m_GoingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
        animationHandler();
    }

    private void FixedUpdate()
    {
        movementHandler();
    }

    void InputHandler()
    {
        if (m_PlayerController.isInteracting) return;
        m_HorizontalInput = Input.GetAxis("Horizontal");
        if(m_PlayerController.hasEaten) m_SprintInput = Input.GetKey(KeyCode.LeftShift);
    }

    void movementHandler()
    {
        float speedMultiplier = m_SprintInput ? 2.0f : 1.0f;
        float XVelocity = m_HorizontalInput * m_PlayerSpeed * speedMultiplier;
        float YVelocity = m_Rigidbody2D.velocity.y;

        Vector2 resultingVelocity = new Vector2(XVelocity, YVelocity);

        m_Rigidbody2D.velocity = resultingVelocity;
        m_Rigidbody2D.gravityScale = (m_HorizontalInput == 0) ? 0 : 1;
    }

    void animationHandler()
    {
        if (m_HorizontalInput != 0)
        {
            m_Animator.speed = m_SprintInput ? 1.5f : 0.8f;
            m_Animator.SetBool("walking", true);
        }
        else m_Animator.SetBool("walking", false);

        if (m_GoingRight && m_HorizontalInput < 0)
        {
            GoingRight = false;
        }
        else if (!m_GoingRight && m_HorizontalInput > 0)
        {
            GoingRight = true;
        }
    }
}
