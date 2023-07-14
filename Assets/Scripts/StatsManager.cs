using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statMaxHealth;
    [SerializeField] TextMeshProUGUI statMaxArmor;
    [SerializeField] TextMeshProUGUI statMaxStamina;
    [SerializeField] TextMeshProUGUI statMaxSpeed;
    [SerializeField] TextMeshProUGUI statCurrentHealth;
    [SerializeField] TextMeshProUGUI statCurrentArmor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeStatMaxHealth()
    {
        statMaxHealth.text = $"MAX HEALTH: {CharController_Motor.Instance.maxHealth}";
    }
    public void ChangeStatMaxArmor()
    {
        statMaxArmor.text = $"MAX ARMOR: {CharController_Motor.Instance.maxArmor}";
    }
    public void ChangeStatMaxStamina()
    {
        statMaxStamina.text = $"STAMINA: {CharController_Motor.Instance.maxStamina}";
    }
    public void ChangeStatCurrentArmor()
    {
        statCurrentArmor.text = $"CURRENT ARMOR: {CharController_Motor.Instance.armor}";
    }
    public void ChangeStatSpeed()
    {
        statMaxSpeed.text = $"SPEED: {CharController_Motor.Instance.speed.ToString("F1")}";
    }
    public void ChangeStatCurrentHealth()
    {
        statCurrentHealth.text = $"CURRENT HEALTH: {CharController_Motor.Instance.healthPlayer}";
    }

    private void OnEnable()
    {
        ChangeStatMaxHealth();
        ChangeStatMaxArmor();
        ChangeStatMaxStamina();
        ChangeStatCurrentArmor();
        ChangeStatSpeed();
        ChangeStatCurrentHealth();
    }
}
