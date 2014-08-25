using UnityEngine;
using System.Collections;

public abstract class Spell {

    protected GameObject player;

    protected MovementController playerMC { get { return player.GetComponent<MovementController>(); } }
    public Spell(GameObject player) { this.player = player; }
    public abstract void SpellUpdate();
}
