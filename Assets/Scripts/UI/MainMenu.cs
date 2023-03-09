using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Para cambiar entre escenas

public class MainMenu : MonoBehaviour
{
    //Variable para saber la escena a la que queremos ir al principio
    public string startScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método para el Botón Start
    public void StartGame()
    {
        //Para saltar a la escena que le pasamos en la variable
        SceneManager.LoadScene(startScene);
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
