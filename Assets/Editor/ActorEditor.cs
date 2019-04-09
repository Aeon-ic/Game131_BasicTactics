using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


[CustomEditor(typeof(Actor))]
public class ActorEditor : Editor
{
    bool boardFoldout = false;
    bool statsFoldout = false;
    bool attackFoldout = false;
    bool immunitiesFoldout = false;
    int sideSelection = 0;
    int columnSelection = 0;
    int rowSelection = 0;

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
            //Get old Board position
            switch (newActor.boardPosition)
            {
                case Actor.Position.left_front_top:
                    sideSelection = 0;
                    columnSelection = 0;
                    rowSelection = 0;
                    break;
                case Actor.Position.left_front_center:
                    sideSelection = 0;
                    columnSelection = 0;
                    rowSelection = 1;
                    break;
                case Actor.Position.left_front_bottom:
                    sideSelection = 0;
                    columnSelection = 0;
                    rowSelection = 2;
                    break;
                case Actor.Position.left_rear_top:
                    sideSelection = 0;
                    columnSelection = 1;
                    rowSelection = 0;
                    break;
                case Actor.Position.left_rear_center:
                    sideSelection = 0;
                    columnSelection = 1;
                    rowSelection = 1;
                    break;
                case Actor.Position.left_rear_bottom:
                    sideSelection = 0;
                    columnSelection = 1;
                    rowSelection = 2;
                    break;
                case Actor.Position.right_front_top:
                    sideSelection = 1;
                    columnSelection = 0;
                    rowSelection = 0;
                    break;
                case Actor.Position.right_front_center:
                    sideSelection = 1;
                    columnSelection = 0;
                    rowSelection = 1;
                    break;
                case Actor.Position.right_front_bottom:
                    sideSelection = 1;
                    columnSelection = 0;
                    rowSelection = 2;
                    break;
                case Actor.Position.right_rear_top:
                    sideSelection = 1;
                    columnSelection = 1;
                    rowSelection = 0;
                    break;
                case Actor.Position.right_rear_center:
                    sideSelection = 1;
                    columnSelection = 1;
                    rowSelection = 1;
                    break;
                case Actor.Position.right_rear_bottom:
                    sideSelection = 1;
                    columnSelection = 1;
                    rowSelection = 2;
                    break;
            }
            
            //Side of Board
            EditorGUILayout.LabelField("Side: ");
            string[] sideOptions = { "Left", "Right" };
            sideSelection = GUILayout.SelectionGrid(sideSelection, sideOptions, sideOptions.Length);

            //Column of Board
            EditorGUILayout.LabelField("Column: ");
            string[] columnOptions = { "Front", "Rear" };
            columnSelection = GUILayout.SelectionGrid(columnSelection, columnOptions, columnOptions.Length);

            
            //Row of Board
            EditorGUILayout.LabelField("Row: ");
            string[] rowOptions = { "Top", "Center", "Bottom" };
            rowSelection = GUILayout.SelectionGrid(rowSelection, rowOptions, rowOptions.Length);

            //Set new Board position
            if (sideSelection == 0)
            {
                if (columnSelection == 0)
                {
                    if (rowSelection == 0)
                    {
                        newActor.boardPosition = Actor.Position.left_front_top;
                    }
                    else if (rowSelection == 1)
                    {
                        newActor.boardPosition = Actor.Position.left_front_center;
                    }
                    else
                    {
                        newActor.boardPosition = Actor.Position.left_front_bottom;
                    }
                }
                else
                {
                    if (rowSelection == 0)
                    {
                        newActor.boardPosition = Actor.Position.left_rear_top;
                    }
                    else if (rowSelection == 1)
                    {
                        newActor.boardPosition = Actor.Position.left_rear_center;
                    }
                    else
                    {
                        newActor.boardPosition = Actor.Position.left_rear_bottom;
                    }
                }
            }
            else
            {
                if (columnSelection == 0)
                {
                    if (rowSelection == 0)
                    {
                        newActor.boardPosition = Actor.Position.right_front_top;
                    }
                    else if (rowSelection == 1)
                    {
                        newActor.boardPosition = Actor.Position.right_front_center;
                    }
                    else
                    {
                        newActor.boardPosition = Actor.Position.right_front_bottom;
                    }
                }
                else
                {
                    if (rowSelection == 0)
                    {
                        newActor.boardPosition = Actor.Position.right_rear_top;
                    }
                    else if (rowSelection == 1)
                    {
                        newActor.boardPosition = Actor.Position.right_rear_center;
                    }
                    else
                    {
                        newActor.boardPosition = Actor.Position.right_rear_bottom;
                    }
                }
            }
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
            string[] attackSourceOptions = { "Weapon", "Life", "Death", "Fire", "Earth", "Water", "Air"};
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
            //Setup variables for toggling immunities
            bool weaponImmunity = false;
            bool lifeImmunity = false;
            bool deathImmunity = false;
            bool fireImmunity = false;
            bool earthImmunity = false;
            bool waterImmunity = false;
            bool airImmunity = false;

            //Get current immunities
            foreach (Actor.ActionSource currSource in newActor.immunities)
            {
                switch (currSource)
                {
                    case Actor.ActionSource.Weapon:
                        weaponImmunity = true;
                        break;
                    case Actor.ActionSource.Life:
                        lifeImmunity = true;
                        break;
                    case Actor.ActionSource.Death:
                        deathImmunity = true;
                        break;
                    case Actor.ActionSource.Fire:
                        fireImmunity = true;
                        break;
                    case Actor.ActionSource.Earth:
                        earthImmunity = true;
                        break;
                    case Actor.ActionSource.Water:
                        waterImmunity = true;
                        break;
                    case Actor.ActionSource.Air:
                        airImmunity = true;
                        break;
                }
            }

            //Immunities
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            weaponImmunity = EditorGUILayout.ToggleLeft("Weapon", weaponImmunity, GUILayout.MaxWidth(100));
            lifeImmunity = EditorGUILayout.ToggleLeft("Life", lifeImmunity, GUILayout.MaxWidth(100));
            deathImmunity = EditorGUILayout.ToggleLeft("Death", deathImmunity, GUILayout.MaxWidth(100));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            fireImmunity = EditorGUILayout.ToggleLeft("Fire", fireImmunity, GUILayout.MaxWidth(100));
            earthImmunity = EditorGUILayout.ToggleLeft("Earth", earthImmunity, GUILayout.MaxWidth(100));
            waterImmunity = EditorGUILayout.ToggleLeft("Water", waterImmunity, GUILayout.MaxWidth(100));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            airImmunity = EditorGUILayout.ToggleLeft("Air", airImmunity, GUILayout.MaxWidth(100));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            //Set immunities list
            //Create List for outputting
            List<Actor.ActionSource> immunityOutput = new List<Actor.ActionSource>();

            //Add to list
            if(weaponImmunity)
            {
                immunityOutput.Add(Actor.ActionSource.Weapon);
            }
            if (lifeImmunity)
            {
                immunityOutput.Add(Actor.ActionSource.Life);
            }
            if (deathImmunity)
            {
                immunityOutput.Add(Actor.ActionSource.Death);
            }
            if (fireImmunity)
            {
                immunityOutput.Add(Actor.ActionSource.Fire);
            }
            if (earthImmunity)
            {
                immunityOutput.Add(Actor.ActionSource.Earth);
            }
            if (waterImmunity)
            {
                immunityOutput.Add(Actor.ActionSource.Water);
            }
            if (airImmunity)
            {
                immunityOutput.Add(Actor.ActionSource.Air);
            }

            //Output list
            newActor.immunities = immunityOutput.ToArray();
        }
    }
}
