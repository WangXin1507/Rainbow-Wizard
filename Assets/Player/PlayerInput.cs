using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Reads A/S/D (including two-key combos), mouse, and Escape through Input Actions.
/// </summary>
[DefaultExecutionOrder(-1000)]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] InputActionAsset actions;

    public UnityEvent OnAPressed;
    public UnityEvent OnSPressed;
    public UnityEvent OnDPressed;
    public UnityEvent OnASPressed;
    public UnityEvent OnADPressed;
    public UnityEvent OnSDPressed;
    public UnityEvent OnEscapePressed;
    public UnityEvent<Vector2> OnLeftClick;
    public UnityEvent<Vector2> OnRightClick;
    public UnityEvent<Vector2> OnMiddleClick;

    public bool A { get; private set; }
    public bool S { get; private set; }
    public bool D { get; private set; }
    public bool AS { get; private set; }
    public bool AD { get; private set; }
    public bool SD { get; private set; }
    public bool Escape { get; private set; }
    public bool EscapePressed { get; private set; }

    public Vector2 MousePosition { get; private set; }
    public Vector2 MouseWorldPosition { get; private set; }
    public Vector2 MouseDelta { get; private set; }
    public Vector2 MouseScroll { get; private set; }

    public bool LeftMouse { get; private set; }
    public bool RightMouse { get; private set; }
    public bool MiddleMouse { get; private set; }
    public bool LeftMousePressed { get; private set; }
    public bool RightMousePressed { get; private set; }
    public bool MiddleMousePressed { get; private set; }

    InputActionAsset runtimeActions;
    bool ownsActions;

    InputAction aAction;
    InputAction sAction;
    InputAction dAction;
    InputAction escapeAction;
    InputAction pointAction;
    InputAction deltaAction;
    InputAction scrollAction;
    InputAction leftClickAction;
    InputAction rightClickAction;
    InputAction middleClickAction;

    Camera cam;

    void Awake()
    {
        cam = Camera.main;

        if (actions != null)
        {
            runtimeActions = actions;
        }

        InputActionMap map = runtimeActions.FindActionMap("Player", true);
        aAction = map.FindAction("A", true);
        sAction = map.FindAction("S", true);
        dAction = map.FindAction("D", true);
        escapeAction = map.FindAction("Escape", true);
        pointAction = map.FindAction("Point", true);
        deltaAction = map.FindAction("Delta", true);
        scrollAction = map.FindAction("Scroll", true);
        leftClickAction = map.FindAction("LeftClick", true);
        rightClickAction = map.FindAction("RightClick", true);
        middleClickAction = map.FindAction("MiddleClick", true);
    }

    void OnEnable()
    {
        runtimeActions?.Enable();
    }

    void OnDisable()
    {
        runtimeActions?.Disable();
    }

    void OnDestroy()
    {
        if (ownsActions && runtimeActions != null)
            Destroy(runtimeActions);
    }

    void Update()
    {
        A = aAction.IsPressed();
        S = sAction.IsPressed();
        D = dAction.IsPressed();
        Escape = escapeAction.IsPressed();

        AS = A && S;
        AD = A && D;
        SD = S && D;

        if (A) OnAPressed?.Invoke();
        if (S) OnSPressed?.Invoke();
        if (D) OnDPressed?.Invoke();
        if (AS) OnASPressed?.Invoke();
        if (AD) OnADPressed?.Invoke();
        if (SD) OnSDPressed?.Invoke();
        if (EscapePressed) OnEscapePressed?.Invoke();

        MousePosition = pointAction.ReadValue<Vector2>();
        MouseDelta = deltaAction.ReadValue<Vector2>();
        MouseScroll = scrollAction.ReadValue<Vector2>();
        MouseWorldPosition = ScreenToWorld(MousePosition);

        LeftMouse = leftClickAction.IsInProgress();
        RightMouse = rightClickAction.IsInProgress();
        MiddleMouse = middleClickAction.IsPressed();
        LeftMousePressed = leftClickAction.IsInProgress();
        RightMousePressed = rightClickAction.IsInProgress();
        MiddleMousePressed = middleClickAction.WasPressedThisFrame();

        if (LeftMousePressed) OnLeftClick?.Invoke(MouseWorldPosition);
        if (RightMousePressed) OnRightClick?.Invoke(MouseWorldPosition);
        if (MiddleMousePressed) OnMiddleClick?.Invoke(MouseWorldPosition);
    }

    static readonly Plane playPlane = new(Vector3.forward, Vector3.zero);

    Vector2 ScreenToWorld(Vector2 screen)
    {
        if (cam == null)
            cam = Camera.main;
        if (cam == null)
            return screen;

        Ray ray = cam.ScreenPointToRay(screen);
        if (playPlane.Raycast(ray, out float distance))
            return ray.GetPoint(distance);

        return screen;
    }
}
