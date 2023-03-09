using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //Variable para saber si este objeto es una gema o una cura
    public bool isGem, isHeal;

    //Variable para conocer si un objeto ya ha sido recogido
    private bool isCollected;

    //Referencia al objeto que aparecer� para representar el efecto de coger un item
    public GameObject pickupEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //M�todo para interactuar con los objetos al entrar en su zona
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el jugador es el que entra en la zona del objeto y este no hab�a sido recogido con anterioridad
        if (collision.CompareTag("Player") && !isCollected)
        {
            //Si el objeto en este caso es una gema
            if (isGem)
            {
                //Sumo uno al contador de gemas
                LevelManager.sharedInstance.gemCollected++;
                //El objeto ha sido recogido
                isCollected = true;
                //Actualizamos el contador de gemas
                UIController.sharedInstance.UpdateGemCount();
                //Llamamos al sonido de recoger una gema
                AudioManager.sharedInstance.PlaySFX(6);
                //Instanciamos el efecto de recoger el item
                Instantiate(pickupEffect, transform.position, transform.rotation);//Le pasamos el objeto a instanciar, su posici�n, su rotaci�n
                //Destruimos el Game Object
                Destroy(gameObject);
            }
        }
        //Si el objeto en este caso es una cura
        if (isHeal)
        {
            //Si el jugador no tiene la vida al m�ximo
            if (PlayerHealthController.sharedInstance.currentHealth != PlayerHealthController.sharedInstance.maxHealth)
            {
                //Hacemos el m�todo que cura vida al jugador
                PlayerHealthController.sharedInstance.HealPlayer();
                //El objeto ha sido recogido
                isCollected = true;
                //Llamamos al sonido de recoger una cura
                AudioManager.sharedInstance.PlaySFX(7);
                //Instanciamos el efecto de recoger el item
                Instantiate(pickupEffect, transform.position, transform.rotation);//Le pasamos el objeto a instanciar, su posici�n, su rotaci�n
                //Destruimos el objeto
                Destroy(gameObject);
            }

        }
    }
}
