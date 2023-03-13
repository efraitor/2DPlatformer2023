using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Para poder cambiar entre escenas

public class LoadScene : MonoBehaviour
{
    //Variable para saber la escena a la que queremos ir
    public string levelScene;

    // Start is called before the first frame update
    void Start()
    {
        //Llamamos a la corrutina que hace el salto al selector de niveles
        StartCoroutine(LoadLevelSelect());
        //Paramos todos los sonidos y música
        AudioManager.sharedInstance.bgm.Stop();
        //Llamamos al sonido del autobús
        AudioManager.sharedInstance.PlaySFX(12);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Corrutina para ir al selector de niveles
    public IEnumerator LoadLevelSelect()
    {
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(5);
        //Llamamos al método que hace fundido a negro
        UIController.sharedInstance.FadeToBlack();
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(1);
        //Para saltar a la escena que le pasamos en la variable
        SceneManager.LoadScene(levelScene);
    }
}
