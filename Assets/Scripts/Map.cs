using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public GameObject hexPrefab;
    // высота равна (стороне * корень из трех)/2
    public float height1 = (1.0f * Mathf.Sqrt(3)/2);
    float cof = 1.1f;
    float vectorLength;

    public List<List<GameObject>> circlesOfHexes = new List<List<GameObject>>(); // хексы по крушам - каждый список - это круг
    public List<Vector3> allHexesPositions = new List<Vector3>(); // координаты центров всех хексов

    // Use this for initialization
    void Start () {
        vectorLength = cof * (1.0f * Mathf.Sqrt(3) / 2);

        var startHex = Instantiate(hexPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        List<GameObject> startList = new List<GameObject>();
        List<Vector3> startVector = new List<Vector3>();

        startList.Add(startHex);
        startVector.Add(startHex.transform.position);
        circlesOfHexes.Add(startList);
        allHexesPositions.Add(startHex.transform.position);

        SetUpMap(4, startList);		
	}
	
	void SetUpMap(int circlesNumber, List<GameObject> curcle) {
        if(circlesOfHexes.Count >= circlesNumber){ return; }

        List<GameObject> newCercle = new List<GameObject>();
        foreach (GameObject hex in curcle) {
            float x = hex.transform.position.x;
            float z = hex.transform.position.z;

            for(float alpha = 0; alpha < 360.0f; alpha += 60.0f) {
                float alphaRad = alpha * Mathf.Deg2Rad;
                Vector3 newVector = GetVectorForNewhex(x, z, alphaRad);
                if (DoesNotThisHexExistApprox(newVector)) {
                    var a = Instantiate(hexPrefab, newVector, Quaternion.identity);
                    newCercle.Add(a);
                    allHexesPositions.Add(a.transform.position);
                }
                else{
                    Debug.Log(newVector + "is already exist");
                }
            }
        }
        circlesOfHexes.Add(newCercle);
        SetUpMap(circlesNumber, newCercle);
    }

    Vector3 GetVectorForNewhex(float x1, float z1, float alphaRad )
    {
        return new Vector3((vectorLength * Mathf.Cos(alphaRad)) + x1 , 0, (vectorLength * Mathf.Sin(alphaRad)) + z1 );
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
}
