using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] float speed, rotationSpeed;

    void Update() {
        GetComponent<Rigidbody>().velocity = (transform.forward * Input.GetAxis("Vertical") * speed) +
                                             (transform.right * Input.GetAxis("Horizontal") * speed);
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotationSpeed);
        GetComponent<Animator>().SetFloat("velocity", GetComponent<Rigidbody>().velocity.magnitude);
    }
}
