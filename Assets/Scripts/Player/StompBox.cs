using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{
    //Referencia al objeto que aparece al eliminar a un enemigo
    public GameObject collectible;
    //Atributo de la variable para determinar probabilidad de soltar objetos tras derrotar al enemigo
    [Range(0, 100)] public float chanceToDrop; //Range -> A�adimos un rango de valores entre un m�nimo y un m�ximo
    //Variable para aplicar una fuerza de salto
    public float bounceForce = 8;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //M�todo para detectar cuando un GO ha entrado en la zona del StompBox
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el GO es un enemigo
        if (collision.CompareTag("Enemy"))
        {
            //Mensaje para saber si hemos pisado al enemigo
            Debug.Log("Hit Enemy");
            //Llamamos al m�todo que elimina al enemigo ya que podemos acceder a sus propiedades a trav�s del Collider
            collision.gameObject.GetComponentInParent<EnemyDeath>().EnemyDeathController();
            //Llamamos al m�todo que hace rebotar al jugador
            PlayerController.sharedInstance.Bounce(PlayerController.sharedInstance.bounceForce);

            //Generamos un valor entre 0 y 100
            float dropSelect = Random.Range(0, 100f);
            //Si el valor generado es igual o menor que nuestra probabilidad dada
            if (dropSelect <= chanceToDrop)
            {
                Instantiate(collectible, collision.transform.position, collision.transform.rotation);
            }
        }
    }
}
