using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionScript : MonoBehaviour {

    public int faction = 0;

    private SpriteRenderer sr;

    private bool isPlayer = false;

    //0 - blue, 1 - red, 2 - green 
    public Color factionBlue;
    public Color factionRed;
    public Color factionGreen;

    private Color[] factionColorArray = new Color[3];

    private void Awake()
    {
        if (CompareTag("Player"))
        {
            isPlayer = true;
        }

        if (isPlayer)
        {
            sr = transform.GetChild(1).GetComponent<SpriteRenderer>();
        }
        else
        {
            sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        factionColorArray[0] = factionBlue;
        factionColorArray[1] = factionRed;
        factionColorArray[2] = factionGreen;

        if (isPlayer)
            faction = DetermineFaction();
    }

    // Use this for initialization
    void Start () {
        SetFactionMaterial();
	}

    public int DetermineFaction()
    {
        return Random.Range(0, 3);
    }

    public void DetermineFaction(int newFaction)
    {
        faction = newFaction;
        SetFactionMaterial();
    }

    public void SetFactionMaterial()
    {
        sr.color = factionColorArray[faction];
    }

    public bool CompareFaction(int otherFaction)
    {
        if (otherFaction == faction)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
