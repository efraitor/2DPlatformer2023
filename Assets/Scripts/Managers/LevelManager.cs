using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Para cambiar entre escenas

public class LevelManager : MonoBehaviour
{
    //Tiempo antes de respawnear
    public float waitToRespawn;

    //Variable para el contador de gemas
    public int gemCollected;
    //Variable para guardar el tiempo hecho en el nivel
    public float timeInLevel;

    //Variable para guardar el nombre del nivel al que queremos ir
    public string levelToLoad;

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
        //Ocultamos el cartel de haber finalizado el nivel
        UIController.sharedInstance.levelCompleteText.gameObject.SetActive(false);
        //Inicializamos el tiempo hecho en el nivel
        timeInLevel = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Vamos haciendo que aumente el tiempo hecho en el nivel a 1 por segundo
        timeInLevel += Time.deltaTime;
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
        //Llamamos al método que hace fundido a negro
        UIController.sharedInstance.FadeToBlack();
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(waitToRespawn);
        //Llamamos al método que hace fundido a transparente
        UIController.sharedInstance.FadeFromBlack();
        //Activamos de nuevo al jugador
        PlayerController.sharedInstance.gameObject.SetActive(true);
        //Lo ponemos en la posición de respawn
        PlayerController.sharedInstance.transform.position = CheckpointController.sharedInstance.spawnPoint;
        //Ponemos la vida del jugador al máximo
        PlayerHealthController.sharedInstance.currentHealth = PlayerHealthController.sharedInstance.maxHealth;
        //Actualizamos la UI
        UIController.sharedInstance.UpdateHealthDisplay();
    }

    //Método para terminar un nivel
    public void ExitLevel()
    {
        //Llamamos a la corrutina
        StartCoroutine(ExitLevelCo());
    }

    //Corrutina de terminar un nivel
    public IEnumerator ExitLevelCo()
    {
        //Paramos los inputs del jugador
        PlayerController.sharedInstance.stopInput = true;
        //Paramos el movimiento del jugador
        PlayerController.sharedInstance.StopPlayer();
        //Paramos la música del nivel
        AudioManager.sharedInstance.bgm.Stop();
        //Reproducimos la música de ganar el nivel
        AudioManager.sharedInstance.levelEndMusic.Play();
        //Mostramos el cartel de haber finalizado el nivel
        UIController.sharedInstance.levelCompleteText.gameObject.SetActive(true);
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(1.5f);
        //Fundido a negro
        UIController.sharedInstance.FadeToBlack();
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(1.5f);

        //Generamos en el documento de guardado una variable cuyo nombre será NombreDeLaEscena_unlocked y la iguala a 1 para indicar que está desbloqueado
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
        //Guardamos el nombre de la escena actual en la que nos encontramos
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
        //Si existe la Key de gemas recogidas en este nivel
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_gems"))
        {
            //Si esta vez hemos recogido más gemas que la vez anterior
            if (gemCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems"))
            {
                //Guardamos el número de gemas recogidas en este nivel concreto
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemCollected);
            }
        }
        //Sin embargo si es la primera vez que nos pasamos este nivel
        else
        {
            //Guardamos el número de gemas recogidas en este nivel concreto
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemCollected);
        }
        //Si existe la Key de tiempo hecho en este nivel
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time"))
        {
            //Si esta vez hemos hecho menos tiempo que la vez anterior
            if (timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time"))
            {
                //Guardamos el tiempo hecho en este nivel concreto
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
            }
        }
        //Sin embargo si es la primera vez que nos pasamos este nivel
        else
        {
            //Guardamos el tiempo hecho en este nivel concreto
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
        }

        //Ir a la pantalla de carga
        SceneManager.LoadScene(levelToLoad);
    }
}
