using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshData : MonoBehaviour {

    public int lengthY; //網格的長
    public int lengthX; //寬
    public Material mat; //貼圖材質
    public Vector3[] matrix; //把網格中各點的座標存下來
    public float levelY;
    public Texture2D image;
    public MeshRenderer mr;
    public MeshFilter mf;
 
    void Start()
    {
        print(image.width);
        print(image.height);
        Create();
    }
    private void OnValidate()
    {
        Create();
    }
    void Create()
    {
        //建立網格點座標陣列
        matrix = new Vector3[lengthX * lengthY];
        for (int y = 0; y < lengthY; ++y)
        {
            for (int x = 0; x < lengthX; ++x)
            {
                float levelYPosition = (image.GetPixel(x, y).grayscale) * levelY;
                matrix[y * lengthX + x] = new Vector3(((float)x) / 10, levelYPosition, ((float)y) / 10);
            }
        }

        //建立[vert][Normals][UVs]
        Vector3[] vertices = new Vector3[lengthX * lengthY];
        Vector3[] norms = new Vector3[lengthX * lengthY];
        Vector2[] UVs = new Vector2[lengthX * lengthY];

        for (int y = 0; y < lengthY; ++y)
        {
            for (int x = 0; x < lengthX; ++x)
            {
                vertices[y * lengthX + x] = matrix[y * lengthX + x];
                norms[y * lengthX + x] = Vector3.up;
                UVs[y * lengthX + x] = new Vector2((1 / (float)(lengthX - 1)) * x, (1 / (float)(lengthY - 1)) * y);
            }
        }

        //建立[Triangle]
        int[] triangles = new int[(lengthX - 1) * (lengthY - 1) * 6];
        int ind = 0;
        for (int y = 0; y < lengthY - 1; ++y)
        {
            for (int x = 0; x < lengthX - 1; ++x)
            {
                triangles[ind++] = y * lengthX + x;
                triangles[ind++] = (y + 1) * lengthX + (x + 1);
                triangles[ind++] = y * lengthX + (x + 1);
                triangles[ind++] = y * lengthX + x;
                triangles[ind++] = (y + 1) * lengthX + x;
                triangles[ind++] = (y + 1) * lengthX + (x + 1);
            }
        }

        //建立新的MeshRenderer並設定好材質

        Mesh meshData = new Mesh();
        meshData.vertices = vertices;
        meshData.normals = norms;
        meshData.triangles = triangles;
        meshData.uv = UVs;
        mr.material = mat;
        mf.mesh = meshData;

        //把這個Mesh掛在當前物件底下
    }
}
