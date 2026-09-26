using Unity.VisualScripting;
using UnityEngine;

public class PaintPool : MonoBehaviour
{
    public Red Red;
    public Yellow Yellow;
    public Blue Blue;


    void Awake()
    {
        Red = this.AddComponent<Red>();

    }
}