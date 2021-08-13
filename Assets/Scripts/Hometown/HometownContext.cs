using Project.Components;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Hometown
{
    [RequireComponent(typeof(InputManager) , typeof(Spawner))]
    public class HometownContext : MonoBehaviour
    {
        [SerializeField]
        private InputManager _inputManager;
        [SerializeField]
        private Spawner _spawner;

        public HouseController houseController { get; private set; }
        public HouseView houseView;
        

        private void Awake()
        {
            if(_inputManager == null)
            {
                _inputManager = GetComponent<InputManager>();
            }

            if (_spawner == null)
            {
                _spawner =  GetComponent<Spawner>();
            }

            //add implementation
            houseController = new HouseController(this, "level", _inputManager);
            houseView.Setup(houseController);
            houseController.OnLevelUp += houseView.HandleOnHouseLevelUp;

            _spawner.EnableScript();
            houseController.TriggerSpawn += _spawner.HandleOnSpawnTriggered;

            _inputManager.OnInputTouch += houseController.HandleOnInputTouch;

        }

        private void OnDestroy()
        {
            //add implementation
        }
    }
}