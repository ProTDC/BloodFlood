using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingedEnemyDetection : MonoBehaviour
{
    [field: SerializeField]
    public bool PlayerDetected { get; private set; }
    public Vector2 DirectionToTarget => target.transform.position - detectorOrigin.position;

    [Header("OverlapBox Parameters")]
    [SerializeField]
    private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOriginOffset = Vector2.zero;

    public float detectionDelay = 0.3f;

    public LayerMask detectorLayerMask;

    [Header("Gizmo Parameters")]
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectedColor = Color.red;
    public bool showGizmos = true;

    private GameObject target;
    private WingedAI ai;
    private WingedMamaAI mamaAi;

    private int isMama;

    public GameObject Target
    {
        get => target;
        private set
        {
            target = value;
            PlayerDetected = target != null;
        }
    }

    private void Start()
    {
        if (GetComponent<WingedAI>() != null)
        {
            ai = GetComponent<WingedAI>();
            isMama = 1;
        }
        else
        {
            mamaAi = GetComponent<WingedMamaAI>();
            isMama = 2;
        }

        StartCoroutine(DetectionCoroutine());
    }
    private void Update()
    {
        if (PlayerDetected == true)
        {
            if (isMama == 1)
            {
                ai.enabled = true;
            }

            if (isMama == 2)
            {
                mamaAi.enabled = true;
            }
        }
        else
        {
            if (isMama == 1)
            {
                ai.enabled = false;
            }
            
            if (isMama == 2)
            {
                mamaAi.enabled = false;
            }
        }
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    public void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize, 0, detectorLayerMask);
        
        if (collider != null)
        {
            Target = collider.gameObject;
        }
        else
        {
            Target = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos && detectorOrigin != null)
        {
            if (showGizmos && detectorOrigin != null)
            {
                Gizmos.color = gizmoIdleColor;
                if (PlayerDetected)
                {
                    Gizmos.color = gizmoDetectedColor;
                }

                Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize);
            }
        }
    }


}
