using UnityEngine;

public enum AbilityState { ready, active, cooldown }

public class Ability : MonoBehaviour
{
    protected Player player;
    protected Transform tf;
    protected Rigidbody rb;

    public new string name;
    public KeyCode key;
    public float cooldownTime;
    float currCooldownTime;
    public float activeTime;
    float currActiveTime;
    
    public Sprite abilityImage;
    public AbilityState state = AbilityState.ready;

    public int getCurrCooldownTime() {
        return (int)currCooldownTime;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        tf = player.GetComponent<Transform>();
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        switch (state)
        {
            case AbilityState.ready:
                if (Input.GetKeyDown(key))
                {
                    state = AbilityState.active;
                    this.Activate();
                    currActiveTime = activeTime;
                }
            break;
            case AbilityState.active:
                if (currActiveTime > 0)
                {
                    currActiveTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.cooldown;
                    Deactivate();
                    currCooldownTime = cooldownTime;
                }
            break;
            case AbilityState.cooldown:
                if (currCooldownTime > 0)
                {
                    currCooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
            break;
        }
    }

    public virtual void Activate() { ; }
    public virtual void Deactivate() { ; }
}
