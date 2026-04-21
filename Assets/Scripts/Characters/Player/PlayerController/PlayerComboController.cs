using System.Collections;
using UnityEngine;

public class PlayerComboController : MonoBehaviour
{
    [SerializeField] int _maxComboValue;
    [SerializeField] float _comboReduceSpeed = -50;
    [SerializeField] float _comboHoldTime;
    [SerializeField] PlayerEvent _playerEvents;
    private ComboGrade _grade;
    private float _comboValue;
    //private Coroutine _comboHoldCoroutine;
    

    void OnEnable()
    {
        _comboValue = 0;
        _grade = ComboGrade.D;
        _maxComboValue = (int)ComboGrade.Max;
    }

    void OnDisable()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
         
    } 
}

public enum ComboGrade
{
    D = 0,
    C = 50,
    B = 100,
    A = 200,
    S = 500,
    Max = 700
}
