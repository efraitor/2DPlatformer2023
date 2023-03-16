using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSCameraController : MonoBehaviour
{

    //Puntos m�nimos y m�ximos entre los que se puede mover la c�mara
    public Vector2 minPos, maxPos;

    //Referencia al target de la c�mara
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate() //Ponemos LateUpdate para que este m�todo se reproduzca despu�s del Update del jugador evitando tirones de la c�mara
    {
        //Creamos una variable con la restricci�n de la posici�n en X entre un m�nimo y un m�ximo
        float xPos = Mathf.Clamp(target.position.x, minPos.x, maxPos.x);
        //Creamos una variable con la restricci�n de la posici�n en Y entre un m�nimo y un m�ximo
        float yPos = Mathf.Clamp(target.position.y, minPos.y, maxPos.y);

        //Ahora teniendo en cuenta las restricciones movemos la c�mara siguiendo al jugador
        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
}
