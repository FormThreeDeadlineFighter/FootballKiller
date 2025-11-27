using UnityEngine;
using UnityEngine.EventSystems;

public class FirstButtun : MonoBehaviour
{
    [SerializeField] private GameObject _nowUI;
    [SerializeField] private GameObject _firstButtun;
    void OnEnable()
    {
        if(_firstButtun != null)
        {
            EventSystem.current.SetSelectedGameObject(null);

            EventSystem.current.SetSelectedGameObject(_firstButtun);
            Debug.Log(gameObject.name + " Page Enabled. Focus set to: " + _firstButtun.name);
        }
        else
        {
            Debug.LogWarning(gameObject.name + " Page Enabled, but _firstButtun is NULL!");
        }
    }
    void OnDisable()
    {
        if(EventSystem.current.currentSelectedGameObject == _firstButtun)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        Debug.Log(gameObject.name + " Page Disabled.");
    }
}
