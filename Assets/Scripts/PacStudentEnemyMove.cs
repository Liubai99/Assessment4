using System.Collections.Generic;
using UnityEngine;

public class PacStudentEnemyMove : MonoBehaviour
{
    public GameObject[] GhostwayPointsGos;
    public float Ghostspeed = 0.2f;
    private List<Vector3> wayPoints = new List<Vector3>();
    private int index = 0;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position + new Vector3(0, 3, 0);
        LoadPacStudentAPath(GhostwayPointsGos[GhostManager.Instance.usingIndex[GetComponent<SpriteRenderer>().sortingOrder - 2]]);
    }

    private void Update()
    {
        if (transform.position != wayPoints[index])
        {
            Vector2 temp = Vector2.MoveTowards(transform.position, wayPoints[index], Ghostspeed);
            GetComponent<Rigidbody2D>().MovePosition(temp);
        }
        else
        {
            index++;
            if (index >= wayPoints.Count)
            {
                index = 0;
                LoadPacStudentAPath(GhostwayPointsGos[Random.Range(0, GhostwayPointsGos.Length)]);
            }
        }

        Vector2 dir = wayPoints[index] - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    private void LoadPacStudentAPath(GameObject PacStudentMove)
    {
        wayPoints.Clear();
        foreach (Transform t in PacStudentMove.transform)
        {
            wayPoints.Add(t.position);
        }
        wayPoints.Insert(0, startPos);
        wayPoints.Add(startPos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "PacStudent")
        {
            if (GhostManager.Instance.isSuperPacman)
            {
                transform.position = startPos - new Vector3(0, 3, 0);
                index = 0;
                GhostManager.Instance.score += 10;
            }
            else
            {
                PacmanManager.Instance.UpdateLife();
            }
        }
    }

}
