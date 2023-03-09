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

    //La referencia del efecto de muerte del jugador
    public GameObject deathEffect;

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
        //Si el contador de tiempo de invencibilidad se ha agotado, es decir, ya no somos invencibles
        if (invincibleCounter <= 0)
        {

            //Restamos 1 de la vida que tengamos
            currentHealth--; //currentHealth -= 1; currentHealth = currentHealth - 1;

            //Si la vida está en 0 o por debajo (para asegurarnos de tener en cuenta solo valores positivos)
            if (currentHealth <= 0)
            {
                //Hacemos cero la vida si fuera negativa
                currentHealth = 0;

                //Hacemos desaparecer de momento al jugador
                //gameObject.SetActive(false);

                //Instanciamos el efecto de muerte del jugador
                Instantiate(deathEffect, transform.position, transform.rotation);

                //Llamamos al método que respawnea al jugador
                LevelManager.sharedInstance.RespawnPlayer();
            }
            //Si el jugador ha recibido daño pero no ha muerto
            else
            {
                //Inicializamos el contador de invencibilidad
                invincibleCounter = invincibleLength;
                //Cambiamos el color del sprite, mantenemos el RGB y ponemos la opacidad a la mitad
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);

                //Llamamos al método que hace que el jugador realice el KnockBack
                PlayerController.sharedInstance.KnockBack();

                //Llamamos al sonido de hacerle daño al jugador
                AudioManager.sharedInstance.PlaySFX(9);
            }

            //Actualizamos la UI
            UIController.sharedInstance.UpdateHealthDisplay();
        }
    }

    //Método para curar al jugador
    public void HealPlayer()
    {
        //Curamos al jugador a su vida máxima
        //currentHealth = maxHealth;

        //Sumamos 1 a la vida del jugador
        currentHealth++;
        //Si la vida actual es mayor que la máxima
        if (currentHealth > maxHealth)
        {
            //Hacemos que la vida del jugador vuelva a la máxima
            currentHealth = maxHealth;
        }
        //Actualizamos la UI
        UIController.sharedInstance.UpdateHealthDisplay();
    }
}
