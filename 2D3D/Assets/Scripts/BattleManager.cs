using UnityEngine;
using System.Collections.Generic;

using Assets.Scripts.RPG;

public class BattleManager : MonoBehaviour {

    public enum BattleState
    {
        ENTRY,
        PLAYER,
        BETWEEN,
        ENEMY,
        END

    }

    //private List<RpgCharacter> playerTeam;
    //private List<RpgCharacter> enemyTeam;
    //private EndCondition
    RpgCharacter player = new RpgCharacter();
    RpgCharacter enemy = new RpgCharacter();

	// Use this for initialization
	void Start () {

	}
	
	void Update () {
        if (Input.GetButtonUp("Fire1"))
        {
            player.Attack(enemy);
            // animate attack here
            // set healthbar for enemy to enemy.battleStats.CurHealth
            Debug.Log("Player HP:" + player.BattleStats.CurHealth);
            Debug.Log("Enemy HP:" + enemy.BattleStats.CurHealth);
        }
	}


    bool IsBattleOver() {
        // evaluate whether the battle is over or not
        // check Exit condition, etc
        return false;
    }
}
