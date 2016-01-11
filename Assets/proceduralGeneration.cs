using UnityEngine;
using System.Collections;


public class proceduralGeneration : MonoBehaviour {
    
    public Transform Townhall;
    public Transform Castle;
    //int spawnHouse = 4; 
    //int spawnHotel = 10; 
    //int spawnLodge = 10; 
    //int structureCount = 0; 
    public float radius = 5f;
    public int numberOfVillages;
    public Object[] villages;
    
    // void Update () { 
    //     spawnHouse = Random.Range(0,11); 
    //     //spawnHotel = Random.Range(0,11); 
    //     //spawnLodge = Random.Range(0,11); 
    //     if(structureCount >=10){ 
    //         if(spawnHouse == 0){ 
    //             Instantiate(House, transform.position, transform.rotation); 
    //             structureCount = structureCount + 1; 
    //         }
    //         // } if(spawnHotel == 0){ 
    //         //     Instantiate(Hotel, transform.position, transform.rotation); 
    //         //     structureCount = structureCount + 1; 
    //         // } if(spawnLodge == 0){ 
    //         //     Instantiate(Lodge, transform.position, transform.rotation); 
    //         //     structureCount = structureCount + 1; 
    //         // }
    //     }
    // }
    
    void Start() {
        
        villages = new Object[numberOfVillages];
        
		Vector3 startingPos = new Vector3(0f,13.3f,0f);

		Instantiate(Castle, startingPos, Quaternion.Euler(0, 0, 0));
        
        villages = populateArea(numberOfVillages, radius, "City", Townhall, villages);   
    }
    
    
    public static Object[] populateArea(int instances, float radius, string prefabName, Object prefab, Object[] arrayOfInstances)
    {
        for(int i = 0; i < instances; i++) 
        {                   
            var angle = i * Mathf.PI * 2 / instances;
            
            Vector3 pos = getPosition(GameObject.Find(prefabName).transform.position, radius, angle);
            
            arrayOfInstances[i] = Instantiate(prefab, pos, Quaternion.Euler(0, Random.Range(0,360), 0)); 
            
            arrayOfInstances[i].name = "village" + i;         
        }       
        
        for(int i = 0; i < arrayOfInstances.Length; i++)
        {
            int villageSize = Random.Range(3,20);
            
            for(int y = 0; y < villageSize; y++) 
            {                   
                var angle = y * Mathf.PI * 2 / villageSize;
                
                Vector3 pos = getPosition(GameObject.Find("village"+ i).transform.position, 1.0f, angle);
                
                Object[] arrayOfHouses = new Object[villageSize];

                arrayOfHouses[y] = Instantiate(GameObject.Find("smallHousePrefab"), pos, Quaternion.Euler(270, Random.Range(0,360), 0)); 
                    
                arrayOfHouses[y].name = "house" + y;       
            }       
            
        }
        
        return arrayOfInstances;
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