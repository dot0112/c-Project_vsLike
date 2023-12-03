using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    static int level=0;
    static List<GameObject> monsterList = new List<GameObject>();
    public GameObject[] monsterPrefab;

    public static Vector3 waitLoc = new Vector3(0, -10, 0);
    public float delayBetweenSpawns = 0.5f;

	/*
     * {1,2} == monsterPrefab[0] �� 1�� ��ȯ, [1] �� 2�� ��ȯ
     */
	private int[,] level_summon =
    {
        {1,1,1,1,1,1 },
        {2,2,2,2,2,2 },
        {3,3,3,3,3,3 }
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void levelUp()
	{
		level++;
		StartCoroutine(SpawnMonsters());
	}

	IEnumerator SpawnMonsters()
	{
		for (int i = 0; i < monsterPrefab.Length; i++)
		{
			for (int j = 0; j < level_summon[level, i]; j++)
			{
				monsterList.Add(Instantiate(monsterPrefab[j], waitLoc, Quaternion.identity));
				yield return new WaitForSeconds(delayBetweenSpawns);
			}
		}
	}
}
