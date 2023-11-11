using System.Collections.Generic;
using UnityEngine;

public class Car : PoolableObject
{
   private int a = 5;
   
   public override void Init(Transform parent = null)
   {
      base.Init(parent);
      a = 10;

      List<int> myList = new List<int>();
   }

   public override void Clear()
   {
      base.Clear();
      a = 5;
   }
}

