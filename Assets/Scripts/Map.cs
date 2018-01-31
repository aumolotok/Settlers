using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour {

    public HexObj hexPrefab;
    public TownPoint townPoint;
    // высота равна (стороне * корень из трех)/2
    static float radius = 1.0f;
    public float height1 = (radius * Mathf.Sqrt(3)/2);
    float cof = 2.0f;
    float vectorLengthForNewHex;

    public List<List<HexObj>> circlesOfHexes = new List<List<HexObj>>(); // хексы по крушам - каждый список - это круг
    public List<Vector3> allHexesPositions = new List<Vector3>(); // координаты центров всех хексов
    public List<TownPoint> allTownsPositions = new List<TownPoint>();

    // Use this for initialization
    void Start () {
        vectorLengthForNewHex = cof * (1.0f * Mathf.Sqrt(3) / 2);

        var startHex = Instantiate(hexPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        List<HexObj> startList = new List<HexObj> { startHex };
        List<Vector3> startVector = new List<Vector3> { startHex.transform.position };

        circlesOfHexes.Add(startList);
        allHexesPositions.Add(startHex.transform.position);
        SetUpHexMap(4, startList);
        		
	}
	
	void SetUpHexMap(int circlesNumber, List<HexObj> curcle) {
        if(circlesOfHexes.Count >= circlesNumber){ return; }

        List<HexObj> newCercle = new List<HexObj>();
        foreach (HexObj hex in curcle) {
            float x = hex.transform.position.x;
            float z = hex.transform.position.z;

            for(float alpha = 0; alpha < 360.0f; alpha += 60.0f) {
                float alphaRad = alpha * Mathf.Deg2Rad;
                Vector3 newVector = GetVectorForNewhex(x, z, alphaRad);
                if (DoesNotThisHexExistApprox(newVector)) {
                    var a = Instantiate<HexObj>(hexPrefab, newVector, Quaternion.identity);
                    newCercle.Add(a);
                    allHexesPositions.Add(a.transform.position);
                }
                else{
                    Debug.Log(newVector + "is already exist");
                }
            }
        }
        circlesOfHexes.Add(newCercle);
        SetUpHexMap(circlesNumber, newCercle);
    }

    void SetUpTownPointsMap()
    {
        foreach(List<HexObj> layer in circlesOfHexes)
        {
            foreach(HexObj hex in layer)
            {

            }
        }
    }

    Vector3 GetVectorForNewhex(float x1, float z1, float alphaRad )
    {
        return GetVector(x1, z1, alphaRad, vectorLengthForNewHex);
    }

    Vector3 GetVectorForTownPoint(float x1, float z1, float alphaRad)
    {
        return GetVector(x1, z1, alphaRad, radius);
    }

    Vector3 GetVector(float x1, float z1, float alphaRad, float length)
    {
        return new Vector3((length * Mathf.Cos(alphaRad)) + x1, 0, (length * Mathf.Sin(alphaRad)) + z1);
    }

    bool DoesNotThisHexExistApprox(Vector3 newVector)
    {
        bool result = true;
        foreach(var vectorFromAll in allHexesPositions)
        {
            float deltaX = vectorFromAll.x - newVector.x;
            float deltaZ = vectorFromAll.z - newVector.z;
            if ( deltaX > 0.01 || deltaX <-0.01 || deltaZ > 0.01 || deltaZ < -0.01)
            {
                result &= true;
            }
            else
            {
                result &= false;
            }
        }
        return result;
    }

    bool IsPointAlreadyAdded(Vector3 newVector)
    {
        var a = from b in allTownsPositions
                where (transform.position.x - newVector.x < 0.01f || transform.position.x - newVector.x > -0.01f)
                        ||
                      (transform.position.z - newVector.z < 0.01f || transform.position.z - newVector.z > -0.01f)
                select b;
        return true;
    }

    void BuildPoints(HexObj hex)
    {
        float hexX = hex.transform.position.x;
        float hexZ = hex.transform.position.y;
    }

    
}
