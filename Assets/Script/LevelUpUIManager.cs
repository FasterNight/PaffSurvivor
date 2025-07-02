using UnityEngine;

public class LevelUpUIManager : MonoBehaviour
{
    public void ChooseBonus(int bonusType)
    {
        // Exemple de traitement selon le bouton choisi
        switch (bonusType)
        {
            case 0:
                Debug.Log("Bonus : +20 HP");
                // Ajoute ici des effets concrets si besoin
                break;
            case 1:
                Debug.Log("Bonus : +Dégâts");
                break;
            case 2:
                Debug.Log("Bonus : +Vitesse");
                break;
        }

        // Fermer le menu et reprendre le jeu
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
