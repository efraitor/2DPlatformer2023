using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Para poder saltar entre escenas

public class LSManager : MonoBehaviour
{

    //Generamos una referencia al LSPlayer para poder acceder a su información
    public LSPlayer thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        
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
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(1f);
        //Cargamos el nivel al que queremos ir
        SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad);
    }
}
