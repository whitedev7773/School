using System;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Wave : MonoBehaviour
{
    private Vector3 startPos;
    public Manager manager;
    
    int pos_x;
    int pos_y;

    bool is_left_triangle = false;
    bool is_right_triangle = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
    }

    public void SetState(string name)
    {
        this.name = name;
        pos_x = int.Parse(name[1].ToString());
        pos_y = int.Parse(name[0].ToString());

        if (pos_x >= 0 && pos_y < manager.y_length - 1 && pos_x < manager.x_length - 1)
        {
            is_left_triangle = true;
            GameObject left_mesh = Instantiate(GameObject.Find("LMesh"));
            manager.LeftMesh.Add(left_mesh);
            left_mesh.name = $"{pos_y}{pos_x} LMesh";
        }

        if (pos_y > 0 && pos_x > 0)
        {
            is_right_triangle = true;
            GameObject right_mesh = Instantiate(GameObject.Find("RMesh"));
            manager.RightMesh.Add(right_mesh);
            right_mesh.name = $"{pos_y}{pos_x} RMesh";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.hide_point)
        {
            this.transform.localScale = Vector3.zero;
        }
        else
        {
            this.transform.localScale = manager.point_size;
        }

        var pos = gameObject.transform.position;
        float theta = startPos.x + Time.time;
        float sin = manager.height * Mathf.Sin(theta + startPos.z);
        float cos = manager.lambda * Mathf.Cos(theta);
        pos.y = startPos.y + sin;
        pos.x = startPos.x + cos;

        gameObject.transform.position = pos;


        if (this.name == "Target" || manager.y_length < 2 || manager.x_length < 2) { return; }
        MeshUpdate();
    }

    void MeshUpdate()
    {
        if (pos_x >= 0 && pos_y < manager.y_length-1 && pos_x < manager.x_length - 1)
        {
            GameObject left_mesh = GameObject.Find($"{pos_y}{pos_x} LMesh");
            if (manager.draw_triangle_left_down)
            {
                left_mesh.transform.localScale = Vector3.one;
            }
            else
            {
                left_mesh.transform.localScale = Vector3.zero;
            }

            Mesh mesh = new Mesh();

            Vector3[] left_vertices;
            int[] left_indexes;

            // Debug.Log($"LEFT x: {pos_x} y: {pos_y} / x: {pos_x+1} y: {pos_y+1}");
            Transform target_1 = manager.dots[pos_y+1][pos_x].transform;
            Transform target_2 = manager.dots[pos_y][pos_x+1].transform;

            left_vertices = new Vector3[]
            {
                target_1.position,
                target_2.position,
                this.transform.position
            };

            left_indexes = new int[] { 0, 1, 2 };

            mesh.vertices = left_vertices;
            mesh.triangles = left_indexes;
            left_mesh.GetComponent<MeshFilter>().mesh = mesh;
        }
        if (pos_y > 0 && pos_x > 0)
        {
            GameObject right_mesh = GameObject.Find($"{pos_y}{pos_x} RMesh");

            if (manager.draw_triangle_left_down)
            {
                right_mesh.transform.localScale = Vector3.one;
            }
            else
            {
                right_mesh.transform.localScale = Vector3.zero;
            }

            Mesh mesh = new Mesh();

            Vector3[] right_vertices;
            int[] right_indexes;

            // Debug.Log($"RIGHT x: {pos_x} y: {pos_y} / x: {pos_x - 1} y: {pos_y - 1}");
            Transform target_1 = manager.dots[pos_y - 1][pos_x].transform;
            Transform target_2 = manager.dots[pos_y][pos_x - 1].transform;

            right_vertices = new Vector3[]
            {
                target_2.position,
                this.transform.position,
                target_1.position
            };

            right_indexes = new int[] { 0, 1, 2 };

            mesh.vertices = right_vertices;
            mesh.triangles = right_indexes;
            right_mesh.GetComponent<MeshFilter>().mesh = mesh;
        }
    }
}