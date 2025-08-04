using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class exposes the the game model in the inspector, and ticks the
    /// simulation.
    /// </summary> 
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        public GameObject Player;
        public HandController handController;

        private void Awake()
        {
            Debug.Log("正在初始化卡牌数据库");
            CardDB.Init();
            Player = GameObject.Find("Player");
            handController = Player.GetComponent<HandController>();
            Debug.Log("正在初始化牌组");
            handController.InitializeDrawPile
                (
                new List<string> { "NormalCard", "NormalCard", "ThunderAttack", "FireAttack", "FireAttack", "FireAttack" },
                Player
                );
        }

        void OnEnable()
        {
            Instance = this;
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }
    }
}