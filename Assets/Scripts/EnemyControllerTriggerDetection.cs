using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerTriggerDetection : MonoBehaviour {
    [SerializeField] Vector3 min, max;
    Vector3 destination;
    bool playerDetected = false;

    void Start() {
        
    }

    void Update() {
        
    }

    void RandomDestination() {
        destination = new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z));
        GetComponent<NavMeshAgent>().SetDestination(destination);
        GetComponent<Animator>().SetFloat("velocity", 2);
    }

    IEnumerator Patrol() {
        GetComponent<NavMeshAgent>().SetDestination(destination);
        while (true) {
            if (Vector3.Distance(transform.position, destination) < 1.5f) {
                GetComponent<Animator>().SetFloat("velocity", 0);
                yield return new WaitForSeconds(Random.Range(1f, 3f));
                RandomDestination();
            }
            yield return new WaitForEndOfFrame();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            playerDetected = true;
            StopCoroutine(Patrol());
            transform.LookAt(other.transform);
            GetComponent<NavMeshAgent>().SetDestination(other.transform.position);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            playerDetected = false;
        }
    }
}