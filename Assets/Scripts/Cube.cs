using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Vector3? DefaultPosition { get; private set; } = null;
    public float MinScale = 0.2f;
    public float MaxScale = 1f;
    public float Scale { get; set; } = 1f;
    public Color Color;

    private void OnValidate()
    {
        if (DefaultPosition == null)
        {
            DefaultPosition = transform.position;
        }
    }

    [ContextMenu("SetRandomScale")]
    public void SetRandomScale()
    {
        Scale = Random.Range(MinScale, MaxScale);
        transform.localScale = Vector3.one * Scale;
    }

    public void SetScale()
    {
        transform.localScale = Vector3.one * Scale;
    }

    public void SetRandomColor()
    {
        Color = Random.ColorHSV();
        GetComponent<Renderer>().sharedMaterial.color = Color;
    }

    public void SetColor()
    {
        GetComponent<Renderer>().sharedMaterial.color = Color;
    }
}
