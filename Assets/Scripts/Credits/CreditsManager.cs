using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video; // Necesario para trabajar con VideoPlayer

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer; // Referencia al VideoPlayer
    [SerializeField] private string nextSceneName = "FinalScene"; // Nombre de la escena siguiente

    private void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("¡No se asignó un VideoPlayer en el Inspector!");
            return;
        }

        // Suscribirse al evento que se dispara cuando el video termina
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("El video terminó. Cambiando a la escena: " + nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }
}