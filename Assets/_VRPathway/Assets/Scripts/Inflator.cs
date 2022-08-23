using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Inflator : XRGrabInteractable
{
    [Header("Balloon Data")]
    public Transform attachPoint;
    public Balloon balloonPrefab;

    private Balloon m_BalloonInstance;

    private XRBaseController m_Controller;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        m_BalloonInstance = Instantiate(balloonPrefab, attachPoint);
        var controllerInteractor = args.interactorObject as XRBaseControllerInteractor;
        m_Controller = controllerInteractor.xrController;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        Destroy(m_BalloonInstance.gameObject);
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (isSelected && m_Controller != null)
        {
            m_BalloonInstance.transform.localScale = Vector3.one * Mathf.Lerp(1.0f, 4.0f, m_Controller.activateInteractionState.value);
        }
    }
}
