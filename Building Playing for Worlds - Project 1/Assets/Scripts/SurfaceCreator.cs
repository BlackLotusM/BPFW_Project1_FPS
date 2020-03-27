using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SurfaceCreator : MonoBehaviour {

    public float frequency = 1f;

    [Range(1, 8)]
    public int octaves = 1;

    [Range(1f, 4f)]
    public float lacunarity = 2f;

    [Range(0f, 1f)]
    public float persistence = 0.5f;

    [Range(-3f, 0f)]
    public float height = 0.5f;

    [Range(1, 3)]
    public int dimensions = 3;

    public NoiseMethodType type;

    public Gradient coloring;

    MeshCollider meshc;
    private Mesh mesh;

	private void OnEnable () {
        meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;

        if (mesh == null) {
			mesh = new Mesh();
			mesh.name = "Surface Mesh";
			GetComponent<MeshFilter>().mesh = mesh;
		}
		Refresh();
        mesh.RecalculateBounds();
    }

    [Range(1, 200)]
    public int resolution = 10;

    private int currentResolution;

    public Vector3 offset;

    public Vector3 rotation;

    public void Refresh()
    {
        meshc.sharedMesh = mesh;
        if (resolution != currentResolution)
        {
            offset = new Vector3(Random.Range(-999999, 999999), 0, Random.Range(-999999, 999999));
            CreateGrid();
        }
        Quaternion q = Quaternion.Euler(rotation);

        
        Vector3 point00 = q * new Vector3(-0.5f, -0.5f) + offset;
        Vector3 point10 = q * new Vector3(0.5f, -0.5f) + offset;
        Vector3 point01 = q * new Vector3(-0.5f, 0.5f) + offset;
        Vector3 point11 = q * new Vector3(0.5f, 0.5f) + offset;

        NoiseMethod method = Noise.methods[(int)type][dimensions - 1];
        float stepSize = 1f / resolution;
        for (int v = 0, y = 0; y <= resolution; y++)
        {
            Vector3 point0 = Vector3.Lerp(point00, point01, y * stepSize);
            Vector3 point1 = Vector3.Lerp(point10, point11, y * stepSize);
            for (int x = 0; x <= resolution; x++, v++)
            {
                Vector3 point = Vector3.Lerp(point0, point1, x * stepSize);
                float sample = Noise.Sum(method, point, frequency, octaves, lacunarity, persistence);
                sample = type == NoiseMethodType.Value ? (sample - 0.5f) : (sample * 0.5f);

                if (type != NoiseMethodType.Value)
                {
                    sample = sample * 0.5f + 0.5f;
                }

                if (sample > 0.28f)
                {
                    sample = 0.4f;
                }
                else
                if (sample > 0.263f)
                {
                    sample = 0.33f;
                }
                else
                if (sample > 0.23f)
                {
                    sample = 0.2f;
                }
                else if (sample > 0.2f)
                {
                    sample = 0.1f;
                }
                else if (sample > 0.1f)
                {
                    sample = 0.0f;
                }
                else if (sample > 0f)
                {
                    sample = -0.02f;
                }
                else if (sample > -0.1f)
                {
                    sample = -0.05f;
                }
                else if (sample > -0.15f)
                {
                    sample = -0.07f;
                }
                else
                {
                    sample = -0.2f;
                }

                vertices[v].y = sample;
                colors[v] = coloring.Evaluate(sample + 0.5f);
            }
        }
        mesh.vertices = vertices;
        mesh.colors = colors;

        meshc.sharedMesh = mesh;

    }

    private Vector3[] vertices;
    private Vector3[] normals;
    private Color[] colors;

    private void CreateGrid()
    {
        currentResolution = resolution;//Makes new baseres
        mesh.Clear();

        vertices = new Vector3[(resolution + 1) * (resolution + 1)];
        colors = new Color[vertices.Length];
        normals = new Vector3[vertices.Length];
        Vector2[] uv = new Vector2[vertices.Length];

        float stepSize = 1f / resolution;
        for (int v = 0, z = 0; z <= resolution; z++)
        {
            for (int x = 0; x <= resolution; x++, v++)
            {
                vertices[v] = new Vector3(x * stepSize - 0.5f, 0f, z * stepSize - 0.5f);
                normals[v] = Vector3.up;
                uv[v] = new Vector2(x * stepSize, z * stepSize);
            }
        }

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uv;

        int[] triangles = new int[resolution * resolution * 6];
        for (int t = 0, v = 0, y = 0; y < resolution; y++, v++)
        {
            for (int x = 0; x < resolution; x++, v++, t += 6)
            {
                triangles[t] = v;
                triangles[t + 1] = v + resolution + 1;
                triangles[t + 2] = v + 1;
                triangles[t + 3] = v + 1;
                triangles[t + 4] = v + resolution + 1;
                triangles[t + 5] = v + resolution + 2;
            }
        }
        mesh.triangles = triangles;
        meshc.sharedMesh = mesh;
        mesh.RecalculateBounds();
    }
}