using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Actor))]
public class ActorEditor : Editor
{
    bool boardFoldout = false;

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
    }
}
