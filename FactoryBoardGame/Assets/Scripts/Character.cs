using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] private int startingMoney = 0;
    [SerializeField] Text abilityText;
    [SerializeField] private Text startingMoneyText;
    [SerializeField] private Text nameText;


    void Start()
    {
        nameText.text = "";
        startingMoneyText.text = "";
        abilityText.text = "Kijátszhat egy lapot amit már kijátszott";
    }

    private void Action()
    {

    }
    public void setActive(bool val)
    {
        this.gameObject.SetActive(val);
    }
}
