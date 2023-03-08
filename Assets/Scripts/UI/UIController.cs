using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Para poder trabajar con elementos de la UI
using TMPro;

public class UIController : MonoBehaviour
{
    //Referencias a las imágenes de los corazones de la UI
    public Image heart1, heart2, heart3;

    //Referencias a los sprites que cambiarán al perder o ganar un corazón
    public Sprite heartFull, heartEmpty;

    //Referencia al texto de la UI
    public TextMeshProUGUI gemText;

    //Hacemos el Singleton de este script
    public static UIController sharedInstance;

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
        //Inicializar el contador de gemas
        UpdateGemCount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método para actualizar la vida en la UI
    public void UpdateHealthDisplay()
    {
        //En este caso será mejor implementar un Switch ya que depende del valor de la misma variable
        ////Si la vida del jugador fuera 3
        //if(PlayerHealthController.sharedInstance.currentHealth == 3)
        //{
        //    //Ponemos la imagen de los tres corazones en lleno
        //    heart1.sprite = heartFull;
        //    heart2.sprite = heartFull;
        //    heart3.sprite = heartFull;
        //}

        //Dependiendo del valor de la vida actual del jugador
        switch (PlayerHealthController.sharedInstance.currentHealth)
        {
            //En el caso en el que la vida actual valga 3
            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                //Cerramos el caso
                break;
            //En el caso en el que la vida actual valga 2
            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                //Cerramos el caso
                break;
            //En el caso en el que la vida actual valga 1
            case 1:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                //Cerramos el caso
                break;
            //En el caso en el que la vida actual valga 0
            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                //Cerramos el caso
                break;
            //En el caso por defecto, el jugador estará muerto
            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                //Cerramos el caso
                break;
        }
    }

    //Método para actualizar el contador de gemas
    public void UpdateGemCount()
    {
        //Actualizar el número de gemas recogidas
        gemText.text = LevelManager.sharedInstance.gemCollected.ToString();//Cast -> convertimos el número entero en texto para que pueda ser representado en la UI
    }
}
