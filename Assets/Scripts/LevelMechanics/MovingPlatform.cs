using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //Array de puntos por los que se mueve la plataforma móvil
    public Transform[] points;
    //Velocidad de movimiento de la plataforma
    public float moveSpeed;
    //Variable para conocer en que punto del recorrido se encuentra la plataforma
    public int currentPoint;

    //Referencia a la posición de la plataforma
    public Transform platform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movemos la plataforma
        platform.position = Vector3.MoveTowards(platform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

        //Si la plataforma prácticamente ha llegado a su punto de destino
        if (Vector3.Distance(platform.position, points[currentPoint].position) < 0.01f)
        {
            //Pasamos al siguiente punto
            currentPoint++;

            //Comprobamos si hemos llegado al último punto del array
            if (currentPoint >= points.Length)
            {
                //Reseteamos al primer punto del array
                currentPoint = 0;
            }
        }
    }
}
