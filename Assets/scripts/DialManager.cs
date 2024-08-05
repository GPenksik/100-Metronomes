using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialManager : MonoBehaviour
{
    public static DialManager Instance { get; private set; }

    private List<Playeranimation> playerAnimations;

    private void Awake()
    {
        // 确保只有一个实例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 跨场景保持管理器实例
            playerAnimations = new List<Playeranimation>();
        }
        else
        {
            Destroy(gameObject); // 如果已经有一个实例，销毁新的实例
        }
    }

    private void Start()
    {
        // 查找场景中的所有 Playeranimation 对象并添加到列表中
        Playeranimation[] players = FindObjectsOfType<Playeranimation>();
        playerAnimations.AddRange(players);
    }

    public void AddPlayerAnimation(Playeranimation player)
    {
        if (!playerAnimations.Contains(player))
        {
            playerAnimations.Add(player);
        }
    }

    public void RemovePlayerAnimation(Playeranimation player)
    {
        if (playerAnimations.Contains(player))
        {
            playerAnimations.Remove(player);
        }
    }

    public Playeranimation GetPlayerAnimation(int index)
    {
        if (index >= 0 && index < playerAnimations.Count)
        {
            return playerAnimations[index];
        }
        return null;
    }

    public List<Playeranimation> GetAllPlayerAnimations()
    {
        return new List<Playeranimation>(playerAnimations);
    }
}
