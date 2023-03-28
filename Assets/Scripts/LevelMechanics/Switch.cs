using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    //Objeto sobre el que act�a el interruptor
    public GameObject objectToSwitch;
    //Sprites al cambiar de estado el interruptor
    public Sprite downSprite, upSprite;
    //Desactivamos al usar el interruptor
    private bool activateSwitch;
    //Referencia al panel de informaci�n
    public GameObject infoPanel;
    //Referencia al Sprite Renderer del interruptor
    private SpriteRenderer theSR;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el Sprite Renderer del interruptor
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Si pulsamos el bot�n E y el jugador puede interactuar
        if (Input.GetKeyDown(KeyCode.E) && PlayerController.sharedInstance.canInteract)
        {
            //Si no est� activo el interruptor
            if (!activateSwitch)
            {
                //Desactivamos el objeto sobre el que act�a el interruptor
                objectToSwitch.SetActive(false);
                //El interruptor estar�a activado
                activateSwitch = true;
                //Cambiamos el interruptor a bajado
                theSR.sprite = downSprite;
            }
            //Si por el contrario est� activado
            else
            {
                //Activamos el objeto sobre el que act�a el interruptor
                objectToSwitch.SetActive(true);
                //El interruptor estar�a desactivado
                activateSwitch = false;
                //Cambiamos el interruptor a subido
                theSR.sprite = upSprite;
            }
        }
           
    }

    //M�todo para conocer cuando un objeto entra en la zona de Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es el jugador el que entra en la zona del interruptor
        if (collision.CompareTag("Player"))
        {
            //Mostramos el panel de informaci�n
            infoPanel.SetActive(true);
            //Permitimos al jugador que pueda interactuar con el objeto
            PlayerController.sharedInstance.canInteract = true;
        }
    }

    //M�todo para conocer cuando un objeto sale de la zona de Trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Ocultamos el panel de informaci�n
        infoPanel.SetActive(false);
        //No permitimos al jugador que pueda interactuar con el objeto
        PlayerController.sharedInstance.canInteract = false;
    }
}
