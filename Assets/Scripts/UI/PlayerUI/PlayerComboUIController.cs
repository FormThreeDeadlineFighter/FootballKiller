using UnityEngine;
using TMPro;
using UnityEngine.UI
;

public class PlayerComboUIController : MonoBehaviour
{
    [SerializeField] Image ComboGrade;
    [SerializeField] Image ComboValue;
    [SerializeField] PlayerEvent _playerEvents; 
    [SerializeField] Sprite S;
    [SerializeField] Sprite A;
    [SerializeField] Sprite B;
    [SerializeField] Sprite C;
    [SerializeField] Sprite D;
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
        
        if(currentValue >= (int)global::ComboGrade.Max)
        {
            currentValue = (int)global::ComboGrade.Max;
        }
        else if(currentValue <= 0)
        {
            currentValue = 0;
        }
        
        float percentage = (currentValue - _minGradeValue) / _maxGradeValue * 0.75f;
        ComboValue.fillAmount = percentage;
    }
    
    private void ComboValueCahnge(ComboGrade grade)
    { 
        _currentGrade = grade;      
        switch(_currentGrade)
        {
            case global::ComboGrade.S:
                _maxGradeValue = global::ComboGrade.Max - global::ComboGrade.S;
                _minGradeValue = (int)global::ComboGrade.S;
                ComboGrade.sprite = S;        
            break;
            case global::ComboGrade.A:
                _maxGradeValue = global::ComboGrade.S - global::ComboGrade.A;
                _minGradeValue = (int)global::ComboGrade.A;
                ComboGrade.sprite = A; 
            break;
            case global::ComboGrade.B:
                _maxGradeValue = global::ComboGrade.A - global::ComboGrade.B;
                _minGradeValue = (int)global::ComboGrade.B;
                ComboGrade.sprite = B; 
            break;
            case global::ComboGrade.C:
                _maxGradeValue = global::ComboGrade.B - global::ComboGrade.C;
                _minGradeValue = (int)global::ComboGrade.C;
                ComboGrade.sprite = C; 
            break;
            default:
                _maxGradeValue = global::ComboGrade.C - global::ComboGrade.D;
                _minGradeValue = (int)global::ComboGrade.D;
                ComboGrade.sprite = D; 
            break;
        } 
    }
}
