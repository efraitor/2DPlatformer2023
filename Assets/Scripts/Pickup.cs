using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //Variable para saber si este objeto es una gema o una cura
    public bool isGem, isHeal;

    //Variable para conocer si un objeto ya ha sido recogido
    private bool isCollected;

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
                                                                                //Destruimos el objeto
                Destroy(gameObject);
            }

        }
    }
}
