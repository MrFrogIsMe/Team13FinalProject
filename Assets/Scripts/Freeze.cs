using UnityEngine;

public class Freeze : Ability
{
    GameObject[] monsters;

    public override void Activate()
    {
        monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject monster in monsters)
        {
            monster.GetComponent<Monster>().isFreezing = true;
        }
    }

    public override void Deactivate()
    {
        foreach (GameObject monster in monsters)
        {
            monster.GetComponent<Monster>().isFreezing = false;
        }
    }

}
