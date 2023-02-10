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

    //Referencia al Animator del jugador
    private Animator anim;
    //Referencia al SpriteRenderer del jugador
    private SpriteRenderer theSR;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el RigidBody del jugador
        theRB = GetComponent<Rigidbody2D>();
        //Rellenamos la referencia del Animator del jugador, porque accedemos a ese componente del jugador usando GetComponent
        anim = GetComponent<Animator>();
        //Rellenamos la referencia del SpriteRenderer del jugador
        theSR = GetComponent<SpriteRenderer>();
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

        //Girar el sprite del jugador seg�n su direcci�n de movimiento
        //Si el jugador se mueve hacia la izquierda
        if (theRB.velocity.x < 0)
        {
            //No cambiamos la direcci�n del sprite
            theSR.flipX = false;
        }
        //Si el jugador por el contrario se est� moviendo hacia la derecha
        else if (theRB.velocity.x > 0)
        {
            //Cambiamos la direcci�n del sprite
            theSR.flipX = true;
        }

        //ANIMACIONES DEL JUGADOR
        //Cambiamos el valor del par�metro del Animator "moveSpeed", dependiendo del valor en X de la velocidad de Rigidbody
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));//Mathf.Abs hace que un valor negativo sea positivo, lo que nos permite que al movernos a la izquierda tambi�n se anime esta acci�n
        //Cambiamos el valor del par�metro del Animator "isGrounded", dependiendo del valor de la booleana del c�digo "isGrounded"
        anim.SetBool("isGrounded", isGrounded);
    }
}
