using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[CustomEditor(typeof(EnemySpawn))]
public class PatrolHelper : Editor
{
    private EnemySpawn _enemySpawn;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (Selection.activeGameObject.GetComponent<EnemySpawn>())
        {
            if (_enemySpawn == null)
            {
                _enemySpawn = Selection.activeGameObject.GetComponent<EnemySpawn>();
            }
        }

        Debug.Log(_enemySpawn.patrolPoints.Length);
        if (_enemySpawn.patrolPoints.Length < 2)
        {
            _enemySpawn.patrolPoints = new GameObject[2];
        }

        if (Selection.activeGameObject.GetComponent<EnemySpawn>().enemyTypes == EnemySpawn.EnemyTypes.Patrol)
        {
            if (_enemySpawn.patrolPoints[0] == null || _enemySpawn.patrolPoints[1] == null)
            {
                NukeControlPoints();
                _enemySpawn.patrolPoints[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _enemySpawn.patrolPoints[0].name = "Patrol Point 1";
                _enemySpawn.patrolPoints[0].transform.position = Selection.activeGameObject.transform.position + new Vector3(2, 0, 0);
                _enemySpawn.patrolPoints[1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _enemySpawn.patrolPoints[1].name = "Patrol Point 2";
                _enemySpawn.patrolPoints[1].transform.position = Selection.activeGameObject.transform.position + new Vector3(-2, 0, 0);
                
                EditorUtility.SetDirty(_enemySpawn);
            }

            Handles.DrawDottedLine(_enemySpawn.patrolPoints[0].transform.position, _enemySpawn.patrolPoints[1].transform.position, 5);
        }
        else
        {
            NukeControlPoints();
        }
    }

    public void NukeControlPoints()
    {
        for (int i = 0; i < _enemySpawn.patrolPoints.Length; i++)
        {
            if (_enemySpawn.patrolPoints[i] != null)
            {
                DestroyImmediate(_enemySpawn.patrolPoints[i]);
            }
        }
    }
}