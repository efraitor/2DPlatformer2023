using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Comprobamos si es el jugador el que ha entrado en esa zona de trigger
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Hit");
            //Buscará un objeto que tenga metido el script PlayerHealthController y realizará el método DealWithDamage
            //FindObjectOfType<PlayerHealthController>().DealWithDamage();

            //Llamamos al Singleton y usamos el método que necesitamos
            //PlayerHealthController.sharedInstance.DealWithDamage();

            collision.GetComponent<PlayerHealthController>().DealWithDamage();
        }
    }
}
