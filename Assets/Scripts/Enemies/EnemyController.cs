using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Velocidad del enemigo
    public float moveSpeed;

    //Posiciones m�s a la izquierda y m�s a la derecha que se va a poder mover el enemigo
    public Transform leftPoint, rightPoint;

    //Variable para conocer la direcci�n de movimiento del enemigo
    private bool movingRight;

    //Contadores para esperar un tiempo tras moverse y para saber cuanto tiempo se mueve el enemigo
    public float moveTime, waitTime;
    private float moveCount, waitCount;

    //Referencia al RigidBody del enemigo
    private Rigidbody2D theRB;
    //Referencia al SpriteRenderer del enemigo
    private SpriteRenderer theSR;
    //Referencia al Animator del enemigo
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el Rigidbody del enemigo
        theRB = GetComponent<Rigidbody2D>();
        //Inicializamos el SpriteRenderer del enemigo teniendo en cuenta que est� en el GO hijo
        theSR = GetComponentInChildren<SpriteRenderer>();
        //Inicializamos el Animator del enemigo
        anim = GetComponent<Animator>();

        //Sacamos el Leftpoint y el Rightpoint del objeto padre, para que no se muevan junto con este
        leftPoint.parent = null;//null es vac�o o no tiene en este caso
        rightPoint.parent = null;

        //Iniciamos el movimiento hacia la derecha
        movingRight = true;
        //Inicializamos el contador de tiempo de movimiento
        moveCount = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Si el contador de tiempo de movimiento no est� vac�o, el enemigo se puede mover
        if (moveCount > 0)
        {
            //Hacemos que el contador de tiempo de movimiento se vaya vaciando
            moveCount -= Time.deltaTime;

            //Si el enemigo se est� moviendo hacia la derecha
            if (movingRight)
            {
                //Aplicamos una velocidad hacia la derecha al enemigo
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);

                //Giramos en horizontal el sprite del enemigo
                theSR.flipX = true;

                //Si la posici�n en X del enemigo est� m�s a la derecha que el RighPoint
                if (transform.position.x > rightPoint.position.x)
                {
                    //Ya no se mover� a la derecha sino a la izquierda
                    movingRight = false;
                }
            }
            //Si el enemigo se est� moviendo hacia la izquierda
            else
            {
                //Aplicamos una velocidad hacia la izquierda al enemigo
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);

                //Mantenemos la direcci�n hacia la que mira el sprite
                theSR.flipX = false;

                //Si la posici�n en X del enemigo est� m�s a la izquierda que el LeftPoint
                if (transform.position.x < leftPoint.position.x)
                {
                    //Ya no se mover� a la izquierda sino a la derecha
                    movingRight = true;
                }
            }

            //En el momento en el que el contador de tiempo de movimiento se haya vaciado
            if (moveCount <= 0)
            {
                //Inicializamos el contador de tiempo de espera
                //waitCount = waitTime;
                waitCount = Random.Range(waitTime * .25f, waitTime * 1.25f);//Random.Range(valor m�nimo, valor m�ximo)
            }

            //Animaci�n de movimiento del enemigo
            anim.SetBool("isMoving", true);
        }
        //Si por el contrario es el contador de tiempo de espera el que est� lleno
        else if (waitCount > 0)
        {
            //Hacemos que el contador de tiempo de espera se vaya vaciando
            waitCount -= Time.deltaTime;

            //Paramos al enemigo
            theRB.velocity = new Vector2(0f, theRB.velocity.y);

            //En el momento en el que el contador de tiempo de espera se haya vaciado
            if (waitCount <= 0)
            {
                //Inicializamos el contador de tiempo de movimiento
                moveCount = moveTime;
                //moveCount = Random.Range(moveTime * .75f, moveTime * 1.25f);
            }

            //Animaci�n de espera del enemigo
            anim.SetBool("isMoving", false);
        }
    }
}
