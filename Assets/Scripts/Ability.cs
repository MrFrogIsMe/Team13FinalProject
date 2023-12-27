using UnityEngine;

public enum AbilityState { ready, active, cooldown }

public class Ability : MonoBehaviour
{
    public new string name;
    public KeyCode key;
    public float cooldownTime;
    float currCooldownTime;
    public float activeTime;
    float currActiveTime;

    public AbilityState state = AbilityState.ready;

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