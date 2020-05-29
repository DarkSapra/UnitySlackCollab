namespace USC.CharacterController
{
    using UnityEngine;
    using Mirror;
public class Ride : MonoBehaviour
{
    public shipController ship;
    public LayerMask handControlLayer;
    private InputBridge input;
    public bool riding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            if(ship && input.Interact())
            {
                riding = true;
            }
            if(!ship)
                riding = false;
        
    }  

    void ControlShip(Vector3 input)
    {

    }
     private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == handControlLayer) 
        {       
            ship = other.GetComponent<shipController>();
            
        }
    }
    private void OnTriggerExit(Collider other)
    {     
        if (other.gameObject.layer == handControlLayer)
        {       
            ship = null;

        }    
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == handControlLayer)        
        {       
                ship = other.GetComponent<shipController>();
        }
    } 
}
}
