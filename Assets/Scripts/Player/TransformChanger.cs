using System.Collections.Generic;
using UnityEngine;

public class TransformChanger : MonoBehaviour
{
    [SerializeField] private List<Transform> thicknessPieces;
    [SerializeField] private Transform upperBody;
    [SerializeField] private Transform torso;
    [SerializeField] private Transform root;

    [SerializeField] private float defaultThicknessMultiplier = 0.01f;
    [SerializeField] private float defaultHeightMultiplier = 0.005f;

    public float DefaultThicknessMultiplier => defaultThicknessMultiplier;
    public float DefaultHeightMultiplier => defaultHeightMultiplier;

    private CapsuleCollider capsuleCollider;
    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();

        // Get saved thickness and height level at the start
        int thicknessLevel = DataController.Instance.GameData.thicknessLevel;
        int heightLevel = DataController.Instance.GameData.heightLevel;

        // Adjust sizes according to the saved level
        ChangeThickness(thicknessLevel * defaultThicknessMultiplier);
        ChangeHeight(heightLevel * defaultHeightMultiplier);
    }

    // Change the height
    public void ChangeHeight(float value)
    {
        torso.localScale += new Vector3(0, value, 0);
        upperBody.position += new Vector3(0, value * 2, 0);

        capsuleCollider.height += value * 2;
        capsuleCollider.center += new Vector3(0, value, 0);

        // If the torso's size is less than 0.1, kill the character
        if (torso.localScale.y < 0.1f)
        {
            GetComponent<PlayerDead>().Death();
        }
    }

    // Adjust thickness pieces
    public void ChangeThickness(float value)
    {
        foreach (Transform item in thicknessPieces)
        {
            item.localScale += new Vector3(value, 0, value);
        }
        root.localScale += new Vector3(value, value * 0.5f, value);

        // If the root's size is less than 0.1, kill the character
        if (root.localScale.x < 0.1f)
        {
            GetComponent<PlayerDead>().Death();
        }
    }
}