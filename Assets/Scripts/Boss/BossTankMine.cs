using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankMine : MonoBehaviour
{
    //Referencia al efecto de explosi�n de la mina
    public GameObject explosion;

    //Cuando un objeto entra en la zona de detecci�n de la mina
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es el jugador el que entra en la zona de la mina
        if (collision.CompareTag("Player"))
        {
            //Llamamamos al m�todo que explota las minas
            Explode();
            //Hacemos da�o al jugador
            PlayerHealthController.sharedInstance.DealWithDamage();
        }
    }

    //M�todo para explotar las minas
    public void Explode()
    {
        //Destruimos la mina
        Destroy(gameObject);
        //Instanciamos el efecto de explosi�n
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
