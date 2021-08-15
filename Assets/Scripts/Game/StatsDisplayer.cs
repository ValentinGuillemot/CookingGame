using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsDisplayer : MonoBehaviour
{
    [SerializeField]
    private Stats statsToDisplay;

    [SerializeField]
    private TextMeshPro HPText;

    [SerializeField]
    private TextMeshPro MPText;

    [SerializeField]
    private TextMeshPro AttackText;

    [SerializeField]
    private TextMeshPro MagicText;

    [SerializeField]
    private TextMeshPro DefenseText;

    [SerializeField]
    private TextMeshPro FullnessText;

    void Start()
    {
        UpdateUI();
    }

    /// <summary>
	/// Change texts to fit current stats
	/// </summary>
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
