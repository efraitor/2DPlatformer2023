using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Tiempo antes de respawnear
    public float waitToRespawn;

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
        
    }
}
