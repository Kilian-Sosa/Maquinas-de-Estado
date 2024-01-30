using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerFOVDetection : MonoBehaviour {
    [SerializeField] Vector3 min, max;
    Vector3 destination;
    [SerializeField] float playerDistanceDetection;
    Transform player;
    float visionAngle;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        RandomDestination();
        StartCoroutine(Patrol());
        StartCoroutine(Alert());
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

    IEnumerator Alert() {
        while (true) {
            if (Vector3.Distance(transform.position, player.position) < playerDistanceDetection) {
                Vector3 vectorPlayer = player.position - transform.position;
                if (Vector3.Angle(vectorPlayer.normalized, transform.forward) < visionAngle) {
                    StopCoroutine(Patrol());
                    GetComponent<NavMeshAgent>().SetDestination(player.position);
                } else StartCoroutine(Alert());
            } else StartCoroutine(Patrol());
            yield return new WaitForEndOfFrame();
        }
    }
}