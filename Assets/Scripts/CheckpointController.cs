using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    //Declaramos un array donde guardar los checkpoints de la escena
    private Checkpoint[] checkpoints;

    //Referencia a la posici�n del jugador
    public Vector3 spawnPoint;

    //Hacemos el Singleton de este script
    public static CheckpointController sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Buscamos todos los Game Objects que tengan el script asociado checkpoint y los guardamos en nuestro array
        checkpoints = GetComponentsInChildren<Checkpoint>();
        //Guardamos la posici�n inicial como punto de checkpoint si a�n no hemos activado ninguno
        spawnPoint = PlayerController.sharedInstance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //M�todo para desactivar los checkpoints
    public void DeactivateCheckpoints()
    {
        //Hacemos un bucle que pase por todos los checkpoints almacenados en el array
        for (int i = 0; i < checkpoints.Length; i++)
        {
            //Para cada checkpoint uso su m�todo de reset
            checkpoints[i].ResetCheckpoint();
        }
    }

    //M�todo para generar el punto de reaparici�n del jugador
    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        //El punto de spawn del jugador ser� el del checkpoint activo que le pasemos
        spawnPoint = newSpawnPoint;
    }
}
