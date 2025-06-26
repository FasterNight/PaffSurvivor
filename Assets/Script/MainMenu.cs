using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Appelé quand on clique sur "Jouer"
    public void PlayGame()
    {
        // Remplace "GameScene" par le nom exact de ta scène
        SceneManager.LoadScene("Ennemie");
    }

    // Appelé quand on clique sur "Quitter"
    public void QuitGame()
    {
        // Quitte l'application (ne marche qu'en build)
        Application.Quit();

        // Affiche un message dans l'éditeur (utile pour debug)
        Debug.Log("Quitter le jeu");
    }
}
