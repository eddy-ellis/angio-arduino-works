using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {

	//a variable to hold the prefab we want to spawn
	public GameObject cube;

	//we want some variables to decide how any cubes to spawn 
	//and how high above us we want them to spawn
	public int numberToSpwan;
	public float lowestSpawnheight;
	public float highestSpawnheight;

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < numberToSpwan; i++)
		{
			Instantiate(cube, new Vector3(Random.Range(-9, 9), Random.Range(lowestSpawnheight, highestSpawnheight), 0), Quaternion.identity);

		}
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
