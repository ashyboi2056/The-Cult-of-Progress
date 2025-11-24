using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class TargetUITransparencyOnTargetProximity : MonoBehaviour
{
    [SerializeField] public GameObject target;
    [SerializeField] public Image uiTarget;

    [Space(10)]

    [SerializeField] public float distanceMaxTransparency;
    [Label("Must be furthur than MaxTransparency")]
    [SerializeField] public float distanceCullPoint;

    [Space(10)]

    [SerializeField] public bool QueryTargetUIChildren = true;

    public void Update()
    {
        float distance = Vector2.Distance(target.transform.position, transform.position);
        float progressIntoProximity = distance + distanceMaxTransparency;
        Color colorLastFrame = uiTarget.color;
        float setAlpha = uiTarget.color.a;

        if (distance < distanceCullPoint)
        {
            setAlpha = progressIntoProximity / distanceMaxTransparency;
        }
        else { setAlpha = 0; }

        Color newColor = new Color(colorLastFrame.r, colorLastFrame.g, colorLastFrame.b, setAlpha);

        uiTarget.color = newColor;

        if (QueryTargetUIChildren)
        {
            if (uiTarget.transform.childCount < 1) { return; }
            foreach (Transform child in uiTarget.transform)
            {
                if (child.GetComponent<Image>() == null)
                {
                    continue;
                }
                child.GetComponent<Image>().color = newColor;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, distanceCullPoint);
        
        Gizmos.DrawWireSphere(transform.position, distanceMaxTransparency);
    }
}