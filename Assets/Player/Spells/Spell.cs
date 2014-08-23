using UnityEngine;
using System.Collections;

public abstract class Spell {

    protected GameObject player;
    public Spell(GameObject player) { this.player = player; }
    public abstract void SpellUpdate();
}
