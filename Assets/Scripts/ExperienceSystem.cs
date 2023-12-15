using UnityEngine;
using UnityEngine.Events;

public class ExperienceSystem : MonoBehaviour
{
    public static ExperienceSystem Instance;
    public UnityEvent<int> OnExperienceChange;
    public UnityEvent OnLevelChange;

    public int exp;
    public int nextLevelExp;
    public int level;

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
        }
    }

    public void AddExperience(int amount)
    {
        exp += amount;
        if (exp >= nextLevelExp)
        {
            exp -= nextLevelExp;
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

