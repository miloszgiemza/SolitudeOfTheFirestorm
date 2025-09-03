using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameDatabase
{
    [Serializable]
    public class DatabaseSpells
    {
        public BaseSpell[] SpellsAll => spellsAll;

        [SerializeField] private BaseSpell[] spellsAll;

        public BaseSpell ReturnItem(string ID)
        {
            BaseSpell itemToReturn = null;
            bool itemFound = false;

            for(int i=0; i < spellsAll.Length && itemFound==false; i++)
            {
                if (String.Equals(spellsAll[i].IDGameDatabase, ID))
                {
                    itemFound = true;
                    itemToReturn = spellsAll[i];
                }
            }

            return itemToReturn;
        }
    }
}