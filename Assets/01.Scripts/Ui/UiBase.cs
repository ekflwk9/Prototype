using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    public abstract void Init();

    public virtual void OnUI()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void OffUI()
    {
        this.gameObject.SetActive(false);
    }

}