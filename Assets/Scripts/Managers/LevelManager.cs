using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Tiempo antes de respawnear
    public float waitToRespawn;

    //Variable para el contador de gemas
    public int gemCollected;

    //Hacemos el Singleton de este script
    public static LevelManager sharedInstance;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método para respawnear al jugador cuando muere
    public void RespawnPlayer()
    {
        //Llamamos a la corrutina que respawnea al jugador
        StartCoroutine(RespawnPlayerCo());
    }

    //Corrutina para respawnear al jugador
    public IEnumerator RespawnPlayerCo()
    {
        //Desactivamos al jugador
        PlayerController.sharedInstance.gameObject.SetActive(false);
        //Llamamos al sonido de muerte
        AudioManager.sharedInstance.PlaySFX(8);
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(waitToRespawn);
        //Activamos de nuevo al jugador
        PlayerController.sharedInstance.gameObject.SetActive(true);
        //Lo ponemos en la posición de respawn
        PlayerController.sharedInstance.transform.position = CheckpointController.sharedInstance.spawnPoint;
        //Ponemos la vida del jugador al máximo
        PlayerHealthController.sharedInstance.currentHealth = PlayerHealthController.sharedInstance.maxHealth;
        //Actualizamos la UI
        UIController.sharedInstance.UpdateHealthDisplay();
    }
}
