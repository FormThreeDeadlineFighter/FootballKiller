using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class MenuManager : MonoBehaviour
{
    [Header("Menu Object")]
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _stageMenuUI;

    [Header("First Selected object")]
    [SerializeField] private GameObject _mainMenuFirst;
    [SerializeField] private GameObject _stageMenuFirst;
    /*[Header("System")]
    [SerializeField] private InputActionAsset _inputAction;*/


    void Start()
    {
        _mainMenuUI.SetActive(true);
        _stageMenuUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }
    void Update()
    {

    }
    public void GameStart()
    {
        AudioManager.Instance.Play(1, "shoot", false);
        _mainMenuUI.SetActive(false);
        _stageMenuUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_stageMenuFirst);
    }
    public void ReturnToMenu()
    {
        AudioManager.Instance.Play(1, "shoot", false);
        _mainMenuUI.SetActive(true);
        _stageMenuUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }
}
