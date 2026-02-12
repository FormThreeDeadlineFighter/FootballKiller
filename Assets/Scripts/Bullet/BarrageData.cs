using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrageData : MonoBehaviour
{
    [SerializeField] public Transform attackTransform;
    [SerializeField] public List<BulletArrayData> _bulletsArray = new();
    Coroutine _stootCoroutine;
    
    public void StartShoot()
    {
        if(attackTransform == null) return;
        if(_bulletsArray == null) return;
        if(_stootCoroutine != null) return;
        _stootCoroutine = StartCoroutine(Shoot());
    }
    
    IEnumerator Shoot()
    {
        for(int i = 0; i < _bulletsArray.Count; i++)
        {   
            for(int j = 0; j < _bulletsArray[i].Bullets.Count; j++)
            {   
                float height = attackTransform.position.y + _bulletsArray[i].Bullets[j].Height;
                Vector3 position = new Vector3(attackTransform.position.x, height, attackTransform.position.z);
                Quaternion rotate = attackTransform.rotation * Quaternion.Euler(_bulletsArray[i].Bullets[j].Angle.y, _bulletsArray[i].Bullets[j].Angle.x, 0);;
                Instantiate(_bulletsArray[i].Bullets[j].Bullet, position, rotate);
            }    
            yield return new WaitForSeconds(_bulletsArray[i].DelayTime);
        }
        StopCoroutine(_stootCoroutine);
        _stootCoroutine = null;
    }
}
