using UnityEngine;

public class AbstractBullet : MonoBehaviour
{
    private bool wasInit = false;

    private Object source;
    public Object Source
    {
        get { return source; }
    }

    public void init(Object source)
    {
        wasInit = true;
        this.source = source;
    }

    public void Update()
    {
        if (!wasInit)
        {
            Debug.Log("Object wasn't init. Object destroyed");
            Destroy(this);
        }
    }

}
