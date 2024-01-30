using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    [SerializeField] Vector3 min, max;
    Vector3 destination;

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
        while (true) {
            if (Vector3.Distance(transform.position, destination) < 1.5f) {
                GetComponent<Animator>().SetFloat("velocity", 0);
                yield return new WaitForSeconds(Random.Range(1f, 3f));
                RandomDestination();
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
A