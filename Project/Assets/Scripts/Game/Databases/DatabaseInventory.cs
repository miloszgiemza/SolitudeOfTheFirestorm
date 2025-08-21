using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameDatabase
{
    [Serializable]
    public class DatabaseInventory
    {
        public BaseItem[] BigScrollsAll => bigScrollsAll;
        public BaseItem[] MixturesAll => mixturesAll;
        public BaseItem[] SmallScrollsAll => smallScrollsAll;

        [SerializeField] private BaseItem[] bigScrollsAll;
        [SerializeField] private BaseItem[] mixturesAll;
        [SerializeField] private BaseItem[] smallScrollsAll;

        private void CheckForItemInArray(ref bool itemFound, ref BaseItem itemToReturn, string ID, BaseItem[] itemsArray)
        {
            for(int i=0; i< itemsArray.Length && itemFound==false; i++)
            {
                if(String.Equals(itemsArray[i].IDGameDatabase, ID))
                {
                    itemFound = true;
                    itemToReturn = itemsArray[i];
                }
            }
        }

        public BaseItem ReturnItem(string ID)
        {
            BaseItem itemToReturn = null;
            bool itemFound = false;

            CheckForItemInArray(ref itemFound, ref itemToReturn, ID, bigScrollsAll);
            CheckForItemInArray(ref itemFound, ref itemToReturn, ID, MixturesAll);
            CheckForItemInArray(ref itemFound, ref itemToReturn, ID, smallScrollsAll);

            return itemToReturn;
        }
    }
}

