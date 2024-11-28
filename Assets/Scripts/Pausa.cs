using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;

    // Start is called before the first frame update
    public void Pause()
    {
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void Cerrar()
    {
        // Restablecer las estadísticas de GameManager
        GameManager.Instance.playerScore = 0;
        GameManager.Instance.playerLife = GameManager.Instance.playerMaxLife;
        GameManager.Instance.playerDamage = 10;

        SceneManager.LoadScene(0);

        Reanudar();
    }
}
