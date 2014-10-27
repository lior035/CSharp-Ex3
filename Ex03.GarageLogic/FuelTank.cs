namespace Ex03.GarageLogic
{
     using System;
     using System.Collections.Generic;
     using System.Text;
   
     public class FuelTank
     {
          private readonly float r_MaximumFuelCapacity;
          private readonly eFuelType r_FuelType;
          private float m_CurrentFuelAmount;
         
          public FuelTank(float i_MaximumFuelCapacity, eFuelType i_FuelType)
          {
               r_MaximumFuelCapacity = i_MaximumFuelCapacity;
               r_FuelType = i_FuelType;
               m_CurrentFuelAmount = 0;
          }

          public enum eFuelType
          {
               Octan95,
               Octan96,
               Octan98,
               Soler,
          }

          public float CurrentFuelAmmount
          {
               get { return m_CurrentFuelAmount; }
               set { m_CurrentFuelAmount = value; }
          }

          public float MaximumFuelCapacity
          {
               get { return r_MaximumFuelCapacity; }
          }

          public eFuelType FuelType
          {
               get { return r_FuelType; }
          }
     }
}
