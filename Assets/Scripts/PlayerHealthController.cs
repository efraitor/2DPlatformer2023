using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    //Variables para controlar la vida actual del jugador y el máximo de vida que puede tener
    public int currentHealth, maxHealth;

    //Variables para el contador de tiempo de invencibilidad
    public float invincibleLength; //Valor que tendrá el contador de tiempo
    private float invincibleCounter; //Contador de tiempo

    //Referencia del SpriteRenderer del jugador
    private SpriteRenderer theSR;

    //Hacemos el Singleton de este script
    public static PlayerHealthController sharedInstance;

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
        //Inicializamos la vida del jugador
        currentHealth = maxHealth;
        //Obtenemos el SpriteRenderer del jugador
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Comprobamos si el contador de invencibilidad aún no está vacío
        if (invincibleCounter > 0)
        {
            //Le restamos 1 cada segundo a ese contador independientemente del dispositivo que ejecute el juego
            invincibleCounter -= Time.deltaTime;

            //Cuando el contador haya decrecido hasta 0
            if (invincibleCounter <= 0)
            {
                //Cambiamos el color del sprite, mantenemos el RGB y ponemos la opacidad a tope
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        }
    }

    //Método para manejar el daño
    public void DealWithDamage()
    {
        //Restamos 1 de la vida que tengamos
        currentHealth--; //currentHealth -= 1; currentHealth = currentHealth - 1;

        //Si la vida está en 0 o por debajo (para asegurarnos de tener en cuenta solo valores positivos)
        if (currentHealth <= 0)
        {
            //Hacemos cero la vida si fuera negativa
            currentHealth = 0;

            //Hacemos desaparecer de momento al jugador
            gameObject.SetActive(false);
        }
        //Si el jugador ha recibido daño pero no ha muerto
        else
        {
            //Inicializamos el contador de invencibilidad
            invincibleCounter = invincibleLength;
            //Cambiamos el color del sprite, mantenemos el RGB y ponemos la opacidad a la mitad
            theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);
        }

        //Actualizamos la UI
        UIController.sharedInstance.UpdateHealthDisplay();
    }
}
