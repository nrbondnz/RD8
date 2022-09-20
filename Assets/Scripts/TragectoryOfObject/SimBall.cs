using UnityEngine;

public class SimBall : MonoBehaviour {
    
    public void Init(Vector3 velocity) {
        gameObject.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);
    }

   
}