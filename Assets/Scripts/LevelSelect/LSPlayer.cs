using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSPlayer : MonoBehaviour
{
    //Referencia al punto del mapa en el que se encuentra el jugador
    public MapPoint currentPoint;
    //Velocidad de movimiento del jugador
    public float moveSpeed = 10f;

    //Referencia al LSManager para poder acceder a su información
    private LSManager theManager;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la referencia, buscando un objeto que tenga el Script de LSManager
        theManager = FindObjectOfType<LSManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movemos al jugador de un punto a otro
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);
        //MoveTowards es moverse hacia un punto, le pasamos tres valores(punto inicial, punto al que ir, velocidad a la que ir)

        //Si la distancia entre el jugador y el siguiente punto es menor de 0.1 
        //Distance calcula la distancia entre dos puntos haciendo su resta, le pasamos dos parámetros(primer punto, segundo punto)
        if (Vector3.Distance(transform.position, currentPoint.transform.position) < .1f)
        {
            //Si pulsamos el botón de movimiento horizontal a la derecha (en el caso de un joystick a más de la mitad de su recorrido)
            if (Input.GetAxisRaw("Horizontal") > .5f)
            {
                //Si hay un punto a la derecha desde el que estamos
                if (currentPoint.right != null)//Osea si la referencia a este punto no está vacía
                {
                    //Cambiaríamos el punto actual por el de la derecha
                    SetNextPoint(currentPoint.right);
                }
            }

            //Si pulsamos el botón de movimiento horizontal a la izquierda (en el caso de un joystick a más de la mitad de su recorrido)
            if (Input.GetAxisRaw("Horizontal") < -.5f)
            {
                //Si hay un punto a la izquierda desde el que estamos
                if (currentPoint.left != null)//Osea si la referencia a este punto no está vacía
                {
                    //Cambiaríamos el punto actual por el de la izquierda
                    SetNextPoint(currentPoint.left);
                }
            }

            //Si pulsamos el botón de movimiento vertical hacia arriba (en el caso de un joystick a más de la mitad de su recorrido)
            if (Input.GetAxisRaw("Vertical") > .5f)
            {
                //Si hay un punto hacia arriba desde el que estamos
                if (currentPoint.up != null)//Osea si la referencia a este punto no está vacía
                {
                    //Cambiaríamos el punto actual por el de arriba
                    SetNextPoint(currentPoint.up);
                }
            }

            //Si pulsamos el botón de movimiento vertical hacia abajo (en el caso de un joystick a más de la mitad de su recorrido)
            if (Input.GetAxisRaw("Vertical") < -.5f)
            {
                //Si hay un punto hacia abajo desde el que estamos
                if (currentPoint.down != null)//Osea si la referencia a este punto no está vacía
                {
                    //Cambiaríamos el punto actual por el de abajo
                    SetNextPoint(currentPoint.down);
                }
            }

            //Si el MapPoint actual es un nivel y tiene un nivel que cargar
            if (currentPoint.isLevel && currentPoint.levelToLoad != "")
            {
                //Llamamos al método que muestra el panel de información del nivel
                LSUIController.sharedInstance.ShowInfo(currentPoint);

                //Si pulsamos el botón de salto
                if (Input.GetButtonDown("Jump"))
                {
                    //Llamamos al método que carga el nivel
                    theManager.LoadLevel();
                }
            }
        }
    }

    //Método para pasar al siguiente punto
    public void SetNextPoint(MapPoint nextPoint)
    {
        //El punto actual ahora será el pasado por parámetro
        currentPoint = nextPoint;
        //Llamamos al método que oculta la información del nivel
        LSUIController.sharedInstance.HideInfo();
    }
}
