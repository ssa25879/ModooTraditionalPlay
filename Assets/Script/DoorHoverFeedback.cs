using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[DisallowMultipleComponent]
[RequireComponent(typeof(XRSimpleInteractable))]
public sealed class DoorHoverFeedback : MonoBehaviour
{
    [SerializeField] private bool enableHoverFeedback = true;
    [SerializeField] private TMP_Text instructionText;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private XRBaseInputInteractor leftController;
    [SerializeField] private XRBaseInputInteractor rightController;
    [SerializeField] private Color highlightColor = new Color(1f, 0.8f, 0.35f, 1f);
    [SerializeField] private Renderer[] panelBorders;

    private XRSimpleInteractable interactable;
    private string originalText;
    private Color originalInstructionColor;
    private Color originalTitleColor;
    private bool initialized;
    private bool highlighted;
    private MaterialPropertyBlock borderProperties;

    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        if (instructionText == null || titleText == null)
            return;
        originalText = instructionText.text;
        originalInstructionColor = instructionText.color;
        originalTitleColor = titleText.color;
        initialized = true;
    }

    private void LateUpdate()
    {
        if (!initialized)
            return;
        var hovered = enableHoverFeedback && interactable.isActiveAndEnabled &&
                      (IsHovering(leftController) || IsHovering(rightController));
        SetHighlighted(hovered);
    }

    private bool IsHovering(XRBaseInputInteractor controller)
    {
        return controller != null && controller.isActiveAndEnabled &&
               interactable.interactorsHovering.Contains(controller);
    }

    private void SetHighlighted(bool value)
    {
        if (!initialized || highlighted == value)
            return;
        highlighted = value;
        instructionText.text = value ? "트리거를 누르세요" : originalText;
        instructionText.color = value ? highlightColor : originalInstructionColor;
        titleText.color = value ? highlightColor : originalTitleColor;
        if (panelBorders == null)
            return;
        borderProperties ??= new MaterialPropertyBlock();
        foreach (var border in panelBorders)
        {
            if (border == null || border.sharedMaterial == null)
                continue;
            border.GetPropertyBlock(borderProperties);
            borderProperties.SetColor("_BaseColor", value ? highlightColor : border.sharedMaterial.GetColor("_BaseColor"));
            borderProperties.SetColor("_EmissionColor", value ? highlightColor * 0.35f : border.sharedMaterial.GetColor("_EmissionColor"));
            border.SetPropertyBlock(borderProperties);
        }
    }

    private void OnDisable() => SetHighlighted(false);
}
