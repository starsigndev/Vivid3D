namespace Vivid.Scene;

public class SceneEvent
{

    public string EventName
    {
        get;
        set;
    }

    public SceneEvent(string name)
    {

        EventName = name;

    }
    
    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        
    }

    public virtual void Render()
    {
        
    }

    public virtual void Stop()
    {
        
    }
    
}