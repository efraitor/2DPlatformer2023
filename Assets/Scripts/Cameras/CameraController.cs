using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Para obtener la posici�n del objetivo de la c�mara
    public Transform target;

    //Referencias a las posiciones de los fondos
    public Transform farBackground, middleBackground;
    //Variables para posici�n m�nima y m�xima en vertical de la c�mara
    public float minHeight, maxHeight;

    //Variable donde guardar la �ltima posici�n en X que tuvo el jugador
    //private float lastXPos;
    //Referencia a la �ltima posici�n del jugador en X e Y
    private Vector2 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        //Al empezar el juego la �ltima posici�n del jugador ser� la actual
        //lastXPos = transform.position.x;
        lastPos = transform.position;
    }

    // LateUpdate is called once per frame, y los LateUpdate se hacen despu�s de todos los Update.
    //Con lo cu�l evitamos problemas de tirones de la c�mara
    void LateUpdate()
    {
        //La c�mara sigue al jugador sin variar su posici�n Z
        //transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        //Creamos una restricci�n entre un m�nimo y un m�ximo para el movimiento de la c�mara en Y
        //float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
        //Actualizamos el movimiento de la c�mara usando las restricciones
        //transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);

        //Esta l�nea equivale a todas las de arriba
        transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);


        //Variable que me permite conocer cuanto hay que moverse en X
        //float amountToMoveX = transform.position.x - lastXPos;
        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        //farBackground.position = farBackground.position + new Vector3(amountToMoveX, 0f, 0f);
        farBackground.position = farBackground.position + new Vector3(amountToMove.x, amountToMove.y, 0f);
        //middleBackground.position += new Vector3(amountToMoveX * .5f, 0f, 0f);
        middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * .5f;

        //lastXPos = transform.position.x;
        lastPos = transform.position;
    }
}
