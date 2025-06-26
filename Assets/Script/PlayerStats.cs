using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    // Références aux UI (injection via GameManager)
    private Slider healthBar;
    private Slider xpBar;

    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Experience")]
    public int currentXP = 0;
    public int xpToLevelUp = 100;
    public int playerLevel = 1;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        UpdateXPUI();
    }

    // Appelée par le GameManager après l’instanciation
    public void InitializeBars(Slider health, Slider xp)
    {
        healthBar = health;
        xpBar = xp;

        UpdateHealthUI();
        UpdateXPUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    public void GainXP(int amount)
    {
        currentXP += amount;

        while (currentXP >= xpToLevelUp)
        {
            currentXP -= xpToLevelUp;
            LevelUp();
        }

        UpdateXPUI();
    }

    void LevelUp()
    {
        playerLevel++;
        xpToLevelUp += 50; // ou une formule plus dynamique
        Debug.Log("Niveau augmenté ! Niveau actuel : " + playerLevel);
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
            healthBar.value = (float)currentHealth / maxHealth;
    }

    void UpdateXPUI()
    {
        if (xpBar != null)
            xpBar.value = (float)currentXP / xpToLevelUp;
    }

    void Die()
    {
        Debug.Log("Le joueur est mort.");
        // Tu peux désactiver le joueur ou déclencher une animation ici
        gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateHealthUI ();
        UpdateXPUI ();
    }
}
