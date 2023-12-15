using UnityEngine;
using UnityEngine.Events;

public class ExperienceSystem : MonoBehaviour
{
    public static ExperienceSystem Instance;
    public UnityEvent<int> OnExperienceChange;
    public UnityEvent OnLevelChange;

    public int exp = 0;
    public int nextLevelExp = 10;
    public int level = 1;

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
        }
    }

    public void AddExperience(int amount)
    {
        exp += amount;
        print($"exp = {exp}, amount = {amount}, nextLevelExp = {nextLevelExp}");
        if (exp >= nextLevelExp)
        {
            exp = 0;
            ++level;
            OnLevelChange?.Invoke();
        }
        OnExperienceChange?.Invoke(amount);
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

