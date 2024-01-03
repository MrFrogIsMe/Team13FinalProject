using UnityEngine;
using UnityEngine.Events;

public class ExperienceSystem : MonoBehaviour
{
    public static ExperienceSystem Instance;
    public UnityEvent<int> OnExperienceChange;
    public UnityEvent OnLevelChange;
    public expBar expBar;

    public int exp = 0;
    public int nextLevelExp = 100;
    public int level = 1;

    public GameObject LevelUP;

    public GameObject player;

    // Singleton
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            exp = 0;
            level = 1;
            expBar.setLevel(level,nextLevelExp);
        }
    }

    public void AddExperience(int amount)
    {
        exp += amount;
        print($"exp = {exp}, amount = {amount}, nextLevelExp = {nextLevelExp}");
        while (exp >= nextLevelExp)
        {
            exp -= nextLevelExp;
            ++level;
            Instantiate(LevelUP, player.transform.position,this.transform.rotation);
            nextLevelExp = (int)(nextLevelExp * 1.2f);
            OnLevelChange?.Invoke();
            expBar.setLevel(level, nextLevelExp);
        }
        OnExperienceChange?.Invoke(amount);
        expBar.setExp(exp);
    }

    public int GetExperience()
    {
        return exp;
    }
    
    public int GetLevel()
    {
        return level;
    }
}

