using UnityEngine;
using TMPro;
using UnityEngine.UI
;

public class PlayerComboUIController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI ComboText;
    [SerializeField] Image ComboValue;
    [SerializeField] PlayerEvent _playerEvents; 
    ComboGrade _currentGrade;
    int _maxGradeValue;
    int _minGradeValue;
    float currentValue;

    
    void OnEnable()
    {
        _playerEvents.OnPlayerComboChange += ValueUIBarChange;
        _playerEvents.OnComboGradeChange += ComboValueCahnge;
    }
    void OnDisable()
    {
        _playerEvents.OnPlayerComboChange -= ValueUIBarChange;
        _playerEvents.OnComboGradeChange -= ComboValueCahnge;
    }
    void Update()
    {
        
    }
    private void ValueUIBarChange(float value)
    {
        currentValue += value;
        
        if(currentValue >= (int)ComboGrade.Max)
        {
            currentValue = (int)ComboGrade.Max;
        }
        else if(currentValue <= 0)
        {
            currentValue = 0;
        }
        
        float percentage = (currentValue - _minGradeValue) / _maxGradeValue;
        Debug.Log(percentage);
        ComboValue.transform.localScale = new Vector3(percentage, 1, 1);
    }
    
    private void ComboValueCahnge(ComboGrade grade)
    { 
        _currentGrade = grade;      
        switch(_currentGrade)
        {
            case ComboGrade.S:
            _maxGradeValue = ComboGrade.Max - ComboGrade.S;
            _minGradeValue = (int)ComboGrade.S;
            break;
            case ComboGrade.A:
            _maxGradeValue = ComboGrade.S - ComboGrade.A;
            _minGradeValue = (int)ComboGrade.A;
            break;
            case ComboGrade.B:
            _maxGradeValue = ComboGrade.A - ComboGrade.B;
            _minGradeValue = (int)ComboGrade.B;
            break;
            case ComboGrade.C:
            _maxGradeValue = ComboGrade.B - ComboGrade.C;
            _minGradeValue = (int)ComboGrade.C;
            break;
            default: 
            _maxGradeValue = ComboGrade.C - ComboGrade.D;
            _minGradeValue = (int)ComboGrade.D;
            break;
        } 
        ComboText.text = grade.ToString();
    }
}
