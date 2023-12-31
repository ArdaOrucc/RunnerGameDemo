using UnityEngine;

public class Player : PoolableObject
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private float xClamp = 2;
    private static readonly int RunKey = Animator.StringToHash("Walk");
    private InputHandler _inputHandler;
    private bool _isRunning;
    private const int ScaleAmount = 10; 

    private void Awake()
    {
        _inputHandler = new InputHandler();
        _inputHandler.OnPointerDownAction += StartRunning;
        _inputHandler.OnPointerAction += Drag;
    }

    private void OnDestroy()
    {
        _inputHandler.OnPointerDownAction -= StartRunning;
        _inputHandler.OnPointerAction -= Drag;
    }

    private void Update()
    {
        _inputHandler.PointerUpdate();
        MoveForward();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out Gate gate)) return;
        
        var isGoodGate =  gate.GetGateStatus(); 
        ChangeHeadBlendShape(isGoodGate);
    }

    private void StartRunning()
    {
        _inputHandler.OnPointerDownAction -= StartRunning;
        _isRunning = true;
        animator.SetTrigger(RunKey);
    }

    private void Drag()
    {
        if(!_isRunning) return;

        var newPosition = transform.position;

        if (_inputHandler.DeltaPosition.x < 0 && newPosition.x > -xClamp 
            || _inputHandler.DeltaPosition.x > 0 && newPosition.x < xClamp)
        {
            newPosition.x += _inputHandler.DeltaPosition.x * speed * Time.deltaTime;
        }

        transform.position = newPosition;
    }
    
    private void MoveForward()
    {
        if(!_isRunning) return;

        var newPosition = transform.position;
        newPosition.z += speed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void ChangeHeadBlendShape(bool willScaleUp)
    {
        var blendShapeValue = skinnedMeshRenderer.GetBlendShapeWeight(0);
        var increaseAmount = willScaleUp 
            ? blendShapeValue + ScaleAmount
            : blendShapeValue - ScaleAmount;
        skinnedMeshRenderer.SetBlendShapeWeight(0, increaseAmount);
    }
}
