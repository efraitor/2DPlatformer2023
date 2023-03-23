using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    //Declaramos referencias a los MapPoints adyacentes
    public MapPoint up, right, down, left;
    //Variable para conocer si este MapPoint es un nivel y si está bloqueado o no
    public bool isLevel, isLocked;
    //variable para conocer el nivel que queremos cargar y el nivel que hay que chequear para saber si podemos entrar a este
    public string levelToLoad, levelToCheck;
    //Variable que contiene el nombre del nivel
    public string levelName;

    //Variables para saber las gemas recogidas y el total de gemas en el nivel
    public int gemCollected, totalGems;
    //Variables para el mejor tiempo conseguido en la partida y el tiempo sugerido
    public float bestTime, targetTime;

    //Referenciamos las medallas de haber obtenido todas las gemas, y haber hecho un tiempo mejor que el sugerido
    public GameObject gemBadge, timeBadge;

    // Start is called before the first frame update
    void Start()
    {
        //Si el MapPoint es un nivel y el nivel que cargar no está vacío
        if (isLevel && levelToLoad != null)
        {
            //Si la Key de gemas para este nivel existe en el archivo de guardado
            if (PlayerPrefs.HasKey(levelToCheck + "_gems"))
            {
                //Obtenemos las gemas recogidas en ese nivel
                gemCollected = PlayerPrefs.GetInt(levelToCheck + "_gems");
            }
            //Si la Key de tiempo para este nivel existe en el archivo de guardado
            if (PlayerPrefs.HasKey(levelToCheck + "_time"))
            {
                //Obtenemos el tiempo hecho en ese nivel
                bestTime = PlayerPrefs.GetFloat(levelToCheck + "_time");
                Debug.Log(PlayerPrefs.GetFloat(levelToCheck + "_time"));
            }

            //Si las gemas recogidas son las mismas o más que las que hay en el nivel
            if (gemCollected >= totalGems)
            {
                //Activamos la medalla de gemas para este nivel
                gemBadge.SetActive(true);
            }

            //Si el tiempo hecho en el nivel es mejor que el sugerido y no es 0 
            if (bestTime <= targetTime && bestTime != 0)
            {
                //Activamos la medalla de tiempo para este nivel
                timeBadge.SetActive(true);
            }

            //Primeramente bloqueamos el nivel por defecto, antes de comprobar si está desbloqueado
            isLocked = true;

            //Si el nivel a revisar no está vacío
            if (levelToCheck != null)
            {
                //Si la clave del nivel a revisar existe
                if (PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
                {
                    //Si esa clave tiene el valor de 1 (osea el nivel está desbloqueado)
                    if (PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
                    {
                        //Desbloqueamos el nivel
                        isLocked = false;
                    }
                }
            }

            //Si el nivel a cargar es el mismo que a comprobar (esto solo sucede en el primer nivel)
            if (levelToLoad == levelToCheck)
            {
                //Desbloqueamos este nivel
                isLocked = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
