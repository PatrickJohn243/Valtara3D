using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShowInteractableUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionText;

    private string interactionPrompt;

    public string InteractionPrompt
    {
        get { return interactionPrompt; } 
        set { interactionPrompt = value;
            UpdateInteractionText();
        } 
    }
    public void EnableInteractableUI()
    {
        UpdateInteractionText();
        this.gameObject.SetActive(true);
    }
    public void DisableInteractableUI()
    {
        this.gameObject.SetActive(false);
    }
    private void UpdateInteractionText()
    {
        interactionText.text = interactionPrompt;
    }
}
