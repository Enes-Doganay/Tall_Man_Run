using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinData", menuName = "Data/CharacterSkinDB")]
public class PlayerSkinDatabase : ScriptableObject
{
    public List<PlayerSkinData> PlayerSkins = new List<PlayerSkinData>(); // A list containing all player skins

    public PlayerSkinData GetDefaultPlayerSkin() // A method returning the default skin
    {
        PlayerSkinData defaultSkin = PlayerSkins.Find(skin => skin.Cost == 0); // Assign the skin with a cost of 0 as the default skin
        return defaultSkin; // and return it
    }
}

[System.Serializable]
public struct PlayerSkinData // A structure to hold skin data
{
    public int SkinID;
    public Color SkinColor;
    public int Cost;
}