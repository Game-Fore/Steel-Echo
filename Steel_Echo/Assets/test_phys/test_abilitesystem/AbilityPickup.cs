using UnityEngine;

public class AbilityPickup : MonoBehaviour
{
    public enum AbilityType
    {
        Attack,
        Dash,
        DoubleJump,
        WallClimb
    }

    public AbilityType abilityType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerAbilities abilities =
            collision.GetComponent<PlayerAbilities>();

        if (abilities == null) return;

        switch (abilityType)
        {
            case AbilityType.Attack:
                abilities.attackUnlocked = true;
                break;

            case AbilityType.Dash:
                abilities.dashUnlocked = true;
                break;

            case AbilityType.DoubleJump:
                abilities.doubleJumpUnlocked = true;
                break;

            case AbilityType.WallClimb:
                abilities.wallClimbUnlocked = true;
                break;
        }

        Debug.Log($"Unlocked: {abilityType}");

        Destroy(gameObject);
    }
}