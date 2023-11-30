using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI dialogText;

    [SerializeField]
    GameObject actionSelector;
    [SerializeField]
    GameObject abilitySelector;
    [SerializeField]
    GameObject abilityDetails;

    [SerializeField]
    List<TextMeshProUGUI> actionTexts;
    [SerializeField]
    List<TextMeshProUGUI> abilityTexts;

    [SerializeField]
    TextMeshProUGUI typeText;

    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        foreach (char letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/30);
        }
    }

    public void EnableDialogText(bool enabled)
    {
        dialogText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
    }

    public void EnableAbilitySelector(bool enabled)
    {
        abilitySelector.SetActive(enabled); 
        abilityDetails.SetActive(enabled);
    }

    public void SetMoveNames(List<AbilitiesBase> abilities)
    {
        for (int i = 0; i < abilityTexts.Count; i++)
        {
            if (i < abilities.Count)
            {
                abilityTexts[i].text = abilities[i].Name;
            }
            else
            {
                abilityTexts[i].text = "N/A";
            }
        }
    }

    public void UpdateAbilitySelection(int selected, AbilitiesBase ability)
    {
        for (int i = 0; i < abilityTexts.Count; i++)
        {
            if (i == selected)
            {
                abilityTexts[i].color = Color.yellow;
                abilityTexts[i].alpha = 255f;
            }
            else
            {
                abilityTexts[i].color = Color.white;
                abilityTexts[i].alpha = 255f;
            }

            typeText.text = "Type: " +  ability.Type.ToString();

        }
    }

    public void UpdateActionSelection(int selected)
    {
        for (int i = 0; i < actionTexts.Count; i++)
        {
            if (i == selected)
            {
                actionTexts[i].color = Color.yellow; 
                actionTexts[i].alpha = 255f; 
            }
            else
            {
                actionTexts[i].color = Color.white;
                actionTexts[i].alpha = 255f;
            }
        
        }
    }
}
