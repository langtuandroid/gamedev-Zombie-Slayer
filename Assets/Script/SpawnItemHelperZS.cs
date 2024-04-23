using UnityEngine;

namespace Script
{
	public class SpawnItemHelperZS : MonoBehaviour {
		public bool spawnWhenHit = false;
		public bool spawnWhenDie = true;
		[Range(0,1)]
		public float chanceSpawn=0.5f;
		public GameObject[] Items;
		public Transform spawnPoint;

		private void Start(){
			if (spawnPoint == null)
				spawnPoint = transform;
		}

		public void Spawn(){
			if (Items.Length > 0 && Random.Range (0f, chanceSpawn) < chanceSpawn) {
				Instantiate(Items[Random.Range(0, Items.Length)], spawnPoint.position, Quaternion.identity);
			}
		}
	}
}
