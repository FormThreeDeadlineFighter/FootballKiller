using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnNewGameClicked()
    {
        DataPersistenceManager.instance.NewGame();
    }
    
    public void OnLoadGameClicked()
    {
        DataPersistenceManager.instance.LoadGame();
    }
    
    public void OnSaveGameClicked()
    {
        DataPersistenceManager.instance.SaveGame();
    }
}
