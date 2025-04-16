using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;
    // [SerializeField] float waitTime = 1f;

    Enemy enemyScript;

    void OnEnable()
    {
        // START HERE -- showing program flow of coroutine (enable the debugs to test)
        // Debug.Log("Starting coroutine");

        FindPath();
        ReturnToStart();

        // This is how to call a coroutine
        StartCoroutine(FollowPath());

        // This will execute the moment that the coroutine yields control before processing again.
        // Debug.Log("Finishing start method.");
        // END HERE -- showing program flow of coroutine
    }

    void Start() {
        enemyScript = GetComponent<Enemy>();
    }

    void FindPath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach(Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if(waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    // This is a co-routine
    // Return type is something countable that the system can use.
    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;

                //LERP essentially is used to get the midpoints between two set positions, via the travelPercent in this case. Range is from 0 (startPos) to 1 (endPos).
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);

                // yield return means to give up control | new WaitForSeconds(1) means to return control in 1 second.
                // yield return new WaitForSeconds(waitTime);

                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }

    void FinishPath()
    {
        enemyScript.stealGold();
        gameObject.SetActive(false);
    }
}
