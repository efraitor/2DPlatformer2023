using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método para saber cuando el jugador ha entrado en la zona de muerte
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es el jugador el que ha entrado en la zona
        if (collision.CompareTag("Player"))
        {
            //Ponemos la vida del jugador a 0
            PlayerHealthController.sharedInstance.currentHealth = 0;
            //Actualizamos la UI
            UIController.sharedInstance.UpdateHealthDisplay();
            //Hacemos el respawn
            LevelManager.sharedInstance.RespawnPlayer();
        }
    }
}
