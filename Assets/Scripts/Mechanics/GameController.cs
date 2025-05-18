using Platformer.Core;
using Platformer.Model;
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

        //This model field is public and can be therefore be modified in the 
        //inspector.
        //The reference actually comes from the InstanceRegister, and is shared
        //shared reference when the scene loads, allowing the model to be
        //conveniently configured inside the inspector.
        public PlatformerModel model = Simulation.GetModel<PlatformerModel>();
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
                new List<string> { "NormalCard", "NormalCard", "NormalCard", "FireBall", "FireBall", "FireBall" },
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

        void Update()
        {
            if (Instance == this) Simulation.Tick();
        }
    }
}