using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    //Referencia al objeto que queremos activar al entrar en esta zona
    public GameObject theBossBattle;

    //Método para detectar cuando un objeto entra en la zona del activador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es el jugador el que entra en esta zona
        if (collision.CompareTag("Player"))
        {
            //Activamos al jefe final
            theBossBattle.SetActive(true);
            //Desactivamos este objeto
            gameObject.SetActive(false);
            //Llamamos al método que reproduce la música del jefe final
            AudioManager.sharedInstance.PlayBossMusic();
        }
    }
}
