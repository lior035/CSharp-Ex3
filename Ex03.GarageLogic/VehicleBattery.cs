namespace Ex03.GarageLogic
{
     using System;
     using System.Collections.Generic;
     using System.Text;

     public class VehicleBattery
     {
          private readonly float r_MaxBatteryTimeInHours;
          private float m_RemainingBatteryTimeInHours;
          
          public VehicleBattery(float i_MaximumBatteryTime)
          {
               m_RemainingBatteryTimeInHours = 0;
               r_MaxBatteryTimeInHours = i_MaximumBatteryTime;
          }

          public float RemainingBatteryTime
          {
               get { return m_RemainingBatteryTimeInHours; }
               set { m_RemainingBatteryTimeInHours = value; }
          }

          public float MaximumBatteryTime
          {
               get { return r_MaxBatteryTimeInHours; } 
          }
     }
}
