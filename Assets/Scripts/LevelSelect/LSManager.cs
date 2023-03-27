using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Para poder saltar entre escenas

public class LSManager : MonoBehaviour
{
    //Generamos una referencia al LSPlayer para poder acceder a su información
    public LSPlayer thePlayer;

    //Array donde guardar todos los puntos del mapa que hay en la escena
    private MapPoint[] allPoints;

    // Start is called before the first frame update
    void Start()
    {
        //Rellenamos el array con todos los puntos del mapa
        allPoints = FindObjectsOfType<MapPoint>();
        //Si existe la Key de nivel actual
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            //Recorremos todos los MapPoints guardados en el array
            foreach (MapPoint point in allPoints)
            {
                //Si el punto actual tiene por nivel que cargar el nivel que guardamos como nivel actual
                if (point.levelToLoad == PlayerPrefs.GetString("CurrentLevel"))
                {
                    //Movemos al jugador a ese punto del mapa
                    thePlayer.transform.position = point.transform.position;
                    //Haremos este punto del mapa el punto actual del jugador
                    thePlayer.currentPoint = point;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método que carga el nivel
    public void LoadLevel()
    {
        //Llamamos a la corrutina de cargar el nivel
        StartCoroutine(LoadLevelCo());
    }

    //La corrutina para cargar un nivel
    public IEnumerator LoadLevelCo()
    {
        //Hacemos fundido a negro
        LSUIController.sharedInstance.FadeToBlack();
        //Reproducimos el sonido de cargar un nivel
        AudioManager.sharedInstance.PlaySFX(4);
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(1f);
        //Cargamos el nivel al que queremos ir
        SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad);
    }
}
