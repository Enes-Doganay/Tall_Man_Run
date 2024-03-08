using System.IO;
using UnityEngine;

public class DataController : Singleton<DataController>
{
    [SerializeField] private PlayerSkinDatabase skinDB;
    private const string fileName = "Data.txt";     // The name of the file where Data will be saved
    private GameData gameData;
    public GameData GameData => gameData;

    protected override void Awake()
    {
        base.Awake();

        Load();
    }
    public void NewGame()
    {
        gameData = new GameData(); // Create a new game data
        var defaultSkin = skinDB.GetDefaultPlayerSkin(); // Find the default skin
        gameData.ownedSkins.Add(defaultSkin); // Add to owned skins
        gameData.selectedSkinID = defaultSkin.SkinID; // Assign the selected skin ID to the variable holding the selected ID
        Save();
    }
    public void Save()
    {
        string json = JsonUtility.ToJson(gameData); // Convert the data inside the gameData object to JSON format
        File.WriteAllText(Path.Combine(Application.persistentDataPath, fileName), json); // Write the data to a file
    }

    public void Load()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName); // Combine the device's persistent data path and the filename to create the file path
        if (File.Exists(path)) // If the file exists, proceed
        {
            string json = File.ReadAllText(path); // Read the JSON data from the file
            gameData = JsonUtility.FromJson<GameData>(json); // Convert the JSON data to type GameData and assign it to the gameData variable
        }
        else
        {
            NewGame(); // If the file does not exist, start a new game
            Debug.Log("new game");
        }
    }
}
