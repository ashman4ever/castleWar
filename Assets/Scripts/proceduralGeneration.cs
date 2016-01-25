using UnityEngine;
using System.Collections;


public class ProceduralGeneration : MonoBehaviour {

	public Transform villageCenter;
	public float radius;
	public int numberOfVillages;


	void Start() {

		Vector3 startingPos = new Vector3(0f,13.3f,0f);

		populateArea(numberOfVillages, radius, transform.position, villageCenter, numberOfVillages);   
	}


	public static void populateArea(int instances, float radius, Vector3 center, Object prefab, int amount)
	{
		for(int i = 0; i < instances; i++) 
		{                   
			var angle = i * Mathf.PI * 2 / instances;

			Vector3 pos = getPosition(center, radius, angle);

			Object[] arrayOfInstances = new Object[amount];

			arrayOfInstances[i] = Instantiate(prefab, pos, Quaternion.Euler(0, Random.Range(0,360), 0)); 

			arrayOfInstances[i].name = "village" + i;         
		}       

		for(int i = 0, len = amount; i < len; i++)
		{
			int villageSize = Random.Range(3,30);

			for(int y = 0; y < villageSize; y++) 
			{                   
				var angle = y * Mathf.PI * 2 / villageSize;

				Vector3 pos = getPosition(GameObject.Find("village"+ i).transform.position, 2.0f, angle);

				Object[] arrayOfInstances = new Object[villageSize];

				arrayOfInstances[y] = Instantiate(GameObject.Find("House04"), pos, Quaternion.Euler(0, Random.Range(0,360), 0)); 

				arrayOfInstances[y].name = "house" + y;       
			}       

		}
	}


	public static Vector3 getPosition(Vector3 center, float radius, float angle) 
	{

		var count = 0;
		Vector3 pos = new Vector3(Mathf.Cos(angle) * Random.Range(0.5f,3.0f) * radius + center.x , 0, Mathf.Sin(angle) * Random.Range(0.5f,3.0f) * radius + center.z);

		while(((Mathf.Abs(center.x - pos.x) < 2) || (Mathf.Abs(center.y - pos.y) < 2)) && count < 200) 
		{
			pos = new Vector3(Mathf.Cos(angle) * Random.Range(0.5f,3.0f) * radius + center.x , 0, Mathf.Sin(angle) * Random.Range(0.5f,3.0f) * radius + center.z);
			count ++;
		}

		return pos;
	}
}