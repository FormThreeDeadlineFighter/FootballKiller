using UnityEngine;
using UnityEngine.EventSystems;

public class FirstButtun : MonoBehaviour
{
    [SerializeField] private GameObject _nowUI;
    [SerializeField] private GameObject _firstButtun;
    bool on = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_nowUI.activeSelf == true && on == false)
        {
            EventSystem.current.SetSelectedGameObject(_firstButtun);
            on = true;
        }
        if(_nowUI.activeSelf == false && on == true)
        {
            on = false;
        }
        Debug.Log(_nowUI.name + 
          " self: " + _nowUI.activeSelf + 
          " inHierarchy: " + _nowUI.activeInHierarchy);
    }

}
