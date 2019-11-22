using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDetails : MonoBehaviour
{
    // 0=ruder, 1=spar, 2=hjerte, 3=kloer 
    public int color;
    public int value;   // es = 1

    public int cardID;


    GameObject gameMaster;

    Material cardFaceMaterial;
    Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameObject.Find("GameMaster");


        // Find card face according to settings
        cardFaceMaterial = Resources.Load<Material>(string.Concat("CardFaces/Materials/", color * 13 + value));
        Debug.Log(string.Concat("CardFaces/Materials/", color * 13 + value, ".mat"));

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = cardFaceMaterial;

        // Get renderer to be able to load new material
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
