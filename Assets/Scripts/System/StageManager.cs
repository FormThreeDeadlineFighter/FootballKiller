using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class StageManager : MonoBehaviour
{
    [Header("Canva")]
    [SerializeField] private Canvas _canvaUI;

    [Header("Menu Object")]
    [SerializeField] private GameObject _stageMenuUI;

    [Header("First Selected object")]
    [SerializeField] private GameObject _stageMenuFirst;
    GameObject _currentUI;
    /*private void Awake()
    {
        if (_canvaUI == null)
        {
            Canvas any = Object.FindAnyObjectByType<Canvas>();
            Canvas first = Object.FindFirstObjectByType<Canvas>();
            _canvaUI = PickBestCanvas(any, first);
        }
    }*/
    public void Stage()
    {
        _currentUI = Instantiate(_stageMenuUI, _canvaUI.transform);
        //EventSystem.current.SetSelectedGameObject(_stageMenuFirst);
    }
}
