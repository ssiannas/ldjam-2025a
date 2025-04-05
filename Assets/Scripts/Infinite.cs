using ldjam_hellevator;
using UnityEngine;

public class Infinite : MonoBehaviour
{
    Material mat;
    float distance = 0;
    
    [SerializeField] private WallData wallData;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        distance += Time.deltaTime * wallData.currentWallSpeed;
        mat.SetTextureOffset("_MainTex",Vector2.down*distance);
    }
}
