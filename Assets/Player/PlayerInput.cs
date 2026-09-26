using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Reads A/S/D (including two-key combos), mouse, and Escape through Input Actions.
/// Assign PlayerInputActions, or a default map is created at runtime.
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
    public UnityEvent OnLeftClick;
    public UnityEvent OnRightClick;
    public UnityEvent OnMiddleClick;

    public bool A { get; private set; }
    public bool S { get; private set; }
    public bool D { get; private set; }
    public bool AS { get; private set; }
    public bool AD { get; private set; }
    public bool SD { get; private set; }
    public bool Escape { get; private set; }

    public bool APressed { get; private set; }
    public bool SPressed { get; private set; }
    public bool DPressed { get; private set; }
    public bool ASPressed { get; private set; }
    public bool ADPressed { get; private set; }
    public bool SDPressed { get; private set; }
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
        else
        {
            runtimeActions = CreateDefaultActions();
            ownsActions = true;
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

        APressed = aAction.WasPressedThisFrame();
        SPressed = sAction.WasPressedThisFrame();
        DPressed = dAction.WasPressedThisFrame();
        EscapePressed = escapeAction.WasPressedThisFrame();

        ASPressed = AS && (APressed || SPressed);
        ADPressed = AD && (APressed || DPressed);
        SDPressed = SD && (SPressed || DPressed);

        if (APressed) OnAPressed?.Invoke();
        if (SPressed) OnSPressed?.Invoke();
        if (DPressed) OnDPressed?.Invoke();
        if (ASPressed) OnASPressed?.Invoke();
        if (ADPressed) OnADPressed?.Invoke();
        if (SDPressed) OnSDPressed?.Invoke();
        if (EscapePressed) OnEscapePressed?.Invoke();

        MousePosition = pointAction.ReadValue<Vector2>();
        MouseDelta = deltaAction.ReadValue<Vector2>();
        MouseScroll = scrollAction.ReadValue<Vector2>();
        MouseWorldPosition = ScreenToWorld(MousePosition);

        LeftMouse = leftClickAction.IsPressed();
        RightMouse = rightClickAction.IsPressed();
        MiddleMouse = middleClickAction.IsPressed();
        LeftMousePressed = leftClickAction.WasPressedThisFrame();
        RightMousePressed = rightClickAction.WasPressedThisFrame();
        MiddleMousePressed = middleClickAction.WasPressedThisFrame();

        if (LeftMousePressed) OnLeftClick?.Invoke();
        if (RightMousePressed) OnRightClick?.Invoke();
        if (MiddleMousePressed) OnMiddleClick?.Invoke();
    }

    Vector2 ScreenToWorld(Vector2 screen)
    {
        if (cam == null)
            cam = Camera.main;
        if (cam == null)
            return screen;

        return cam.ScreenToWorldPoint(screen);
    }

    static InputActionAsset CreateDefaultActions()
    {
        InputActionAsset asset = ScriptableObject.CreateInstance<InputActionAsset>();
        InputActionMap map = asset.AddActionMap("Player");

        map.AddAction("A", InputActionType.Button, "<Keyboard>/a");
        map.AddAction("S", InputActionType.Button, "<Keyboard>/s");
        map.AddAction("D", InputActionType.Button, "<Keyboard>/d");
        map.AddAction("Escape", InputActionType.Button, "<Keyboard>/escape");

        InputAction point = map.AddAction("Point", InputActionType.Value);
        point.AddBinding("<Mouse>/position");

        InputAction delta = map.AddAction("Delta", InputActionType.Value);
        delta.AddBinding("<Mouse>/delta");

        InputAction scroll = map.AddAction("Scroll", InputActionType.Value);
        scroll.AddBinding("<Mouse>/scroll");

        map.AddAction("LeftClick", InputActionType.Button, "<Mouse>/leftButton");
        map.AddAction("RightClick", InputActionType.Button, "<Mouse>/rightButton");
        map.AddAction("MiddleClick", InputActionType.Button, "<Mouse>/middleButton");

        return asset;
    }
}
