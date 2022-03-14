using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spada_Laser : MonoBehaviour
{
    //laser della spada
    private GameObject laser;

    //dimensione della spada con laser attivo
    private Vector3 fullSize;

    // Start is called before the first frame update
    void Start()
    {
        //Ricerca del gameobject all'interno della hierarchy
        laser = transform.Find("LightSaber-Blade").gameObject;

        //trasformiamo la scala locale per salvare qualunque sia la dimensione intera
        fullSize = laser.transform.localScale;

        //al di là della scala creiamo un nuovo vettore che mette a  0 la y
        laser.transform.localScale = new Vector3(fullSize.x, 0, fullSize.z);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
