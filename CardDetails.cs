using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDetails : MonoBehaviour
{
    // 0=ruder, 1=spar, 2=hjerte, 3=kloer 
    public int color;
    public int value;   // es = 1

    public int cardID;

    public bool hoveringAnimation = true;
    bool animationUp = false;


    GameObject gameMaster;

    Material cardFaceMaterial;
    Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameObject.Find("GameMaster");


        // Find card face according to settings
        //cardFaceMaterial = Resources.Load<Material>(string.Concat("Cards/Materials/", color * 13 + value));
        cardFaceMaterial = Resources.Load("Cards/Materials/36", typeof(Material)) as Material;
        Debug.Log(string.Concat("Cards/Materials/", color * 13 + value, ".mat"));
        Debug.Log(cardFaceMaterial);

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = cardFaceMaterial;

        // Get renderer to be able to load new material
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (hoveringAnimation)
        {
            if (animationUp)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 10, Space.Self);
            } else
            {
                transform.Translate(Vector3.up * Time.deltaTime * 100 * (-1), Space.Self);
            }
            animationUp = !animationUp;
            
        }*/
    }
}
