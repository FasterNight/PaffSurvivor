using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    private Slider healthBar;
    private Slider xpBar;

    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Experience")]
    public int currentXP = 0;
    public int xpToLevelUp = 100;
    public int playerLevel = 1;
    public int attractRange = 5;

    [Header("XP UI Circular")]
    private Image xpCircle;
    private TextMeshProUGUI levelText;


    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        UpdateXPUI();
    }

    // Injection des barres de vie et xp sliders (existant)
    public void InitializeBars(Slider health, Slider xp)
    {
        healthBar = health;
        xpBar = xp;
        UpdateHealthUI();
        UpdateXPUI();
    }

    // Nouvelle méthode pour injecter la UI XP circulaire
    public void InitializeXPUI(Image circle, TextMeshProUGUI levelTxt)
    {
        xpCircle = circle;
        levelText = levelTxt;
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
        xpToLevelUp += 50;
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

        if (xpCircle != null)
            xpCircle.fillAmount = (float)currentXP / xpToLevelUp;

        if (levelText != null)
            levelText.text = playerLevel.ToString();
    }

    void Die()
    {
        Debug.Log("Le joueur est mort.");
        gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateHealthUI();
        UpdateXPUI();
    }
}
