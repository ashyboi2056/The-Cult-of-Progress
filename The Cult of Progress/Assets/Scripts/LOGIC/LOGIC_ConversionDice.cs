using NaughtyAttributes;
using UnityEngine;

public class LOGIC_ConversionDice : MonoBehaviour
{
    public DiceType type;

    public Transform[] facePoints; // Assign 6 face markers in inspector

    private bool result;

    public int toBeat = 6; // Meet to Beat

    public bool hasSettled = false;

    public Rigidbody rb;


    [Label("Result:")] public int faceUp;

    void Awake()
    {
        // Random force direction
        Vector3 forceDirection = new Vector3(
            Random.Range(-1f, 1f),
            1f,
            Random.Range(-1f, 1f)
        ).normalized;

        // Random torque
        float torqueMag = RULES.RULE_UX_diceRandomTorqueMagnitude;
        Vector3 torque = new Vector3(
            Random.Range(-torqueMag, torqueMag),
            Random.Range(-torqueMag, torqueMag),
            Random.Range(-torqueMag, torqueMag)
        );

        // Apply force and torque
        rb.AddForce(forceDirection * Random.Range(RULES.RULE_UX_diceRandomMinForceMagnitude, RULES.RULE_UX_diceRandomMaxForceMagnitude), ForceMode.Impulse);
        rb.AddTorque(torque, ForceMode.Impulse);
    }

    void Update() {
        if (!hasSettled) {
            if (rb.linearVelocity.magnitude < 0.05f && rb.angularVelocity.magnitude < 0.05f) {
                hasSettled = true;
                // Notify manager this die is done
                LOGIC_ConversionDiceManager.instance.OnDieSettled(this);
            }
        }

        float maxDot = -Mathf.Infinity;
        int bestFace = -1;

        for (int i = 0; i < facePoints.Length; i++) {
            float dot = Vector3.Dot(facePoints[i].up, Vector3.up);
            if (dot > maxDot) {
                maxDot = dot;
                bestFace = i;
            }
        }

        faceUp = bestFace + 1;
    }

    public bool CalculateResult() {
        float maxDot = -Mathf.Infinity;
        int bestFace = -1;

        for (int i = 0; i < facePoints.Length; i++) {
            float dot = Vector3.Dot(facePoints[i].up, Vector3.up);
            if (dot > maxDot) {
                maxDot = dot;
                bestFace = i;
            }
        }

        bestFace += 1; // assuming facePoints[0] = side "1"

        result = bestFace >= toBeat;

        return result;
    }
}

public enum DiceType
{
    Int,
    Cha,
    Str
}