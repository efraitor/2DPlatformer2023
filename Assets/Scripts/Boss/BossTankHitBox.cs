using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankHitBox : MonoBehaviour
{
    //Referencia al jefe final
    public BossTankController bossCont;

    //Si algo entra en la HitBox
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el que entra en la HitBox es el jugador y a su vez está por encima de esta
        if (collision.CompareTag("Player") && PlayerController.sharedInstance.transform.position.y > transform.position.y)
        {
            //Llamamos al método que hace daño al jefe final
            bossCont.TakeHit();
            //Hacemos rebotar al jugador
            PlayerController.sharedInstance.Bounce(8.0f);
            //Desactivamos la HitBox
            gameObject.SetActive(false);
        }
    }
}
