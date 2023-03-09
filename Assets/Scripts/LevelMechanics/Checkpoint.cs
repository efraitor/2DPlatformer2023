using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //Referencia al SpriteRenderer del checkpoint
    private SpriteRenderer theSR;

    //Los sprites que se irán alternando al activar o desactivar los checkpoints
    public Sprite cpOn, cpOff;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la referencia al SpriteRenderer
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método para conocer cuando el jugador entra en la zona de checkpoint
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es el jugador el que ha entrado en la zona de Checkpoint y el Checkpoint estaba inactivo
        if (collision.CompareTag("Player") && theSR.sprite == cpOff)
        {
            //Desactivamos primero todos los checkpoints
            CheckpointController.sharedInstance.DeactivateCheckpoints();
            
            //Cambiamos el sprite a Checkpoint activo
            theSR.sprite = cpOn;
            //Llamamos al sonido de Checkpoint
            AudioManager.sharedInstance.PlaySFX(11);
         
            //Le pasamos al CheckpointController la nueva posición de reaparición
            CheckpointController.sharedInstance.SetSpawnPoint(transform.position);
        }
    }

    //Método para desactivar los Checkpoints
    public void ResetCheckpoint()
    {
        //Cambiamos el sprite a Checkpoint inactivo
        theSR.sprite = cpOff;
    }
}
