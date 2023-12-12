using UnityEngine;
using UnityEngine.Events;

public class ExperienceSystem : MonoBehaviour
{
    public static ExperienceSystem Instance;
    public UnityEvent<int> OnExperienceChange;
    public UnityEvent OnLevelChange;

    int exp;
    int nextLevelExp;
    int level;

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
}

