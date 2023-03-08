using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathEffect : MonoBehaviour
{
    //Referencia al Sprite Renderer del efecto de muerte
    private SpriteRenderer theDeathEffectSR;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la referencia al SpriteRenderer
        theDeathEffectSR = GetComponent<SpriteRenderer>();

        //Girar el sprite del jugador según hacia que lado mira
        //Si el jugador mira hacia la izquierda
        if (PlayerController.sharedInstance.isLeft)
        {
            //No cambiamos la dirección del sprite
            theDeathEffectSR.flipX = false;
        }
        //Si el jugador por el contrario mira hacia la derecha
        else if (!PlayerController.sharedInstance.isLeft)
        {
            //Cambiamos la dirección del sprite
            theDeathEffectSR.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
