using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Para obtener la posición del objetivo de la cámara
    public Transform target;

    //Referencias a las posiciones de los fondos
    public Transform farBackground, middleBackground;
    //Variables para posición mínima y máxima en vertical de la cámara
    public float minHeight, maxHeight;

    //Variable donde guardar la última posición en X que tuvo el jugador
    //private float lastXPos;
    //Referencia a la última posición del jugador en X e Y
    private Vector2 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        //Al empezar el juego la última posición del jugador será la actual
        //lastXPos = transform.position.x;
        lastPos = transform.position;
    }

    // LateUpdate is called once per frame, y los LateUpdate se hacen después de todos los Update.
    //Con lo cuál evitamos problemas de tirones de la cámara
    void LateUpdate()
    {
        //La cámara sigue al jugador sin variar su posición Z
        //transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        //Creamos una restricción entre un mínimo y un máximo para el movimiento de la cámara en Y
        //float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
        //Actualizamos el movimiento de la cámara usando las restricciones
        //transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);

        //Esta línea equivale a todas las de arriba
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
