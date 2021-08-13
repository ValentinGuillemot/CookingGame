using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsDisplayer : MonoBehaviour
{
    [SerializeField]
    Stats statsToDisplay;

    [SerializeField]
    TextMeshPro HPText;

    [SerializeField]
    TextMeshPro MPText;

    [SerializeField]
    TextMeshPro AttackText;

    [SerializeField]
    TextMeshPro MagicText;

    [SerializeField]
    TextMeshPro DefenseText;

    [SerializeField]
    TextMeshPro FullnessText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        HPText.text = "HP: " + statsToDisplay.Life.ToString();
        MPText.text = "MP: " + statsToDisplay.Mana.ToString();
        AttackText.text = "Attack: " + statsToDisplay.Attack.ToString();
        MagicText.text = "Magic: " + statsToDisplay.Magic.ToString();
        DefenseText.text = "Defense: " + statsToDisplay.Defense.ToString();
        FullnessText.text = "Fullness: " + statsToDisplay.Fullness.ToString();
    }
}
