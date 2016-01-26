using UnityEngine;
using System.Collections;

public class Castle : MonoBehaviour
{

    bool X_symmetric, Z_symmetric;
    CastleStyle style;

    private GameObject wall;
    private GameObject tower;
    private GameObject keep; //keep will be based on Tier, and will be the cleaest indicator of tier, due to different styles.

    enum LayoutType
    {        
        Circular,
        Square,
        Cross,
        Star, 
        Random

    };

    enum Tier
    {
        Outpost = 0,
        Manor = 1,
        Fortress = 2,
        Castle = 3,
        Stronghold = 4,
        Citadel = 5
    };

    void ChooseElements()
    {

    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    




}


public class CastleStyle
{

    // Use this for initialization
    public enum Style

    {
        SquareTurrets,
        SquareFancyTurrets,
        CircularTurrets,
        CircularFancyTurrets
    }

    Style style;

    private GameObject turretModel { get; set; }
    private GameObject wallModel { get; set; }



    // Prefab definitions: set these through Unity.
    public GameObject squareTurret;
    public GameObject squareTurretWall;

    public GameObject circularTurret;
    public GameObject circularTurretWall;

    public GameObject circularFancyTurret;
    public GameObject circularFancyTurretWall;

    public GameObject squareFancyTurret;
    public GameObject squareFancyTurretWall;

    //end prefab definitions


    public void setStyle(Style t)
    {
        style = t;

        //assign prefabs based on Castle's style
        if (t == Style.CircularTurrets)
        {            
            turretModel = circularTurret;
            wallModel = squareTurretWall;
        }
        else if (t == Style.SquareTurrets);
        {
            turretModel = squareTurret;
            wallModel = circularTurretWall;

        }
        else if (t == Style.CircularFancyTurrets) 
        {
            turretModel = circularFancyTurret;
            wallModel = circularFancyTurretWall;

        }
        else if (t == Style.SquareFancyTurrets)
        {
            turretModel = squareFancyTurret;
            wallModel = squareFancyTurretWall;

        }
    }


}
