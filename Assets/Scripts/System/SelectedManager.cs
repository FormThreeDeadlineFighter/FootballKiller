using UnityEngine;
using UnityEngine.EventSystems;
public class SelectedManager : MonoBehaviour
{
    [Header("First Selected object")]
    [SerializeField] private GameObject _First;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(_First);
    }
    
}
