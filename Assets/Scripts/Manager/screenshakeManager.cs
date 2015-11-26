using UnityEngine;
using System.Collections;

public class screenshakeManager : MonoBehaviour {

    public static screenshakeManager instance = null;

    void Awake()
    {
        if (screenshakeManager.instance == null)
            instance = this;
        if (this != screenshakeManager.instance)
            Destroy(gameObject);

        originPosition = transform.position;
        originRotation = transform.rotation;
    }


    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shake_decay;
    public float shake_intensity;
    public float shake_intensity_start;
    public float shake_duration = 1f;
    public float shake_start;


    void Update()
    {

        if (Input.GetKey(KeyCode.A))
            Shake(0.01f, 0.05f);
        if (shake_intensity > 0)
        {
            transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
            transform.rotation = new Quaternion(
            originRotation.x + Random.Range(-shake_intensity, shake_intensity) * .2f,
            originRotation.y + Random.Range(-shake_intensity, shake_intensity) * .2f,
            originRotation.z + Random.Range(-shake_intensity, shake_intensity) * .2f,
            originRotation.w + Random.Range(-shake_intensity, shake_intensity) * .2f);
            float ratio = Time.time/(shake_start+shake_duration);
            shake_intensity = shake_intensity_start- (shake_intensity_start*ratio);
        }
    }
    void FixedUpdate()
    {
        if (shake_intensity > 0)
        {
            transform.position = originPosition;
            transform.rotation = originRotation;
        }
    }

    public void Shake( float intensity,float time)
    {
        if(originPosition != Vector3.zero)
            transform.position = originPosition;
        transform.rotation = originRotation;
        originPosition = transform.position;
        originRotation = transform.rotation;
        shake_intensity = intensity;
        shake_intensity_start = intensity;
        shake_duration = time;
        shake_start = Time.time;
    }

}
