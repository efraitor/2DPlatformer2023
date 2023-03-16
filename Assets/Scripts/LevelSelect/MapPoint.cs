using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    //Declaramos referencias a los MapPoints adyacentes
    public MapPoint up, right, down, left;
    //Variable para conocer si este MapPoint es un nivel
    public bool isLevel;
    //variable para conocer el nivel que queremos cargar
    public string levelToLoad;
    //Variable que contiene el nombre del nivel
    public string levelName;

    //Variables para saber las gemas recogidas y el total de gemas en el nivel
    public int gemCollected, totalGems;
    //Variables para el mejor tiempo conseguido en la partida y el tiempo sugerido
    public float bestTime, targetTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
