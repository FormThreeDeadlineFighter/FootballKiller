using UnityEngine;

public class Click : MonoBehaviour
{
    public void click()
    {
        AudioManager.Instance.Play(0,"click",false);
    }
}
