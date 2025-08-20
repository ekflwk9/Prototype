using UnityEngine;

public abstract class UiBase : MonoBehaviour
{
    public virtual void On() => this.gameObject.SetActive(true);
    public virtual void Off() => this.gameObject.SetActive(false);
}
