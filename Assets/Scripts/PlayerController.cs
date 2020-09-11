using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is probably not going to be used
public class PlayerController : MonoBehaviour
{
    /*
    Spells:
    0 = no spell selected
    1 = Stun Spell
    2 = Heal Spell
    3 = Speed Spell
         */

    //todo: different costs for different spells?

    int currentSpell = 0;
    private Transform aimPrefab;
    public Transform speedCircle, healCircle, stunCircle;
    static SpellArea spellArea;

    public BarScript bar;

    public List<Button> spellButtons;
    private bool spellsEnabled;

    void Start() 
    {
            foreach(Button button in spellButtons)
            {
                button.interactable = false;
            }
            spellsEnabled = false;
    }

    public void useSpell(int spell)
    {
        if(currentSpell!=0)
        {
            spellArea.endCircle();
            GameyManager.levelMana += 100;
        }

        //hardcoded 100
        if(100 <= GameyManager.levelMana) {
            currentSpell = spell;
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            if(spell==1)
            {
                aimPrefab = stunCircle;
            } else if(spell==2)
            {
                aimPrefab = healCircle;
            } else if(spell == 3)
            {
                aimPrefab = speedCircle;
            }
            Transform spellCircle = Instantiate(aimPrefab, new Vector3(mousePosition.x, mousePosition.y, 0), Quaternion.identity);
            spellArea = spellCircle.GetComponent<SpellArea>();
            spellArea.spell = spell;
            //todo: change the -= to not be hardcoded
            GameyManager.levelMana -= 100;
        }
        
    }

    void Update()
    {
        if(GameyManager.levelMana < 100 && spellsEnabled == true)
        {
            foreach(Button button in spellButtons)
            {
                button.interactable = false;
            }
            spellsEnabled = false;
        }

        if (currentSpell != 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                spellArea.activateCircle();
                currentSpell = 0;
                GameyManager.spellsCast += 1;
            }
            if(Input.GetMouseButtonDown(1))
            {
                currentSpell = 0;
                GameyManager.levelMana += 100;
                Destroy(spellArea.gameObject);
                foreach (Button button in spellButtons)
                {
                    if (button.name != "disabled")
                    {
                        button.interactable = true;
                    }
                }
                spellsEnabled = true;
            }

            bar.UpdateImage();
        }

        if (GameyManager.gameState == GameyManager.GameState.Play)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1) && spellButtons[0].interactable==true)
            {
                useSpell(1);
                GameyManager.spellsCast += 1;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2) && spellButtons[1].interactable == true)
            {
                useSpell(2);
                GameyManager.spellsCast += 1;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3) && spellButtons[2].interactable == true)
            {
                useSpell(3);
                GameyManager.spellsCast += 1;
            }

            bar.UpdateImage();
        }
    }

    public void EnableSpells()
    {
        foreach(Button button in spellButtons)
        {

            if(button.name != "disabled") button.interactable = true;
        }
        spellsEnabled = true;
    }
}
