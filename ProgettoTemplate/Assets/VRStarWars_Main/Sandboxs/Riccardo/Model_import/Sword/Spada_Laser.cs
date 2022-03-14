using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spada_Laser : MonoBehaviour
{
   
    //laser della spada
    [SerializeField] GameObject laser;

    //dimensione della spada con laser attivo
    private Vector3 fullSize;

    public bool isActive; //false

    // Start is called before the first frame update
    void Start()
    {

        //trasformiamo la scala locale per salvare qualunque sia la dimensione intera
        fullSize = laser.transform.localScale;

        //al di là della scala creiamo un nuovo vettore che mette a  0 la y
        laser.transform.localScale = new Vector3(fullSize.x, 0, fullSize.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Accendo il laser se il transform del local scale di y è minore del fullsize di y
        if(isActive && laser.transform.localScale.y < fullSize.y)
        {
            laser.transform.localScale += new Vector3(0, 0.0001f, 0);
            isActive = true;
        }
        else
        {
            if(!isActive && laser.transform.localScale.y >= 0)
            {
                laser.transform.localScale += new Vector3(0, -0.0001f, 0);
                isActive = false;
            }
        }
        
    }
}
