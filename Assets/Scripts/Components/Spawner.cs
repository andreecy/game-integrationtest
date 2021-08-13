using System.Collections.Generic;
using UnityEngine;

namespace Project.Components
{

    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private MoveableComponent _moveableComponent;

        public Transform spawnLocation;

        // store pooled object
        private List<GameObject> pooledObjects = new List<GameObject>();
        private int poolAmount = 1;

        private void OnDisable()
        {
            //add implementation
        }

        private void OnEnable()
        {
            //add implementation
        }

        public void Setup(ICanTriggerSpawn spawnTrigger)
        {
            //add implementation
        }

        public void EnableScript()
        {
            //remember to enable script from context if needed
            enabled = true;
        }

        public void HandleOnSpawnTriggered()
        {
            //add implementation
            SpawnMoveableObject();
        }

        private void SpawnMoveableObject()
        {
            //add implementation

            Debug.Log("Tank spawned");
            //spawn object from pool
            GameObject tank = GetPooledObject();

            if(tank != null)
            {
                tank.SetActive(true);
            }
        }

        private void Start()
        {
            // initiate object to pool
            for(int i = 0; i < poolAmount; i++)
            {
                MoveableComponent obj = Instantiate(_moveableComponent);
                obj.transform.position = spawnLocation.position;
                obj.SetDestination(new Vector3(5f, spawnLocation.position.y, spawnLocation.position.z));
                obj.gameObject.SetActive(false);
                pooledObjects.Add(obj.gameObject);
            }
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }

            return null;
        }
    }
}