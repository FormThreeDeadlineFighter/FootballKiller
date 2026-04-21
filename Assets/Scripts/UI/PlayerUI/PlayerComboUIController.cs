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

    }
    void OnDisable()
    {
        
    }
    
    void Update()
    {
        
    }

}
