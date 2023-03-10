using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Para cambiar entre escenas

public class PauseMenu : MonoBehaviour
{
    //Variables para saber las escenas a las que queremos ir desde este menú
    public string levelSelect, mainMenu;
    //Referencia al GO del menú de pausa
    public GameObject pauseMenu;
    //Variable para conocer cuando el juego está pausado o no
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Si pulsamos el botón de Intro
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //Pausamos el juego
            PauseUnpause();
        }
    }

    //Método para pausar o continuar el juego
    public void PauseUnpause()
    {
        ////Si la variable pausa es verdadera (el juego está pausado)
        if (isPaused)
        {
            //Entonces el juego continuará
            isPaused = false;
            //Desactivamos el panel de pausa
            pauseMenu.SetActive(false);
            //Reanudamos realmente el tiempo de juego
            Time.timeScale = 1f;

        }
        //Si el juego no estaba pausado
        else
        {
            //Entonces el juego se pondrá en pausa
            isPaused = true;
            //Activamos el panel de pausa
            pauseMenu.SetActive(true);
            //Pausamos realmente el tiempo de juego
            Time.timeScale = 0f;
        }

    }

    //Método para el botón LevelSelect
    public void LevelSelect()
    {
        //Para ir a la escena LevelSelect
        SceneManager.LoadScene(levelSelect);
        //Reanudamos realmente el tiempo de juego
        Time.timeScale = 1f;
    }

    //Método para el botón MainMenu
    public void MainMenu()
    {
        //Para ir a la escena MainMenu
        SceneManager.LoadScene(mainMenu);
        //Reanudamos realmente el tiempo de juego
        Time.timeScale = 1f;
    }
}
