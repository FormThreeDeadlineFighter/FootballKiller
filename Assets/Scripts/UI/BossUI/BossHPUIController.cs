using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHPUIController : MonoBehaviour
{
    [SerializeField] Image HPUI;
    [SerializeField] Image _backgroundHP;
    [SerializeField] float _delayTime = 0.5f;
    [SerializeField] BossEvent _bossEvent;
    Coroutine hpCoroutine;
    
    void OnEnable()
    {
        _bossEvent.OnBossHPCahange += HPUICahnge;
    }
    void OnDisable()
    {
        _bossEvent.OnBossHPCahange -= HPUICahnge;
    }
    
    private void HPUICahnge(float value)
    {
        HPUI.fillAmount = value;
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
            _backgroundHP.fillAmount = Mathf.Lerp(start, target, time / _delayTime);
            yield return null;
        }
        hpCoroutine = null;
    }
}
