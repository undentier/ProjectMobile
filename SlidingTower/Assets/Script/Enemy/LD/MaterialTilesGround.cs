using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTilesGround : MonoBehaviour
{
    public Material[] randomMaterials; //on fait un tableau avec tous les matériaux
    public GameObject[] tilesGround;  //on fait un tableau avec tous les nodes que l'on veut texturer aléatoirement

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject tile in tilesGround)  // pour chaque node du sol...
        {
            tile.GetComponent<Renderer>().material = randomMaterials[Random.Range(0, randomMaterials.Length)]; //on met un material choisi avec une variable comprise entre 0 et le nb de cases de randomMaterials
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
