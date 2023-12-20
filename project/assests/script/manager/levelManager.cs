
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class levelManager : MonoBehaviour
{
	static public int level = 0;
	static List<GameObject> monsterList = new List<GameObject>(), spawnList = new List<GameObject>();
	public GameObject[] w1, w2, w3, w4, boss, prefab;
	static public int gameMode = 0, world = 0;
	float t_spawnEnd = 59;


	public static Vector3 waitLoc = new Vector3(0, -10, 0);
	public float delayBetweenSpawns = 0.2f;

	private float t_roundStart = 0f;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void levelUp(int n)
	{
		level = n;
		foreach (GameObject monster in monsterList)
		{
			Destroy(monster);
		}
		t_roundStart = Time.time;
		setMonster();
	}

	public void setMonster()
	{
		spawnList.Clear();
		if (level!=10&&level!=20) 
		{
			world = UnityEngine.Random.Range(0, 4);
			switch (world)
			{
				case 0:
					prefab = w1;
					break;
				case 1:
					prefab = w2;
					break;
				case 2:
					prefab = w3;
					break;
				case 3:
					prefab = w4;
					break;
			}
			switch (UnityEngine.Random.Range(0, 10))
			{
				case 9:
					gameMode = 3;   // 메이지
					break;
				case 8:
					gameMode = 2;   // 원거리
					break;
				case 7:
					gameMode = 1;   // 근거리
					break;
				default:
					gameMode = 0;   // 일반
					break;
			}

			// 5 라운드 전 : 20 초 소환 | 10 라운드 전 : 40 초 소환
			if (level < 6) t_spawnEnd = 20; else if (level < 11) t_spawnEnd = 40; else t_spawnEnd = 59;

			if (prefab == w4 && gameMode == 3) gameMode = 0;
			else if (prefab == w3 && gameMode == 2) gameMode = 0;

			for (int i = 0; i < prefab.Length; i++)
			{
				switch (gameMode)
				{
					case 3:
						if (prefab[i].GetComponent<M_mage>() != null)
							spawnList.Add(prefab[i]);
						break;
					case 2:
						if (prefab[i].GetComponent<M_Range>() != null)
							spawnList.Add(prefab[i]);
						break;
					case 1:
						if (prefab[i].GetComponent<M_melee>() != null)
							spawnList.Add(prefab[i]);
						break;
					case 0:
						spawnList.Add(prefab[i]);
						break;
				}
			}
		} else
		{
			spawnList.Add(boss[UnityEngine.Random.Range(0, boss.Length)]);
		}
	}


	 public void roundStart()
	{
		StopCoroutine(SpawnMonsters());
		StartCoroutine(SpawnMonsters());
	}

	IEnumerator SpawnMonsters()
	{
		if (level !=10 && level!=20)
		{
			while (true)
			{
				if (t_roundStart + t_spawnEnd < Time.time) break;

				int randNum = UnityEngine.Random.Range(0, spawnList.Count);

				monsterList.Add(Instantiate(spawnList[randNum], waitLoc, Quaternion.identity));
				monsterList[monsterList.Count - 1].SetActive(true);
				monsterList[monsterList.Count - 1].GetComponent<Monster>().Relocation();

				yield return new WaitForSeconds(delayBetweenSpawns);
			}
		} else
		{
			monsterList.Add(Instantiate(spawnList[0], waitLoc, Quaternion.identity));
			monsterList[monsterList.Count - 1].SetActive(true);
			monsterList[monsterList.Count - 1].GetComponent<Monster>().Relocation();
			yield return new WaitForSeconds(1000f);
		}
	}
}
