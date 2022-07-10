using UnityEngine;

public class MenuLighting : MonoBehaviour
{
    private Material stoneMat;

    private float minIntensity = 1f;
    private float maxIntensity = 2f;

    static float t = 0.0f;

    private void Awake()
    {
        stoneMat = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        stoneMat.color = stoneMat.GetColor("_EmissionColor") * Mathf.Lerp(minIntensity, maxIntensity, t);

        t += Time.deltaTime;

        if (t > 1.0f)
        {
            float temp = maxIntensity;
            maxIntensity = minIntensity;
            minIntensity = temp;
            t = 0.0f;
        }
    }
}
