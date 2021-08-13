using Project.Components;
using System;
using UnityEngine;

namespace Project.Hometown
{
    public class HouseController : IController, IUpgradeable , ICanTriggerSpawn
    {
        public event Action<LevelupEventData> OnLevelUp;
        public event Action TriggerSpawn;

        private HometownContext _hometownContext;
        private string _itemName;
        private UpgradeableData _upgradeableData;
        UpgradeableRepository upgradeableRepository;

        public void OnContextDispose()
        {
            //add implementation
        }

        public HouseController(HometownContext hometownContext , string upgradeableItemName , InputManager inputManager)
        {
            _hometownContext = hometownContext;
            _itemName = upgradeableItemName;

            //add implementation
            upgradeableRepository = new UpgradeableRepository(hometownContext);
            upgradeableRepository.GetUpgradeableData(OnGetRepositoryDataCompleted);
        }

        public void Upgrade()
        {
            Debug.Log($"Handle Upgrade {_itemName}");
            //add implementation
            if(_upgradeableData.Level < _upgradeableData.MaxLevel)
            {
                _upgradeableData.LevelUp();
                //Debug.Log("level :" + _upgradeableData.Level);
                LevelupEventData levelupEventData = new LevelupEventData(_upgradeableData.Level, _upgradeableData.MaxLevel);
                OnLevelUp?.Invoke(levelupEventData);

                if(_upgradeableData.Level == _upgradeableData.MaxLevel)
                {
                    TriggerSpawn?.Invoke();
                }
            }
        }

        void OnGetRepositoryDataCompleted(UpgradeableData data)
        {
            _upgradeableData = data;
            Debug.Log("Get Repository Data..");
            Debug.Log("level :" + _upgradeableData.Level);
            Debug.Log("max level:" + _upgradeableData.MaxLevel);
            
            //LevelupEventData levelupEventData = new LevelupEventData(_upgradeableData.Level, _upgradeableData.MaxLevel);
            //OnLevelUp?.Invoke(levelupEventData);
        }

        public void HandleOnInputTouch()
        {
            Upgrade();
        }

    }
}