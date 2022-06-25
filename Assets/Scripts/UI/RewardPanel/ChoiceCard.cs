using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceCard : MonoBehaviour
{
    public Button button;
    
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI value;

    UpgradeCard upgradeCard;

    public void AddCard(UpgradeCard card)
    {
        upgradeCard = card;

        title.text = upgradeCard.name;
        value.text = upgradeCard.GetValueText();
        
        button.onClick.AddListener(upgradeCard.Activate);
    }

    public void RemoveCard()
    {
        upgradeCard = null;

        title.text = null;
        value.text = null;

        button.onClick.RemoveAllListeners();
    }
}
