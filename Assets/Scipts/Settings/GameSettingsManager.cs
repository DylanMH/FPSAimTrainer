using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsManager : MonoBehaviour
{
    [Header("Crosshair Settings")]
    public TMP_Dropdown crosshairTypeDropdown; // Dropdown for crosshair types
    public Button[] crosshairColorButtons; // Buttons for crosshair colors
    public Image crosshairPreview; // Preview Image for crosshair
    public Sprite[] crosshairSprites; // Array of sprites for different crosshair types

    [Header("Target Settings")]
    public Button[] targetColorButtons; // Buttons for target colors
    public Image targetPreview; // Preview Image for target

    private void Start()
    {
        // Initialize dropdown and buttons
        crosshairTypeDropdown.onValueChanged.AddListener(UpdateCrosshairTypePreview);
        foreach (Button button in crosshairColorButtons)
        {
            button.onClick.AddListener(() => UpdateCrosshairColor(button));
        }

        foreach (Button button in targetColorButtons)
        {
            button.onClick.AddListener(() => UpdateTargetPreview(button));
        }

        // Load saved settings
        LoadSavedSettings();
    }

    private void UpdateCrosshairTypePreview(int index)
    {
        // Update the crosshair sprite based on dropdown selection
        if (crosshairPreview != null && crosshairSprites.Length > index)
        {
            crosshairPreview.sprite = crosshairSprites[index];
        }

        // Save the selected crosshair type
        PlayerPrefs.SetInt("CrosshairType", index);
        PlayerPrefs.Save();
    }

    private void UpdateCrosshairColor(Button clickedButton)
    {
        // Get the color from the clicked button
        Color selectedColor = clickedButton.GetComponent<Image>().color;

        // Apply the color to the crosshair preview
        if (crosshairPreview != null)
        {
            crosshairPreview.color = selectedColor;
        }

        // Save the selected color
        PlayerPrefs.SetString("CrosshairColor", ColorUtility.ToHtmlStringRGBA(selectedColor));
        PlayerPrefs.Save();
    }

    private void UpdateTargetPreview(Button clickedButton)
    {
        // Get the color from the clicked button
        Color selectedColor = clickedButton.GetComponent<Image>().color;

        // Apply the color to the target preview
        if (targetPreview != null)
        {
            targetPreview.color = selectedColor;
        }

        // Save the selected color
        PlayerPrefs.SetString("TargetColor", ColorUtility.ToHtmlStringRGBA(selectedColor));
        PlayerPrefs.Save();
    }

    private void LoadSavedSettings()
    {
        // Load saved crosshair type
        if (PlayerPrefs.HasKey("CrosshairType"))
        {
            int savedType = PlayerPrefs.GetInt("CrosshairType");
            crosshairTypeDropdown.value = savedType;
            UpdateCrosshairTypePreview(savedType);
        }

        // Load saved crosshair color
        if (PlayerPrefs.HasKey("CrosshairColor"))
        {
            if (
                ColorUtility.TryParseHtmlString(
                    "#" + PlayerPrefs.GetString("CrosshairColor"),
                    out Color savedColor
                )
            )
            {
                crosshairPreview.color = savedColor;
            }
        }

        // Load saved target color
        if (PlayerPrefs.HasKey("TargetColor"))
        {
            if (
                ColorUtility.TryParseHtmlString(
                    "#" + PlayerPrefs.GetString("TargetColor"),
                    out Color savedColor
                )
            )
            {
                targetPreview.color = savedColor;
            }
        }
    }
}
