using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Velocidad del enemigo
    public float moveSpeed;

    //Posiciones más a la izquierda y más a la derecha que se va a poder mover el enemigo
    public Transform leftPoint, rightPoint;

    //Variable para conocer la dirección de movimiento del enemigo
    private bool movingRight;

    //Referencia al RigidBody del enemigo
    private Rigidbody2D theRB;
    //Referencia al SpriteRenderer del enemigo
    private SpriteRenderer theSR;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el Rigidbody del enemigo
        theRB = GetComponent<Rigidbody2D>();
        //Inicializamos el SpriteRenderer del enemigo teniendo en cuenta que está en el GO hijo
        theSR = GetComponentInChildren<SpriteRenderer>();

        //Sacamos el Leftpoint y el Rightpoint del objeto padre, para que no se muevan junto con este
        leftPoint.parent = null;//null es vacío o no tiene en este caso
        rightPoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        //Si el enemigo se está moviendo hacia la derecha
        if (movingRight)
        {
            //Aplicamos una velocidad hacia la derecha al enemigo
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);

            //Giramos en horizontal el sprite del enemigo
            theSR.flipX = true;

            //Si la posición en X del enemigo está más a la derecha que el RighPoint
            if (transform.position.x > rightPoint.position.x)
            {
                //Ya no se moverá a la derecha sino a la izquierda
                movingRight = false;
            }
        }
        //Si el enemigo se está moviendo hacia la izquierda
        else
        {
            //Aplicamos una velocidad hacia la izquierda al enemigo
            theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);

            //Mantenemos la dirección hacia la que mira el sprite
            theSR.flipX = false;

            //Si la posición en X del enemigo está más a la izquierda que el LeftPoint
            if (transform.position.x < leftPoint.position.x)
            {
                //Ya no se moverá a la izquierda sino a la derecha
                movingRight = true;
            }
        }
    }
}
