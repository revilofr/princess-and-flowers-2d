using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float invincibilityFlashDelay = 0.15f;
    public float invincibilityTimeAfterHit = 3f;
    public bool isInvincible = false;

    public SpriteRenderer graphics;

    public HealthBar healthBar;

    private void Awake() {
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage) {
        if (isInvincible) {
            return;
        }
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        isInvincible = true;
        StartCoroutine(InvincibilityFlash());
        StartCoroutine(InvincibilityDelay());
    }

    public IEnumerator InvincibilityFlash() {
        while (isInvincible) { 
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);

        }
    }

    public IEnumerator InvincibilityDelay() {
        yield return new WaitForSeconds(invincibilityTimeAfterHit);
        isInvincible = false;
    }
}
