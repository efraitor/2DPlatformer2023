using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Para cambiar entre escenas

public class MainMenu : MonoBehaviour
{
    //Variable para saber la escena a la que queremos ir al principio o al continuar el juego
    public string startScene, continueScene;

    //Referencia al botón de Continuar
    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        //Comprobamos si al menos nos hemos pasado el primer nivel, porque eso significa que ya hay un archivo de guardado y podemos continuar partida
        if (PlayerPrefs.HasKey(startScene + "_unlocked"))
        {
            //El botón de guardado se muestra
            continueButton.SetActive(true);
        }
        //Si por el contrario nunca habíamos guardado partida
        else
        {
            //El botón de guardado se esconde
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método para el Botón Continue
    public void ContinueGame()
    {
        //Para saltar a la escena que le pasamos en la variable
        SceneManager.LoadScene(continueScene);
    }

    //Método para el Botón Start
    public void StartGame()
    {
        //Para saltar a la escena que le pasamos en la variable
        SceneManager.LoadScene(startScene);
        //Borramos todo lo que contiene el archivo de guardado del juego
        PlayerPrefs.DeleteAll();
    }

    //Método para el Botón Quit
    public void QuitGame()
    {
        //Para quitar el juego (solo funciona en la Build no en el editor)
        Application.Quit();
        //Feedback para el editor
        Debug.Log("Quitting Game");
    }
}
