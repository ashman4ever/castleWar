using UnityEngine;
using System.Collections;

public class GeneratedCastle : MonoBehaviour {

    private static int POINT_VALUE_LARGE_TOWER_01 = 1250;
    private static int POINT_VALUE_SMALL_TOWER_01 = 550;
    private static int POINT_VALUE_LARGE_TOWER_02 = 200;

    private static float LARGE_TOWER_01_POINTS_PERCENTAGE = .32f;
    private static float SMALL_TOWER_01_POINTS_PERCENTAGE = .49f;
    
    
    private static float LARGE_TOWER_MIN_SPACING = 15f;
    private static float SMALL_TOWER_MIN_SPACING = 10f;


    public Vector3 SpawnCenter;
    public Rigidbody SpawnLocation;
    public int Tier;

    public GameObject LargeTower01, SmallTower01, SmallTower02, LargeWall01, SmallWall01;

    

    // Use this for initialization
    void Start()
    {
        ArrayList LargeTower01List = new ArrayList();
        ArrayList SmallTower01List = new ArrayList();
        ArrayList SmallTower02List = new ArrayList();

        SpawnCenter = SpawnLocation.transform.position;



        //Determine Tower Mix
        int points = Tier * 1000;

        //Spend points on Towers
        int numLargeTower01 = ((int)(points * LARGE_TOWER_01_POINTS_PERCENTAGE)) / POINT_VALUE_LARGE_TOWER_01;
        int numSmallTower01 = ((int)(points * SMALL_TOWER_01_POINTS_PERCENTAGE)) / POINT_VALUE_SMALL_TOWER_01;
        points = points - (numLargeTower01 * POINT_VALUE_LARGE_TOWER_01 + numSmallTower01 * POINT_VALUE_SMALL_TOWER_01);
        int numSmallTower02 = points / POINT_VALUE_LARGE_TOWER_02;






        //distribute Large Towers in a ring;
        if (numLargeTower01 > 0)
        {
            float circumference = LARGE_TOWER_MIN_SPACING * numLargeTower01;
            float radius = circumference / (2 * Mathf.PI);

            if (numLargeTower01 == 1)
            {
                radius = 0;
            }

            float rotationDegreesPerObject = 360.0f / (float)numLargeTower01;
            
            


            for (int i = 0; i < numLargeTower01; i++)
            {
                Vector3 basePosition = new Vector3(SpawnCenter.x + radius, 0, SpawnCenter.z);

                GameObject clone = Instantiate(LargeTower01, basePosition, Quaternion.Euler(-0, 0, rotationDegreesPerObject * i)) as GameObject;
                clone.transform.RotateAround(SpawnCenter, Vector3.up, rotationDegreesPerObject * i);
                LargeTower01List.Add(clone);
            }

        }

        if (numSmallTower01 > 0)
        {
            float circumference = SMALL_TOWER_MIN_SPACING * numSmallTower01;
            float radius = circumference / (2 * Mathf.PI);

            if (numSmallTower01 == 1)
            {
                radius = 0;
            }

            float rotationDegreesPerObject = 360.0f / (float)numSmallTower01;
       
            


            for (int i = 0; i < numSmallTower01; i++)
            {
                Vector3 basePosition = new Vector3(SpawnCenter.x + radius, 0, SpawnCenter.z);

                GameObject clone = Instantiate(SmallTower01, basePosition, Quaternion.Euler(-0, 0, rotationDegreesPerObject * i)) as GameObject;
                clone.transform.RotateAround(SpawnCenter, Vector3.up, rotationDegreesPerObject * i);
                SmallTower01List.Add(clone);
            }

        }
        if (numSmallTower02 > 0)
        {
            float circumference = SMALL_TOWER_MIN_SPACING * numSmallTower02;
            float radius = circumference / (2 * Mathf.PI);

            float rotationDegreesPerObject = 360.0f / (float)numSmallTower02;

            
            for (int i = 0; i < numSmallTower02; i++)
            {
                Vector3 basePosition = new Vector3(SpawnCenter.x + radius, 0, SpawnCenter.z);

                GameObject clone = Instantiate(SmallTower02, basePosition, Quaternion.Euler(-0, 0, rotationDegreesPerObject * i)) as GameObject;
                clone.transform.RotateAround(SpawnCenter, Vector3.up, rotationDegreesPerObject * i);
                SmallTower02List.Add(clone);
            }

        }

        //Calculate and place Walls
        //Big Walls first
        if (numLargeTower01 > 1)
        {            
            //connect towers with walls;
            for (int i = 0; i < LargeTower01List.Count; i++)
            {
                GameObject tower1;
                if (i == 0)
                {
                    tower1 = (GameObject)LargeTower01List[LargeTower01List.Count-1];
                }

                else tower1 = (GameObject)LargeTower01List[i - 1];
                GameObject tower2 = (GameObject)LargeTower01List[i];

                GameObject clone = Instantiate(LargeWall01, SpawnCenter, Quaternion.Euler(-0, 0, 0)) as GameObject;

                //stretch wall
                float desiredLength = calculateWallLength(tower1, tower2);
                float scaleFactorX = WallStretchFactor(clone, desiredLength);

                clone.transform.localScale += new Vector3(scaleFactorX, 0f, 0f);

                //rotate into position
                float angle = getWallRotationAngle(tower1, tower2);
                clone.transform.Rotate(new Vector3(0f, 0f, 1), -1 * angle);

                //set Wall in place
                Vector3 wallPosition = getWallPosition(tower1, tower2);
                clone.transform.position = wallPosition;
            }



        }

        //Middle Ring

        if (numSmallTower01 > 2)
        {
            //connect towers with walls;
            for (int i = 0; i < SmallTower01List.Count; i++)
            {
                GameObject tower1;
                if (i == 0)
                {
                    tower1 = (GameObject)SmallTower01List[SmallTower01List.Count - 1];
                }

                else tower1 = (GameObject)SmallTower01List[i - 1];
                GameObject tower2 = (GameObject)SmallTower01List[i];

                GameObject clone = Instantiate(LargeWall01, SpawnCenter, Quaternion.Euler(-0, 0, 0)) as GameObject;

                //stretch wall
                float desiredLength = calculateWallLength(tower1, tower2);
                float scaleFactorX = WallStretchFactor(clone, desiredLength);

                clone.transform.localScale += new Vector3(scaleFactorX, 0f, 0f);

                //rotate into position
                float angle = getWallRotationAngle(tower1, tower2);
                clone.transform.Rotate(new Vector3(0f, 0f, 1), -1 * angle);

                //set Wall in place
                Vector3 wallPosition = getWallPosition(tower1, tower2);
                clone.transform.position = wallPosition;
            }
        }

        //Small Walls
        if (numSmallTower02 > 2)
        {
            //connect towers with walls;
            for (int i = 0; i < SmallTower02List.Count; i++)
            {
                GameObject tower1;
                if (i == 0)
                {
                    tower1 = (GameObject)SmallTower02List[SmallTower02List.Count - 1];
                }

                else tower1 = (GameObject)SmallTower02List[i - 1];
                GameObject tower2 = (GameObject)SmallTower02List[i];

                GameObject clone = Instantiate(SmallWall01, SpawnCenter, Quaternion.Euler(-0, 0, 0)) as GameObject;

                //stretch wall
                float desiredLength = calculateWallLength(tower1, tower2);
                float scaleFactorX = WallStretchFactor(clone, desiredLength);

                clone.transform.localScale += new Vector3(scaleFactorX, 0f, 0f);

                //rotate into position
                float angle = getWallRotationAngle(tower1, tower2);
                clone.transform.Rotate(new Vector3(0f, 0f, 1), -1 * angle);

                //set Wall in place
                Vector3 wallPosition = getWallPosition(tower1, tower2);
                clone.transform.position = wallPosition;
            }

        }
        





    }
	
	// Update is called once per frame
	void Update () {
	
	}

    float calculateWallLength(GameObject tower1, GameObject tower2)
    {
        float deltaX = tower1.transform.position.x - tower2.transform.position.x;
        float deltaZ = tower1.transform.position.z - tower2.transform.position.z;
        // using Pythagorean theorum: d = sqrt(a^2+b^2)
        return Mathf.Sqrt(deltaX * deltaX + deltaZ * deltaZ);
        
    }
    float WallStretchFactor(GameObject wall, float desiredWallLength)
    {
        float initialWallLength = wall.GetComponent<Renderer>().bounds.size.x;
        float initialTransformX = wall.transform.localScale.x;
        float finalTransformX = initialTransformX * (desiredWallLength / initialWallLength);
        return finalTransformX;
        
    }
    float getWallRotationAngle(GameObject tower1, GameObject tower2)
    {
        float deltaX = tower1.transform.position.x - tower2.transform.position.x;
        float deltaZ = tower1.transform.position.z - tower2.transform.position.z;

        float angle = Mathf.Atan(deltaZ / deltaX) * (180 / Mathf.PI);

        return angle;
    }
    Vector3 getWallPosition(GameObject tower1, GameObject tower2)
    {
        float midpointX = (tower1.transform.position.x + tower2.transform.position.x) / 2;
        float midpointZ = (tower1.transform.position.z + tower2.transform.position.z) / 2;

        return new Vector3(midpointX, 0f, midpointZ);
    }
}
