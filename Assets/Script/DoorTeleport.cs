// 시작 지점의 오브젝트를 컨트롤러로 가리키고 트리거 또는 A 버튼을 누르면 문 앞으로 이동합니다.
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

[DisallowMultipleComponent]
[RequireComponent(typeof(XRSimpleInteractable))]
public sealed class DoorTeleport : MonoBehaviour
{
    [SerializeField, Tooltip("XR Origin에 연결된 Teleportation Provider입니다. 즉시 이동하려면 Delay Time을 0으로 설정합니다.")]
    private TeleportationProvider teleportationProvider;
    [SerializeField, Tooltip("문 앞 바닥의 도착 위치입니다. 파란색 Z축이 문을 향하도록 배치합니다.")]
    private Transform destination;
    [SerializeField, Tooltip("왼손의 Ray 또는 Near-Far Interactor입니다.")]
    private XRBaseInputInteractor leftController;
    [SerializeField, Tooltip("오른손의 Ray 또는 Near-Far Interactor입니다. A 버튼은 오른손이 가리킬 때만 사용합니다.")]
    private XRBaseInputInteractor rightController;

    private XRSimpleInteractable interactable;
    private InputAction rightPrimaryButton;
    private static int lastTeleportFrame = -1;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetTeleportState() => lastTeleportFrame = -1;

    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        rightPrimaryButton = new InputAction("Door teleport A", InputActionType.Button,
            "<XRController>{RightHand}/primaryButton");
        if (teleportationProvider == null || destination == null ||
            (leftController == null && rightController == null))
        {
            Debug.LogError("DoorTeleport: Teleportation Provider, 도착 위치, 컨트롤러를 Inspector에 연결하세요.", this);
            enabled = false;
        }
    }

    private void OnEnable() => rightPrimaryButton?.Enable();
    private void OnDisable() => rightPrimaryButton?.Disable();
    private void OnDestroy() => rightPrimaryButton?.Dispose();

    // XRI의 hover 갱신 이후 입력을 확인합니다. 잡기/선택 입력은 이동에 사용하지 않습니다.
    private void LateUpdate()
    {
        if (lastTeleportFrame == Time.frameCount || !interactable.isActiveAndEnabled)
            return;

        var leftRequested = IsHovering(leftController) &&
                            leftController.activateInput.ReadWasPerformedThisFrame();
        var rightRequested = IsHovering(rightController) &&
                             (rightController.activateInput.ReadWasPerformedThisFrame() ||
                              rightPrimaryButton.WasPressedThisFrame());
        if (!leftRequested && !rightRequested)
            return;

        if (destination == null || teleportationProvider == null || !teleportationProvider.isActiveAndEnabled)
        {
            Debug.LogError("DoorTeleport: 도착 위치와 활성 Teleportation Provider를 확인하세요.", this);
            return;
        }

        var request = new TeleportRequest
        {
            destinationPosition = destination.position,
            destinationRotation = Quaternion.Euler(0f, destination.eulerAngles.y, 0f),
            matchOrientation = MatchOrientation.TargetUpAndForward,
            requestTime = Time.time,
        };
        if (teleportationProvider.QueueTeleportRequest(request))
            lastTeleportFrame = Time.frameCount;
    }

    private bool IsHovering(XRBaseInputInteractor controller)
    {
        return controller != null && controller.isActiveAndEnabled &&
               interactable.interactorsHovering.Contains(controller);
    }
}
