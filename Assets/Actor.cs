using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Actor : MonoBehaviour
{

    #region Enums

    public enum TargetSelectionRule
    {
        AnyAvailable,
        HighestHealth,
        StrongestAttack,
        OneShot
    }
    public TargetSelectionRule targetSelectionRule;

    [Tooltip("Don't change this property; it should be read-only.")]
    public Actor currentTarget;

    public enum ActionTarget
    {
        MeleeEnemy,
        AnyEnemy,
        AnyAlly,
        AllEnemy,
        AllAlly
    }

    public enum ActionEffect
    {
        Normal,
        Disable,
        Heal
    }

    public enum ActionSource
    {
        Weapon,
        Life,
        Death,
        Fire,
        Earth,
        Water,
        Air
    }

    public enum Position
    {
        left_rear_center,
        left_rear_bottom,
        left_rear_top,
        left_front_center,
        left_front_bottom,
        left_front_top,
        right_rear_center,
        right_rear_bottom,
        right_rear_top,
        right_front_center,
        right_front_bottom,
        right_front_top,
    }

    #endregion

    #region Configurable member variables

    public string actorName;

    public int maxHitPoints = 100;
    public int hitPoints = 100;

    public int initiative = 50;

    public ActionTarget actionTarget;

    public int damage = 25;

    public ActionEffect actionEffect;

    public ActionSource actionEffectSource;

    public ActionSource[] immunities;

    public int percentChanceToHit = 75;

    public Position boardPosition;

    public CustomAIList customAIList;

    // TODO: Provide some means of configuring how the target is selected

    #endregion

    #region Private member variables

    private BoardData boardData;

    #endregion

    #region Unity events (do not change)

    void Start()
    {
        boardData = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardData>();
    }

    void Update()
    {
        currentTarget = RefreshTargetSelection(GetAvailableTargets());
    }

    #endregion

    /// <summary>
    /// Modify this function however you need to. The main challenge of this project
    /// is to create a form-based means of authoring the AI for each actor based on
    /// the targets they can currently reach. This function receives a list of 
    /// available targets, and should choose an appropriate target based on how the
    /// designer has configured the AI.
    /// 
    /// How you implement this is up to you. Refer to the project requirements for
    /// what it has to be able to do.
    /// </summary>
    Actor RefreshTargetSelection(List<Actor> availableTargets)
    {
        if (availableTargets.Count == 0)
        {
            return null;
        }
        //Check if the custom ai field has been filled
        if (customAIList != null)
        {
            //Loop through all the elements in the customAI List
            for (int i = 0; i < customAIList.customAIs.Count; i++)
            {
                //Check whether it is to be checked vs highest or lowest
                if (customAIList.customAIs[i].highestOrLowest == CustomAI.HighestOrLowest.Highest)
                {
                    int highestComparison = 0;
                    //Get the property to be compared and get target
                    switch (customAIList.customAIs[i].propertyName)
                    {
                        case CustomAI.Properties.damage:
                            highestComparison = availableTargets[0].damage;
                            for (int j = 0; j < availableTargets.Count; j++)
                                if (availableTargets[j].damage > highestComparison)
                                {
                                    highestComparison = availableTargets[j].damage;
                                }
                            List<Actor> hDamageList = new List<Actor>();
                            for (int j = 0; j < availableTargets.Count; j++)
                            {
                                if (availableTargets[j].damage == highestComparison)
                                {
                                    hDamageList.Add(availableTargets[j]);
                                }
                            }
                            availableTargets = hDamageList;
                            break;

                        case CustomAI.Properties.hitPoints:
                            highestComparison = availableTargets[0].hitPoints;
                            for (int j = 0; j < availableTargets.Count; j++)
                                if (availableTargets[j].hitPoints > highestComparison)
                                {
                                    highestComparison = availableTargets[j].hitPoints;
                                }
                            List<Actor> hHitPoint = new List<Actor>();
                            for (int j = 0; j < availableTargets.Count; j++)
                            {
                                if (availableTargets[j].hitPoints == highestComparison)
                                {
                                    hHitPoint.Add(availableTargets[j]);
                                }
                            }
                            availableTargets = hHitPoint;
                            break;

                        case CustomAI.Properties.maxHitPoints:
                            highestComparison = availableTargets[0].maxHitPoints;
                            for (int j = 0; j < availableTargets.Count; j++)
                                if (availableTargets[j].maxHitPoints > highestComparison)
                                {
                                    highestComparison = availableTargets[j].maxHitPoints;
                                }
                            List<Actor> hMaxHitPoint = new List<Actor>();
                            for (int j = 0; j < availableTargets.Count; j++)
                            {
                                if (availableTargets[j].maxHitPoints == highestComparison)
                                {
                                    hMaxHitPoint.Add(availableTargets[j]);
                                }
                            }
                            availableTargets = hMaxHitPoint;
                            break;

                        case CustomAI.Properties.initiative:
                            highestComparison = availableTargets[0].initiative;
                            for (int j = 0; j < availableTargets.Count; j++)
                                if (availableTargets[j].initiative > highestComparison)
                                {
                                    highestComparison = availableTargets[j].initiative;
                                }
                            List<Actor> hInitiative = new List<Actor>();
                            for (int j = 0; j < availableTargets.Count; j++)
                            {
                                if (availableTargets[j].initiative == highestComparison)
                                {
                                    hInitiative.Add(availableTargets[j]);
                                }
                            }
                            availableTargets = hInitiative;
                            break;

                        case CustomAI.Properties.percentChanceToHit:
                            highestComparison = availableTargets[0].percentChanceToHit;
                            for (int j = 0; j < availableTargets.Count; j++)
                                if (availableTargets[j].percentChanceToHit > highestComparison)
                                {
                                    highestComparison = availableTargets[j].percentChanceToHit;
                                }
                            List<Actor> hPercentHit = new List<Actor>();
                            for (int j = 0; j < availableTargets.Count; j++)
                            {
                                if (availableTargets[j].percentChanceToHit == highestComparison)
                                {
                                    hPercentHit.Add(availableTargets[j]);
                                }
                            }
                            availableTargets = hPercentHit;
                            break;
                    }
                }
                else if (customAIList.customAIs[i].highestOrLowest == CustomAI.HighestOrLowest.Lowest)
                {
                    int lowestComparsion = 0;
                    //Get the property to be compared and get target
                    switch (customAIList.customAIs[i].propertyName)
                    {
                        case CustomAI.Properties.damage:
                            lowestComparsion = availableTargets[0].damage;
                            for (int j = 0; j < availableTargets.Count; j++)
                                if (availableTargets[j].damage < lowestComparsion)
                                {
                                    lowestComparsion = availableTargets[j].damage;
                                }
                            List<Actor> lDamage = new List<Actor>();
                            for (int j = 0; j < availableTargets.Count; j++)
                            {
                                if (availableTargets[j].damage == lowestComparsion)
                                {
                                    lDamage.Add(availableTargets[j]);
                                }
                            }
                            availableTargets = lDamage;
                            break;

                        case CustomAI.Properties.hitPoints:
                            lowestComparsion = availableTargets[0].hitPoints;
                            for (int j = 0; j < availableTargets.Count; j++)
                                if (availableTargets[j].hitPoints < lowestComparsion)
                                {
                                    lowestComparsion = availableTargets[j].hitPoints;
                                }
                            List<Actor> lHitPoint = new List<Actor>();
                            for (int j = 0; j < availableTargets.Count; j++)
                            {
                                if (availableTargets[j].hitPoints == lowestComparsion)
                                {
                                    lHitPoint.Add(availableTargets[j]);
                                }
                            }
                            availableTargets = lHitPoint;
                            break;

                        case CustomAI.Properties.maxHitPoints:
                            lowestComparsion = availableTargets[0].maxHitPoints;
                            for (int j = 0; j < availableTargets.Count; j++)
                                if (availableTargets[j].maxHitPoints < lowestComparsion)
                                {
                                    lowestComparsion = availableTargets[j].maxHitPoints;
                                }
                            List<Actor> lMaxHitPoints = new List<Actor>();
                            for (int j = 0; j < availableTargets.Count; j++)
                            {
                                if (availableTargets[j].maxHitPoints == lowestComparsion)
                                {
                                    lMaxHitPoints.Add(availableTargets[j]);
                                }
                            }
                            availableTargets = lMaxHitPoints;
                            break;

                        case CustomAI.Properties.initiative:
                            lowestComparsion = availableTargets[0].initiative;
                            for (int j = 0; j < availableTargets.Count; j++)
                                if (availableTargets[j].initiative < lowestComparsion)
                                {
                                    lowestComparsion = availableTargets[j].initiative;
                                }
                            List<Actor> lInitiative = new List<Actor>();
                            for (int j = 0; j < availableTargets.Count; j++)
                            {
                                if (availableTargets[j].initiative == lowestComparsion)
                                {
                                    lInitiative.Add(availableTargets[j]);
                                }
                            }
                            availableTargets = lInitiative;
                            break;

                        case CustomAI.Properties.percentChanceToHit:
                            lowestComparsion = availableTargets[0].percentChanceToHit;
                            for (int j = 0; j < availableTargets.Count; j++)
                                if (availableTargets[j].percentChanceToHit < lowestComparsion)
                                {
                                    lowestComparsion = availableTargets[j].percentChanceToHit;
                                }
                            List<Actor> lPercentToHit = new List<Actor>();
                            for (int j = 0; j < availableTargets.Count; j++)
                            {
                                if (availableTargets[j].percentChanceToHit == lowestComparsion)
                                {
                                    lPercentToHit.Add(availableTargets[j]);
                                }
                            }
                            availableTargets = lPercentToHit;
                            break;
                    }

                }
            }
            return availableTargets[Random.Range(0, availableTargets.Count)];
        }
        else
        {
            switch (targetSelectionRule)
            {
                case TargetSelectionRule.AnyAvailable:
                    return availableTargets[Random.Range(0, availableTargets.Count)];
                case TargetSelectionRule.HighestHealth:
                    int highestHealth = 0;
                    for (int i = 0; i < availableTargets.Count; i++)
                        if (availableTargets[i].hitPoints > highestHealth)
                            highestHealth = availableTargets[i].hitPoints;
                    List<int> highestHealthIndexes = new List<int>();
                    for (int i = 0; i < availableTargets.Count; i++)
                        if (availableTargets[i].hitPoints == highestHealth)
                            highestHealthIndexes.Add(i);
                    return availableTargets[highestHealthIndexes[Random.Range(0, highestHealthIndexes.Count)]];
                case TargetSelectionRule.StrongestAttack:
                    int highestAttack = 0;
                    for (int i = 0; i < availableTargets.Count; i++)
                        if (availableTargets[i].damage > highestAttack)
                            highestAttack = availableTargets[i].damage;
                    List<int> highestAttackIndexes = new List<int>();
                    for (int i = 0; i < availableTargets.Count; i++)
                        if (availableTargets[i].damage == highestAttack)
                            highestAttackIndexes.Add(i);
                    return availableTargets[highestAttackIndexes[Random.Range(0, highestAttackIndexes.Count)]];
                case TargetSelectionRule.OneShot:
                    for (int i = 0; i < availableTargets.Count; i++)
                        if (availableTargets[i].hitPoints < damage)
                            return availableTargets[i];
                    return availableTargets[Random.Range(0, availableTargets.Count)];
            }
        }
        return availableTargets[Random.Range(0, availableTargets.Count)];
    }

    #region Target selection core (do not change)

    BoardData.Side MySide { get { return (BoardData.Side)System.Enum.Parse(typeof(BoardData.Side), boardPosition.ToString().Split('_')[0]); } }
    BoardData.Rank MyRank { get { return (BoardData.Rank)System.Enum.Parse(typeof(BoardData.Rank), boardPosition.ToString().Split('_')[1]); } }
    BoardData.Line MyLine { get { return (BoardData.Line)System.Enum.Parse(typeof(BoardData.Line), boardPosition.ToString().Split('_')[2]); } }

    List<Actor> GetAvailableTargets()
    {
        List<Actor> result = new List<Actor>();
        BoardData.Side enemySide = MySide == BoardData.Side.left ? BoardData.Side.right : BoardData.Side.left;

        BoardData.Rank[] rankTargetOrder = new BoardData.Rank[] { BoardData.Rank.front, BoardData.Rank.rear };

        if (actionTarget == ActionTarget.MeleeEnemy)
        {
            // The weird one.
            // If I'm in the back row and anybody is in front of me, I cannot attack.
            if (MyRank == BoardData.Rank.rear)
            {
                if (boardData.GetActorByPosition(MySide, BoardData.Rank.front, BoardData.Line.top) != null
                    || boardData.GetActorByPosition(MySide, BoardData.Rank.front, BoardData.Line.center) != null
                    || boardData.GetActorByPosition(MySide, BoardData.Rank.front, BoardData.Line.bottom) != null)
                {
                    return result;
                }
            }

            // Melee units can only attack units that are right in front of them, or one line away from
            // their current line. They can only attack the rear rank once the front rank is empty.
            for (int i = 0; i < rankTargetOrder.Length && result.Count == 0; i++)
            {
                BoardData.Rank targetRank = rankTargetOrder[i];

                // I can always hit the center...
                Actor candidate = boardData.GetActorByPosition(enemySide, targetRank, BoardData.Line.center);
                if (candidate != null)
                {
                    result.Add(candidate);
                }
                // ... and my own line (applicable only if I'm not at the center).
                if (MyLine != BoardData.Line.center)
                {
                    candidate = boardData.GetActorByPosition(enemySide, targetRank, MyLine);
                    if (candidate != null) result.Add(candidate);
                }


                // I can only hit across the field if there's nobody in the way (applies to bottom and top lines only).
                if (MyLine == BoardData.Line.center || (MyLine == BoardData.Line.top && result.Count == 0))
                {
                    candidate = boardData.GetActorByPosition(enemySide, targetRank, BoardData.Line.bottom);
                    if (candidate != null) result.Add(candidate);
                }
                if (MyLine == BoardData.Line.center || (MyLine == BoardData.Line.bottom && result.Count == 0))
                {
                    candidate = boardData.GetActorByPosition(enemySide, targetRank, BoardData.Line.top);
                    if (candidate != null) result.Add(candidate);
                }
            }

        }
        else
        {
            BoardData.Line[] lines = new BoardData.Line[] { BoardData.Line.top, BoardData.Line.bottom, BoardData.Line.center };
            BoardData.Side targetSide = (actionTarget.ToString().EndsWith("Enemy")) ? enemySide : MySide;
            for (int l = 0; l < lines.Length; l++)
                for (int r = 0; r < rankTargetOrder.Length; r++)
                {
                    Actor candidate = boardData.GetActorByPosition(targetSide, rankTargetOrder[r], lines[l]);
                    if (candidate != null) result.Add(candidate);
                }

        }
        return result;
    }

    #endregion
}
