using System.Collections;
using UnityEngine;

public class PlayerComboController : MonoBehaviour
{
    [SerializeField] PlayerEvent playerEvent;
    [SerializeField] int _maxComboValue;
    [SerializeField] PlayerEvent _playerEvents;
    private ComboGrade _grade;
    private float _comboValue;
    private bool _comboHold;

    void OnEnable()
    {
        _playerEvents.OnPlayerComboChange += ChangeComboValue;
        
        _comboValue = 0;
        _grade = ComboGrade.D;
        _maxComboValue = (int)ComboGrade.Max;
        _comboHold = false;
    }

    void OnDisable()
    {
        _playerEvents.OnPlayerComboChange -= ChangeComboValue;
    }
    
    // Update is called once per frame
    void Update()
    {
        CalculateComboGrade();
        _playerEvents.PlayerComboChange(-10 * Time.deltaTime);
    }
    
    void ChangeComboValue(float value)
    {
        _comboValue += value;
        
        if(_comboValue <= 0)
        {
            _comboValue = 0;
        }
        else if(_comboValue >= _maxComboValue)
        {
            _comboValue = _maxComboValue;
        }
        //ComboHoldTime(1);
    }
    void CalculateComboGrade()
    {
        switch(_comboValue)
        {
            case >= (int)ComboGrade.S:
            _grade = ComboGrade.S;
            break;
            case >= (int)ComboGrade.A:
            _grade = ComboGrade.A;
            break;
            case >= (int)ComboGrade.B:
            _grade = ComboGrade.B;
            break;
            case >= (int)ComboGrade.C:
            _grade = ComboGrade.C;
            break;
            default: _grade = ComboGrade.D;
            break;
        }
        _playerEvents.ComboGradeChange(_grade);
    }
    
    IEnumerator ComboHoldTime(float time)
    {
        _comboHold = true;
        yield return new WaitForSeconds(time);
        _comboHold = false;
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
