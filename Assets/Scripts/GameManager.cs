using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public ElementSO currentElement;
    public List<ElementSO> unlockedElements;
    
    void Awake()
{
    if (Instance != null)
    {
        Destroy(gameObject);
        return;
    }
    Instance = this;
    DontDestroyOnLoad(gameObject);
}
}
