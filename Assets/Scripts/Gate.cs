using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GateType
{
    Height,
    Thickness
}

public class Gate : MonoBehaviour
{
    public GateType gateType = GateType.Height;
    public MeshRenderer gateMeshRenderer;
    public TextMeshPro valueText;
    public Image header;
    public Image arrow;
    public Sprite positiveThicknessSprite;
    public Sprite negativeThicknessSprite;
    public Sprite positiveHeightSprite;
    public Sprite negativeHeightSprite;
    public Color headerRedColor;
    public Color headerBlueColor;
    public int value;
    public float alphaValue = 0.8f;
    private float heightMultiplier = 0.005f;
    private float thicknessMultiplier = 0.01f;


    private void Awake()
    {
        SetGate(); // Set up the gates at the beginning
    }

    private void OnValidate()
    {
        SetGate(); // Set up the gates when there is a change in values
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has a TransformChanger component
        if (other.gameObject.GetComponent<TransformChanger>() != null)
        {
            // Get the TransformChanger component from the object
            TransformChanger transformChanger = other.gameObject.GetComponent<TransformChanger>();
            if (gateType == GateType.Height)  // If the gate type is "Height"
            {
                // Change the player's height using TransformChanger and the value and multiplier
                transformChanger.ChangeHeight(value * heightMultiplier);
            }
            else
            {
                // Change the player's thickness using TransformChanger and the value and multiplier
                transformChanger.ChangeThickness(value * thicknessMultiplier);
            }
            // Deactivate the gate
            gameObject.SetActive(false);
        }
    }

    private void SetGate()
    {
        /*
        if (gateMeshRenderer == null || gateMeshRenderer.sharedMaterial == null)
            return;
        */
        if (gateMeshRenderer == null)
            return;

        // Create a new material, set it to transparent, and assign it to the meshRenderer
        if (gateMeshRenderer.sharedMaterial == null)
        {
            Material material = new Material(Shader.Find("Standard"));
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
            gateMeshRenderer.sharedMaterial = material;
        }

        // Create a new material instance for each gate
        Material gateMaterialInstance = new Material(gateMeshRenderer.sharedMaterial);

        // Set material and appearance settings based on the gate type
        switch (gateType)
        {
            case GateType.Height:
                if (value < 0)
                {
                    header.color = headerRedColor;
                    arrow.sprite = negativeHeightSprite;
                    gateMaterialInstance.color = new Color(headerRedColor.r, headerRedColor.g, headerRedColor.b, alphaValue);
                    valueText.text = value.ToString();
                }
                else
                {
                    header.color = headerBlueColor;
                    arrow.sprite = positiveHeightSprite;
                    gateMaterialInstance.color = new Color(headerBlueColor.r, headerBlueColor.g, headerBlueColor.b, alphaValue);
                    valueText.text = "+" + value;
                }
                break;

            case GateType.Thickness:
                if (value < 0)
                {
                    header.color = headerRedColor;
                    arrow.sprite = negativeThicknessSprite;
                    gateMaterialInstance.color = new Color(headerRedColor.r, headerRedColor.g, headerRedColor.b, alphaValue);
                    valueText.text = value.ToString();
                }
                else
                {
                    header.color = headerBlueColor;
                    arrow.sprite = positiveThicknessSprite;
                    gateMaterialInstance.color = new Color(headerBlueColor.r, headerBlueColor.g, headerBlueColor.b, alphaValue);
                    valueText.text = "+" + value;
                }
                break;
        }

        // Assign the new material instance to the gate
        gateMeshRenderer.material = gateMaterialInstance;
    }
}
