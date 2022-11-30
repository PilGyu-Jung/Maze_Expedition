using UnityEditor;
using UnityEngine;

namespace Defines
{
    public static class Define 
    {
		[System.Serializable]
		public class Wave
		{
			[Header("-------Enemy-------")]
			public bool infinite;
			public int enemyCount;

			public float moveSpeedA;
			public float moveSpeedB;
			public float enemyHealthA;
			public float enemyHealthB;

			public int hitsToKillPlayer;

			public Color skinColour;
			public float timeBetweenSpawns;

			[Header("-------Types-------")]
			public int spawnRarity_A;
			public int spawnRarity_B;

			[Header("-------Items-------")]
			public int count_ItemHealth;
			public int count_ItemAmmo;
			public float minSpawnTime_Heal;
			public float maxSpawnTime_Heal;
			public float minSpawnTime_Ammo;
			public float maxSpawnTime_Ammo;

		}
	}
}