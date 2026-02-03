using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] GameObject[] _enemys;
    
    public GameObject[] Enemys { get { return _enemys;} }

}
