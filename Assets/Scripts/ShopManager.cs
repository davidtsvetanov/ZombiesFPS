using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public Image infoMenu;
    private bool inside;
    [SerializeField] TextMeshProUGUI itemsDescription;
    [SerializeField] int priceMaxHealth = 10;
    [SerializeField] int priceMaxArmor = 10;
    [SerializeField] int priceStamina = 10;
    [SerializeField] int priceSpeed = 10;
    [SerializeField] int priceAk = 25;
    [SerializeField] int priceRefill = 10;
    [SerializeField] int increaseMaxHealth = 10;
    [SerializeField] int increaseMaxArmor = 10;
    [SerializeField] int increaseStamina = 10;
    [SerializeField] float increaseSpeed = 0.1f;
    

    public void BuyHealth()
    {
        if(CharController_Motor.Instance.money < priceMaxHealth)
        {
            return;
        }
        CharController_Motor.Instance.ChangeMoneyAmount(-priceMaxHealth);
        CharController_Motor.Instance.IncreaseMaxHealth(increaseMaxHealth);
    }
    public void BuyArmor()
    {
        if (CharController_Motor.Instance.money < priceMaxArmor)
        {
            return;
        }
        CharController_Motor.Instance.ChangeMoneyAmount(-priceMaxArmor);
        CharController_Motor.Instance.IncreaseMaxArmor(increaseMaxArmor);
    }
    public void BuyStamina()
    {
        if (CharController_Motor.Instance.money < priceStamina)
        {
            return;
        }
        CharController_Motor.Instance.ChangeMoneyAmount(-priceStamina);
        CharController_Motor.Instance.IncreaseMaxStamina(increaseStamina);
    }
    public void BuyAk()
    {
        if (CharController_Motor.Instance.money < priceAk)
        {
            return;
        }
        if (CharController_Motor.Instance.ak.activeInHierarchy == false)
        {
            CharController_Motor.Instance.ChangeMoneyAmount(-priceAk);
            CharController_Motor.Instance.EquipWeapon();
        }
    }
    public void BuySpeed()
    {
        if (CharController_Motor.Instance.money < priceSpeed)
        {
            return;
        }
        CharController_Motor.Instance.ChangeMoneyAmount(-priceSpeed);
        CharController_Motor.Instance.IncreaseMaxSpeed(increaseSpeed);

    }
    public void Refill()
    {
        if (CharController_Motor.Instance.money < priceRefill)
        {
            return;
        }
        CharController_Motor.Instance.ChangeMoneyAmount(-priceRefill);
        CharController_Motor.Instance.RefillStats();

    }

    public void ShowItemsInfo(string info)
    {
        itemsDescription.text = info;
        inside = true;
        infoMenu.gameObject.SetActive(true);
    }

    public void HideItemsInfo()
    {
        inside = false;
        infoMenu.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inside)
        {
            
            infoMenu.transform.position = new Vector3( Input.mousePosition.x, Input.mousePosition.y, infoMenu.transform.position.z);
        }
        
    }
}
