using UnityEngine;
using Pathfinding.Serialization.JsonFx;
using System.Collections;

public class Sketch : MonoBehaviour {

    public GameObject myPrefab;
	string _WebsiteURL = "http://anthonyliu.azurewebsites.net/tables/product?zumo-api-version=2.0.0";

    void Start () {
        string jsonResponse = Request.GET(_WebsiteURL);

        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }

        Product[] products = JsonReader.Deserialize<Product[]>(jsonResponse);

        int totalCubes = products.Length;
		float totalDistance = 2.0f;
        int i = 0;
        foreach (Product product in products)
        {
            float perc = i / (float)totalCubes;
            i++;
			float sin = Mathf.Sin (perc * Mathf.PI / 2);
			float x = 1.5f + sin*totalDistance;
            float y = 5.5f;
            float z = 0.0f;
            GameObject newCube = (GameObject)Instantiate(myPrefab, new Vector3( x, y, z), Quaternion.identity);
			newCube.GetComponent<MyCubeScript>().setSize((0.6f - perc) *2);
			newCube.GetComponent<MyCubeScript>().rotateSpeed = perc;
            newCube.GetComponentInChildren<TextMesh>().text = product.ProductName;
        }        
	}

	void Update () {
	
	}
}
