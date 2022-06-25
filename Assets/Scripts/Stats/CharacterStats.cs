using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat _maxHealth;
    public float maxHealth
    {
        get { return _maxHealth.GetValue(); }

        set
        {
            _maxHealth.AddModifier(value);
            healthbar.SetMaxHealth((int)_maxHealth.GetValue());
        }
    }

    float _currentHealth;
    public float currentHealth
    {
        get { return _currentHealth; }

        set
        {
            _currentHealth = value;
            if (_currentHealth > _maxHealth.GetValue())
                _currentHealth = _maxHealth.GetValue();
            healthbar.SetHealth((int)_currentHealth);
            if (_currentHealth == _maxHealth.GetValue())
            {
                hpBarCanvas.SetActive(false);
            }
        }
    }
    public Stat armor;
    public Stat damage;
    public Stat attackSpeed;
    public Stat projectileSpeed;
    public Stat rotateSpeed;

    public GameObject hpBarCanvas;
    public HealthBar healthbar;
    
    void Awake()
    {
        healthbar = hpBarCanvas.GetComponentInChildren<HealthBar>();

        healthbar.SetMaxHealth((int)maxHealth);
        currentHealth = maxHealth;

        hpBarCanvas.SetActive(false);
    }

    public virtual void TakeDamage(float damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, float.MaxValue);

        if (damage > 0)
        {
            if (hpBarCanvas.activeSelf == false) hpBarCanvas.SetActive(true);

            currentHealth -= damage;
            
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    /// <summary>
    /// This method meant to be overwritten.
    /// </summary>
    public virtual void Die()
    {
        //hpBarCanvas.SetActive(false);
    }
}
