using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Velocidad del jugador
    public float moveSpeed;
    //El rigidbody del jugador
    private Rigidbody2D theRB;
    //Fuerza de salto del jugador
    public float jumpForce;

    //Variable para saber si el jugador est� en el suelo
    private bool isGrounded;
    //Punto por debajo del jugador que tomamos como referencia para detectar el suelo
    public Transform groundCheckPoint;
    //Variable para detectar el Layer de suelo
    public LayerMask whatIsGround;

    //Variable para saber si podemos hacer doble salto
    private bool canDoubleJump;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el RigidBody del jugador
        theRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //El jugador se mueve 8 en X, y la velocidad que ya tuviera en Y
        theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

        //La variable isGrounded se har� true siempre que el c�rculo f�sico que hemos creado detecte suelo
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);//OverlapCircle(punto donde se genera el c�rculo, radio del c�rculo, layer a detectar)

        //Si se pulsa el bot�n de salto
        if (Input.GetButtonDown("Jump"))
        {
            //Si el jugador est� en el suelo
            if (isGrounded)
            {
                //El jugador salta, manteniendo su velocidad en X, y aplicamos la fuerza de salto
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                //Una vez en el suelo, reactivamos la posibilidad de doble salto
                canDoubleJump = true;
            }
            //Si el jugador no est� en el suelo
            else
            {
                //Si la variable booleana canDoubleJump es verdadera
                if (canDoubleJump)
                {
                    //El jugador salta, manteniendo su velocidad en X, y aplicamos la fuerza de salto
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    //Hacemos que no se pueda volver a saltar de nuevo
                    canDoubleJump = false;
                }
            }
        }
    }
}
