using System.Collections.Generic;
using UnityEngine;

public class TransformChanger : MonoBehaviour
{
    [SerializeField] private List<Transform> thicknessPieces;
    [SerializeField] private Transform upperBody;
    [SerializeField] private Transform torso;
    [SerializeField] private Transform root;
    private CapsuleCollider capsuleCollider;
    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void ChangeHeight(float value)
    {
        torso.localScale += new Vector3(0, value, 0);
        upperBody.position += new Vector3(0, value * 2, 0);

        capsuleCollider.height += value * 2;
        capsuleCollider.center += new Vector3(0, value, 0);
    }

    public void ChangeThickness(float value)
    {
        foreach(Transform item in thicknessPieces)
        {
            item.localScale += new Vector3(value, 0, value);
        }
        root.localScale += new Vector3(value, value * 0.5f, value);
    }
}