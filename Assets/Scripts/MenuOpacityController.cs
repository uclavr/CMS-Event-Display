using UnityEngine;
using UnityEngine.UI;

public class MenuOpacityController : MonoBehaviour
{
    // References to the UI components
    public GameObject panel;  // The panel to control
    public Slider opacitySlider;  // The slider to control opacity

    private Image panelImage;  // Reference to the Image component
    private const float minAlpha = 100f / 255f;  // Minimum alpha (100/255)
    private const float maxAlpha = 1f;  // Maximum alpha (255/255)

    void Start()
    {
        // Get the Image component from the panel
        panelImage = panel.GetComponent<Image>();

        // Ensure the panel has an Image component
        if (panelImage == null)
        {
            Debug.LogError("No Image component found on the panel!");
            return;
        }

        // Initialize the slider value to 1 (fully opaque)
        opacitySlider.value = minAlpha;

        // Set the initial opacity to max alpha (255/255)
        UpdateOpacity(opacitySlider.value);

        // Add listener for when the slider value changes
        opacitySlider.onValueChanged.AddListener(UpdateOpacity);
    }

    // Method to update the opacity of the panel's image based on the slider value
    void UpdateOpacity(float value)
    {
        // Map the slider value (0-1) to the desired alpha range (minAlpha to maxAlpha)
        float clampedAlpha = Mathf.Lerp(minAlpha, maxAlpha, value);

        if (panelImage != null)
        {
            Color currentColor = panelImage.color;
            panelImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, clampedAlpha); // Change only the alpha
        }
    }
}