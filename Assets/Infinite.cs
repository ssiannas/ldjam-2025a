using UnityEngine;

public class Infinite : MonoBehaviour
{
    Material mat;
    float distance = 0;

    public float speed=2f;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        distance += Time.deltaTime*speed;
        Debug.Log(mat.name);
        mat.SetTextureOffset("_MainTex",Vector2.up*distance);
    }
}
