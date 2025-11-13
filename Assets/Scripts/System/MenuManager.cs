using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Mono.Cecil.Cil;
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

    void Start()
    {
        _mainMenuUI.SetActive(true);
        _stageMenuUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }

    public void GameStart()
    {
        AudioManager.Instance.Play(1, "shoot", false);
        _mainMenuUI.SetActive(false);
        Instantiate(_player);
    }
    public void LoadGame()
    {
        AudioManager.Instance.Play(1, "shoot", false);
        _mainMenuUI.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("XD");
            _stageMenuUI.SetActive(true);
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _stageMenuUI.SetActive(false);
        }
    }
    public void ReturnToMenu()
    {
        AudioManager.Instance.Play(1, "shoot", false);
        _mainMenuUI.SetActive(true);
        _stageMenuUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }
}
