using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    //Objeto sobre el que actúa el interruptor
    public GameObject objectToSwitch;
    //Sprites al cambiar de estado el interruptor
    public Sprite downSprite, upSprite;
    //Desactivamos al usar el interruptor
    private bool activateSwitch;
    //Referencia al panel de información
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
        //Si pulsamos el botón E y el jugador puede interactuar
        if (Input.GetKeyDown(KeyCode.E) && PlayerController.sharedInstance.canInteract)
        {
            //Si no está activo el interruptor
            if (!activateSwitch)
            {
                //Desactivamos el objeto sobre el que actúa el interruptor
                objectToSwitch.SetActive(false);
                //El interruptor estaría activado
                activateSwitch = true;
                //Cambiamos el interruptor a bajado
                theSR.sprite = downSprite;
            }
            //Si por el contrario está activado
            else
            {
                //Activamos el objeto sobre el que actúa el interruptor
                objectToSwitch.SetActive(true);
                //El interruptor estaría desactivado
                activateSwitch = false;
                //Cambiamos el interruptor a subido
                theSR.sprite = upSprite;
            }
        }
           
    }

    //Método para conocer cuando un objeto entra en la zona de Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es el jugador el que entra en la zona del interruptor
        if (collision.CompareTag("Player"))
        {
            //Mostramos el panel de información
            infoPanel.SetActive(true);
            //Permitimos al jugador que pueda interactuar con el objeto
            PlayerController.sharedInstance.canInteract = true;
        }
    }

    //Método para conocer cuando un objeto sale de la zona de Trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Ocultamos el panel de información
        infoPanel.SetActive(false);
        //No permitimos al jugador que pueda interactuar con el objeto
        PlayerController.sharedInstance.canInteract = false;
    }
}
