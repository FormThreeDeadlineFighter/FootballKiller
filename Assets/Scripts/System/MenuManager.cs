using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Object")]
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _stageMenuUI;

    [Header("First Selected object")]
    [SerializeField] private GameObject _mainMenuFirst;
    [SerializeField] private GameObject _stageMenuFirst;

    public void SwitchStage()
    {
        _mainMenuUI.SetActive(false);
        _stageMenuUI.SetActive(true);
    }
}
