using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankMine : MonoBehaviour
{
    //Referencia al efecto de explosión de la mina
    public GameObject explosion;

    //Cuando un objeto entra en la zona de detección de la mina
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es el jugador el que entra en la zona de la mina
        if (collision.CompareTag("Player"))
        {
            //Llamamamos al método que explota las minas
            Explode();
            //Hacemos daño al jugador
            PlayerHealthController.sharedInstance.DealWithDamage();
        }
    }

    //Método para explotar las minas
    public void Explode()
    {
        //Destruimos la mina
        Destroy(gameObject);
        //Instanciamos el efecto de explosión
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
