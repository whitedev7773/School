using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject target;

    public int y_length = 50;
    public int x_length = 50;

    public float height = 1;
    public float lambda = 1;

    public float distance_x = 0.2f;
    public float distance_z = 0.2f;

    public Vector3 point_size;
    public bool hide_point = false;

    public bool draw_triangle_left_down = false;
    public bool draw_triangle_right_up = false;

    public List<List<GameObject>> dots = new List<List<GameObject>>();
    public List<GameObject> LeftMesh = new List<GameObject>();
    public List<GameObject> RightMesh = new List<GameObject>();

    void Start()
    {
        int count = 0;

        target.name = "Target";
        target.AddComponent<Wave>().manager = this;

        for (int l=0; l < y_length; l++)
        {
            dots.Add(new List<GameObject>());
            for (int i = 0; i < x_length; i++)
            {
                Vector3 pos = target.transform.position;
                pos.x += (float)(i + 1) * distance_x;
                pos.z = (float)(l + 1) * distance_z;

                GameObject go = Instantiate(target, pos, Quaternion.identity);
                go.name = count.ToString();
                go.AddComponent<Wave>().manager = this;
                go.GetComponent<Wave>().SetState($"{l}{i}");

                dots[l].Add(go);
                count++;
            }
        }

        Debug.Log($"x:{dots.Count} y:{dots[0].Count}");
    }
}
