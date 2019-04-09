using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Actor))]
public class ActorEditor : Editor
{
    bool boardFoldout = false;
    bool statsFoldout = false;
    bool attackFoldout = false;
    bool immunitiesFoldout = false;

    public override void OnInspectorGUI()
    {
        Actor newActor = target as Actor;

        //Name field
        newActor.actorName = EditorGUILayout.TextField("Actor Name", newActor.actorName);

        //Target Selection Field
        int targetSelection = 0;
        string[] targetSelectionOptions = { "Any Available", "Strongest Attack", "Highest Health" };
        targetSelection = EditorGUILayout.Popup("Target Selection Rule", (int)newActor.targetSelectionRule, targetSelectionOptions);

        switch (targetSelection)
        {
            case 0:
                newActor.targetSelectionRule = Actor.TargetSelectionRule.AnyAvailable;
                break;
            case 1:
                newActor.targetSelectionRule = Actor.TargetSelectionRule.StrongestAttack;
                break;
            case 2:
                newActor.targetSelectionRule = Actor.TargetSelectionRule.HighestHealth;
                break;
        }

        //Current Target
        newActor.currentTarget = EditorGUILayout.ObjectField("Current Target", newActor.currentTarget, typeof(Actor), true) as Actor;

        //Board Position
        boardFoldout = EditorGUILayout.Foldout(boardFoldout, "Board Position");
        if (boardFoldout)
        {
            int sideSelection = 0;
            int columnSelection = 0;
            int rowSelection = 0;
            string[] sideSelections = { "Left", "Right" };
            EditorGUILayout.LabelField("Side Selection");
            sideSelection = GUILayout.SelectionGrid(sideSelection, sideSelections, sideSelections.Length);
        }

        //Stats
        statsFoldout = EditorGUILayout.Foldout(statsFoldout, "Stats");
        if (statsFoldout)
        {
            //Max hit points
            //Setup variables to check if maxHitPoints gets changed
            bool maxHitChanged = false;
            int maxHitPrev = newActor.maxHitPoints;

            //Set maxHitPoints
            newActor.maxHitPoints = EditorGUILayout.IntSlider("Max Hit Points", newActor.maxHitPoints, 0, 1500);

            //Check if maxHitPoints was changed
            if (maxHitPrev != newActor.maxHitPoints)
            {
                maxHitChanged = true;
            }

            //Hit points
            //If maxHitPoints was changed, set hitPoints to new max otherwise use current value
            if (maxHitChanged)
            {
                newActor.hitPoints = EditorGUILayout.IntField("Hit Points", newActor.maxHitPoints);
            }
            else
            {
                newActor.hitPoints = EditorGUILayout.IntField("Hit Points", newActor.hitPoints);
            }

            //Initiative
            newActor.initiative = EditorGUILayout.IntSlider("Initiative", newActor.initiative, 10, 100);
            newActor.initiative = (newActor.initiative / 5) * 5;

            //Damage
            newActor.damage = EditorGUILayout.IntSlider("Damage", newActor.damage, 0, 180);

            //Percent chance to hit
            newActor.percentChanceToHit = EditorGUILayout.IntSlider("Percent Chance to Hit", newActor.percentChanceToHit, 0, 100);
        }

        //Attack information
        attackFoldout = EditorGUILayout.Foldout(attackFoldout, "Attack");
        if (attackFoldout)
        {
            //Action Target
            int actionTargetSelection = 0;
            string[] actionTargetSelectionOptions = { "Melee Enemy", "Any Enemy", "All Enemy", "Any Ally", "All Ally" };
            actionTargetSelection = EditorGUILayout.Popup("Action Target", (int)newActor.actionTarget, actionTargetSelectionOptions);

            switch (actionTargetSelection)
            {
                case 0:
                    newActor.actionTarget = Actor.ActionTarget.MeleeEnemy;
                    break;
                case 1:
                    newActor.actionTarget = Actor.ActionTarget.AnyEnemy;
                    break;
                case 2:
                    newActor.actionTarget = Actor.ActionTarget.AllEnemy;
                    break;
                case 3:
                    newActor.actionTarget = Actor.ActionTarget.AnyAlly;
                    break;
                case 4:
                    newActor.actionTarget = Actor.ActionTarget.AllAlly;
                    break;
            }

            //Action Effect
            int attackSelection = 0;
            string[] attackSelectionOptions = { "Normal", "Disable", "Heal"};
            attackSelection = EditorGUILayout.Popup("Action Effect", (int)newActor.actionEffect, attackSelectionOptions);

            switch (attackSelection)
            {
                case 0:
                    newActor.actionEffect = Actor.ActionEffect.Normal;
                    break;
                case 1:
                    newActor.actionEffect = Actor.ActionEffect.Disable;
                    break;
                case 2:
                    newActor.actionEffect = Actor.ActionEffect.Heal;
                    break;
            }

            //Action Effect Source
            int attackSourceSelection = 0;
            string[] attackSourceOptions = { "Weapon", "Life", "Death", "Earth", "Air", "Water", "Fire"};
            attackSourceSelection = EditorGUILayout.Popup("Action Effect Source", (int)newActor.actionEffectSource, attackSourceOptions);

            switch(attackSourceSelection)
            {
                case 0:
                    newActor.actionEffectSource = Actor.ActionSource.Weapon;
                    break;
                case 1:
                    newActor.actionEffectSource = Actor.ActionSource.Life;
                    break;
                case 2:
                    newActor.actionEffectSource = Actor.ActionSource.Death;
                    break;
                case 3:
                    newActor.actionEffectSource = Actor.ActionSource.Fire;
                    break;
                case 4:
                    newActor.actionEffectSource = Actor.ActionSource.Earth;
                    break;
                case 5:
                    newActor.actionEffectSource = Actor.ActionSource.Water;
                    break;
                case 6:
                    newActor.actionEffectSource = Actor.ActionSource.Air;
                    break;
            }
        }

        immunitiesFoldout = EditorGUILayout.Foldout(immunitiesFoldout, "Immunities");
        if (immunitiesFoldout)
        {

        }
    }
}
