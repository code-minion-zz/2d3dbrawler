using UnityEngine;
using System.Collections;

public class B_BattleMgr : MonoBehaviour {
	
	// Boolean phases
	bool myTurn;
	bool isAttacking;
	bool isRunning;
	public bool ambushed;

	// Chance of enemy attacking
	int chance = Random.Range (0, 9);

	// Specify stats for the game object to which this script is attached
	// We can have varied values for each game object, right?
	public int ATK;
	public int HP;
	public int maxHP;

	// Assign who is player, who is enemy
	GameObject player = GameObject.FindWithTag("player");
	GameObject enemy = GameObject.FindWithTag("enemy");
	// What if we have multiple enemies?
	// How do we assign as the enemy just the particular one in the battle?

	// Set HP and ATK
	// Public stuff above is to be accessed here
	int ownATK = player.GetComponent < this > (ATK);
	int enemyATK = enemy.GetComponent < this > (ATK);
	int ownHP = player.GetComponent < this > (HP);
	int enemyHP = enemy.GetComponent < this > (HP);

	void Start () {
		// Check who goes first based on whether the encounter is an ambush
		// Reversed, because it will be switched back on CalculateHP();
		if (ambushed) {
			myTurn = true;
		} else {
			myTurn = false;
		}
		// Make sure HP doesn't exceed maximum
		if (HP > maxHP) {
			HP = maxHP;
		}
		// Initiate first functions
		ResetATK ();
		CalculateHP ();
		TurnCheck ();
	}

	void ResetATK () {
		ownATK = 0;
		enemyATK = 0;
	}

	void CalculateHP () {

		ownHP = ownHP - enemyATK;
		enemyHP = enemyHP - ownATK;

		Debug.Log ("Remaining Health: " + ownHP);
		Debug.Log ("Enemy Health: " + enemyHP);

		this.isAttacking = false;
		this.isRunning = false;
		myTurn = !myTurn;

		if (ownHP > 0 && enemyHP > 0) {
			ResetATK ();
			TurnCheck ();
		} else if (ownHP > 0 && enemyHP <= 0) {
			Debug.Log ("A winner is you!"); // Play winning animation
			Application.Quit; // Exit battle, shouldn't just quit
		} else if (ownHP <= 0 && enemyHP > 0) {
			Debug.Log ("You suck at battles") // Play losing animation
			Application.Quit; // Game over
		}
	}

	void TurnCheck () {
		if (myTurn) {
			TurnStart ();
		} else {
			Brace ();
		}
	}

	void TurnStart () {
		// Bring up GUI to call Attack or Run
		if (Input.GetKeyDown(KeyCode.Space)){
			Attack ();
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Run ();
		}
	}

	void Attack () {
		this.isAttacking = true;
		this.ATK = 10; // How do we set this to the value we set initially before ResetATK (); ?
		Debug.Log ("This is Spartaaa!"); // Play attacking animation
		// The other gets hit, animate stuff
		CalculateHP ();
	}

	void Run () {
		this.isRunning = true;
		Debug.Log ("Run for your lives!"); // Play running animation
		// Insert some delay, then exit the game
		Application.Quit;
	}

	void Brace () {
		// Disable movement -- do we need this?
		// Randomize enemy choice, 90% attack or 10% run
		if (chance > 1) {
			enemy.GetComponent<this>().Attack();
		} else {
			enemy.GetComponent<this>().Run();
		}
		// Do we do nothing and let the enemy's Run or Attack functions calculate things, or
		// Do we do stuff based on if enemy isAttacking or isRunning?
	}
}