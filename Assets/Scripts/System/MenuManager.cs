using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
//using Mono.Cecil.Cil;
public class MenuManager : MonoBehaviour
{
    [Header("Menu Object")]
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _stageMenuUI;

    [Header("First Selected object")]
    [SerializeField] private GameObject _mainMenuFirst;
    [SerializeField] private GameObject _stageMenuFirst;
    [Header("System")]
    [SerializeField] private Canvas _canva;
    [SerializeField] private GameObject _player;

    /*void Start()
    {
        _mainMenuUI.SetActive(true);
        _stageMenuUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }
    void Update()
    {
        if(_mainMenuUI.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
        }
        if(_stageMenuUI.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(_stageMenuFirst);
        }
    }*/

    public void GameStart()
    {
        AudioManager.Instance.Play(0, "click", false);
        _mainMenuUI.SetActive(false);
        _stageMenuUI.SetActive(true);
    }
}
