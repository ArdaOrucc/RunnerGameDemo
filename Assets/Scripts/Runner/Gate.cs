using UnityEngine;

public class Gate : PoolableObject
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Color goodColor, badColor;
    public bool IsGoodGate { get; set; }

    public override void Init(Transform parent = null)
    {
        base.Init(parent);
        meshRenderer.material.color = IsGoodGate ? goodColor : badColor;
    }

    public bool GetGateStatus()
    {
        return IsGoodGate;
    }
}
