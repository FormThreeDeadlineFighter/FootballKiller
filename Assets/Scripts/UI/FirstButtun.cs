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

            EventSystem.current.firstSelectedGameObject = _firstButtun;
        }
        else
        {
            Debug.LogWarning(gameObject.name + " Page Enabled, but _firstButtun is NULL!");
        }
    }
    void OnDisable()
    {
        EventSystem.current.firstSelectedGameObject = null;
    }
}
