using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string _fileName;
    private GameData _gameData;
    private List<IDataPersistence> _dataPersistenceObjects;
    private FileDataHandler _dataHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more that one Data Persistence Manager in the scene");
        }
        instance = this;
    }
    void Start()
    {
        this._dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);
        this._dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this._gameData = new GameData();
    }
    
    public void LoadGame()
    {
        // Load any saved data from a file using the data handler
        this._gameData = _dataHandler.Load();
        
        // if  no data can be loaded, initialize to a new game
        if(this._gameData == null)
        {
            Debug.Log("NO data was found. Initializing data to defaults.");
            NewGame();
        }
        
        // push the loaded data to all other scripts that need it
        foreach(IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(_gameData);
        }
    }
    
    public void SaveGame()
    {
        // pass the data to other scripts so they can update it
        foreach(IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref _gameData);
        }

        // save tht data to a film using the data handler
        _dataHandler.Save(_gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistences = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).
            OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistences);
    }
}
