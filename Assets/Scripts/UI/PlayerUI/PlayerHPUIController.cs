using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUIController : MonoBehaviour
{
    [SerializeField] Image _HPUI;
    [SerializeField] Image _backgroundHP;
    [SerializeField] float _delayTime = 0.5f;
    [SerializeField] PlayerEvent _playerEvents;
    Coroutine hpCoroutine;
    void OnEnable()
    {
        _playerEvents.OnPlayerHPChange += HPUICahnge;
    }
    void OnDisable()
    {
        _playerEvents.OnPlayerHPChange -= HPUICahnge;
    }
    
    private void HPUICahnge(float value)
    {
        value = value * 0.9f + 0.1f;
        _HPUI.fillAmount = value;
        IEnumerator enumerator = HPDelayReduce(value);
        
        if (hpCoroutine != null)
        {      
            StopCoroutine(hpCoroutine);
        }
        hpCoroutine = StartCoroutine(enumerator);
    }
    
    IEnumerator HPDelayReduce(float target)
    {
        yield return new WaitForSeconds(0.5f);
        
        float start = _backgroundHP.fillAmount;
        float time = 0;

        while (time < _delayTime)
        {
            time += Time.deltaTime;

            _backgroundHP.fillAmount = Mathf.MoveTowards(start, target, time / _delayTime);

            yield return null;
        }

        _backgroundHP.fillAmount = target;
        hpCoroutine = null;
    }
}
