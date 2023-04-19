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
    //Fuerza de rebote sobre enemigos
    public float bounceForce;

    //Variable para detener al jugador
    public bool stopInput;

    //Variable para saber si el jugador está en el suelo
    private bool isGrounded;
    //Punto por debajo del jugador que tomamos como referencia para detectar el suelo
    public Transform groundCheckPoint;
    //Variable para detectar el Layer de suelo
    public LayerMask whatIsGround;

    //Variables para el contador de tiempo del KnockBack
    public float knockBackLength, knockBackForce; //Valor que tendrá el contador de KnockBack, y la fuerza de KnockBack
    private float knockBackCounter; //Contador de KnockBack

    //Variable para saber si podemos hacer doble salto
    private bool canDoubleJump;

    //Referencia al Animator del jugador
    private Animator anim;
    //Referencia al SpriteRenderer del jugador
    private SpriteRenderer theSR;

    //Variable para conocer hacia donde mira el jugador
    public bool isLeft;
    //Variable para saber cuando el jugador puede interactuar con los objetos
    public bool canInteract = false;

    //Referencia al PauseMenu
    public PauseMenu reference;

    //Hacemos el Singleton de este script
    public static PlayerController sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

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
        //Si el juego está pausado, no funciona el movimiento. Tampoco si el jugador está parado
        if (!reference.isPaused && !stopInput)
        {
            //Si el contador de KnockBack se ha vaciado, el jugador recupera el control del movimiento
            if (knockBackCounter <= 0)
            {
                //El jugador se mueve 8 en X, y la velocidad que ya tuviera en Y
                theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

                //La variable isGrounded se hará true siempre que el círculo físico que hemos creado detecte suelo
                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);//OverlapCircle(punto donde se genera el círculo, radio del círculo, layer a detectar)

                //Si se pulsa el botón de salto
                if (Input.GetButtonDown("Jump") || Input.GetButtonDown("ButtonA"))
                {
                    //Si el jugador está en el suelo
                    if (isGrounded)
                    {
                        //El jugador salta, manteniendo su velocidad en X, y aplicamos la fuerza de salto
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        //Una vez en el suelo, reactivamos la posibilidad de doble salto
                        canDoubleJump = true;
                        //Llamamos al sonido de saltar
                        AudioManager.sharedInstance.PlaySFX(10);
                    }
                    //Si el jugador no está en el suelo
                    else
                    {
                        //Si la variable booleana canDoubleJump es verdadera
                        if (canDoubleJump)
                        {
                            //El jugador salta, manteniendo su velocidad en X, y aplicamos la fuerza de salto
                            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                            //Hacemos que no se pueda volver a saltar de nuevo
                            canDoubleJump = false;
                            //Llamamos al sonido de saltar
                            AudioManager.sharedInstance.PlaySFX(10);
                        }
                    }
                }

                //Girar el sprite del jugador según su dirección de movimiento
                //Si el jugador se mueve hacia la izquierda
                if (theRB.velocity.x < 0)
                {
                    //No cambiamos la dirección del sprite
                    theSR.flipX = false;
                    //El jugador mira a la izquierda
                    isLeft = true;
                }
                //Si el jugador por el contrario se está moviendo hacia la derecha
                else if (theRB.velocity.x > 0)
                {
                    //Cambiamos la dirección del sprite
                    theSR.flipX = true;
                    //El jugador mira a la derecha
                    isLeft = false;
                }
            }
            //Si el contador de KnockBack todavía no está vacío
            else
            {
                //Hacemos decrecer el contador en 1 cada segundo
                knockBackCounter -= Time.deltaTime;
                //Si el jugador mira a la izquierda
                if (!theSR.flipX)
                {
                    //Aplicamos un pequeño empuje a la derecha
                    theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
                }
                //Si el jugador mira a la derecha
                else
                {
                    //Aplicamos un pequeño empuje a la izquierda
                    theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
                }
            }
        }
        //ANIMACIONES DEL JUGADOR
        //Cambiamos el valor del parámetro del Animator "moveSpeed", dependiendo del valor en X de la velocidad de Rigidbody
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));//Mathf.Abs hace que un valor negativo sea positivo, lo que nos permite que al movernos a la izquierda también se anime esta acción
        //Cambiamos el valor del parámetro del Animator "isGrounded", dependiendo del valor de la booleana del código "isGrounded"
        anim.SetBool("isGrounded", isGrounded);
    }

    //Método para gestionar el KnockBack producido al jugador al hacerse daño
    public void KnockBack()
    {
        //Inicializar el contador de KnockBack
        knockBackCounter = knockBackLength;
        //Paralizamos en X al jugador y hacemos que salte en Y
        theRB.velocity = new Vector2(0f, knockBackForce);

        //Activamos el trigger del animator
        anim.SetTrigger("hurt");
    }

    //Método para que el jugador rebote 
    public void Bounce(float bounceForce)
    {
        //Impulsamos al jugador rebotando
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
        //Llamamos al sonido de saltar
        AudioManager.sharedInstance.PlaySFX(10);
    }

    //Método para parar al jugador
    public void StopPlayer()
    {
        theRB.velocity = Vector2.zero;
    }

    //Método para conocer cuando un objeto entra entra en colisión con el jugador
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si el que colisiona contra el jugador es una plataforma
        if (collision.gameObject.tag == "Platform")
        {
            //El jugador pasa a ser hijo de la plataforma
            transform.parent = collision.transform;
        }
    }

    //Método para conocer cuando dejamos de colisionar contra un objeto
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Si el objeto con el que dejamos de colisionar es una plataforma
        if (collision.gameObject.tag == "Platform")
        {
            //El jugador deja de tener padre
            transform.parent = null;
        }
    }
}
