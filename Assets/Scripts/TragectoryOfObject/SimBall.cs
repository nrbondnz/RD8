using UnityEngine;

public class SimBall : MonoBehaviour {
    [SerializeField] private Rigidbody _rb;

    public void Init(Vector3 velocity) {
        _rb.AddForce(velocity, ForceMode.Impulse);
    }

   
}