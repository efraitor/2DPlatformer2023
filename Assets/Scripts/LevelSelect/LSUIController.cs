using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Para poder trabajar con elementos de la UI

public class LSUIController : MonoBehaviour
{
    //Referencia al FadeScreen
    public Image fadeScreen;
    //Variable para la velocidad de transición al FadeScreen
    public float fadeSpeed;
    //Variables para conocer cuando hacemos fundido a negro o vuelta a transparente
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    //Referencia al panel de información del selector de niveles
    public GameObject levelInfoPanel;
    //Referencia al texto del nombre del nivel
    public Text levelName;
    //Texto de gemas recogidas en el nivel y texto de gemas que se pueden recoger en el nivel, texto de tiempo sugerido y texto de mejor tiempo hecho en el nivel
    public Text gemsFound, gemsTarget, timeTarget, bestTime;

    //Hacemos el Singleton de este script
    public static LSUIController sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null) sharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cuando empieza el juego hacemos fundido a transparente
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        //Si hay que hacer fundido a negro
        if (shouldFadeToBlack)
        {
            //Cambiar la transparencia del color a opaco
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            //Mathf.MoveTowards (Moverse hacia) -> el valor que queremos cambiar, valor al que lo queremos cambiar, velocidad a la que lo queremos cambiar
            //Si el color ya es totalmente opaco
            if (fadeScreen.color.a == 1f)
            {
                //Paramos de hacer fundido a negro
                shouldFadeToBlack = false;
            }
        }
        //Si hay que hacer fundido a transparente
        if (shouldFadeFromBlack)
        {
            //Cambiar la transparencia del color a transparente
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            //Mathf.MoveTowards (Moverse hacia) -> el valor que queremos cambiar, valor al que lo queremos cambiar, velocidad a la que lo queremos cambiar
            //Si el color ya es totalmente transparente
            if (fadeScreen.color.a == 0f)
            {
                //Paramos de hacer fundido a transparente
                shouldFadeFromBlack = false;
            }
        }
    }

    //Método para hacer fundido a negro
    public void FadeToBlack()
    {
        //Activamos la booleana de fundido a negro
        shouldFadeToBlack = true;
        //Desactivamos la booleana de fundido a transparente
        shouldFadeFromBlack = false;
    }

    //Método para hacer fundido a transparente
    public void FadeFromBlack()
    {
        //Activamos la booleana de fundido a transparente
        shouldFadeFromBlack = true;
        //Desactivamos la booleana de fundido a negro
        shouldFadeToBlack = false;
    }

    //Método para mostrar el panel de información del nivel
    public void ShowInfo(MapPoint levelInfo) //Al llamar al método le pasamos un punto concreto del mapa por parámetro
    {
        //Ponemos el nombre del nivel actual
        levelName.text = levelInfo.levelName;
        //Ponemos el número de gemas recogidas en el nivel
        gemsFound.text = "FOUND: " + levelInfo.gemCollected;
        //Ponemos el número de gemas que se pueden recoger en el nivel
        gemsTarget.text = "IN LEVEL: " + levelInfo.totalGems;
        //Ponemos el tiempo sugerido para pasar el nivel
        timeTarget.text = "TARGET: " + levelInfo.targetTime + "s";
        //Si el mejor tiempo hecho en el nivel es 0
        if (levelInfo.bestTime == 0)
        {
            //Ponemos un BestTime por defecto
            bestTime.text = "BEST: ----";
        }
        //Si el mejor tiempo hecho en el nivel es distinto de 0
        else
        {
            //Ponemos el mejor tiempo hecho en el nivel
            bestTime.text = "BEST: " + levelInfo.bestTime.ToString("f2") + "s";
        }

        //Activamos el panel
        levelInfoPanel.SetActive(true);
    }

    //Método para ocultar el panel de información del nivel
    public void HideInfo()
    {
        //Desactivamos el panel
        levelInfoPanel.SetActive(false);
    }
}
