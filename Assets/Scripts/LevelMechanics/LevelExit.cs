using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    //Método para detectar cuando un objeto entra en la zona de salida del nivel
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es el jugador el que entra en la zona de salida
        if (collision.CompareTag("Player"))
        {
            //Llamar al método que finaliza el nivel
            LevelManager.sharedInstance.ExitLevel();
        }
    }
}
