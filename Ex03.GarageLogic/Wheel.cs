// -----------------------------------------------------------------------
// <copyright file="Wheel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Ex03.GarageLogic
{
     using System;
     using System.Collections.Generic;
     using System.Text;     

     public class Wheel
     {
          private readonly float r_MaxAllowedAirPressure;
          private readonly string r_ManufacturerName;       
          public const float k_MaximumAmountOfAirToPumpWheel = 32;
          public const float k_MinimumAmountOfAirToPumpWheel = 0; 
          private float m_CurrentAirPressure;          
          
          public override string ToString()
          {
               StringBuilder wheelDetails = new StringBuilder();
               wheelDetails.Append(string.Format("Wheel's manufacturer's name: {0}", r_ManufacturerName)).AppendLine()
                                    .Append(string.Format("Wheel's max air pressure: {0}", r_MaxAllowedAirPressure)).AppendLine();                                    
               
               return wheelDetails.ToString();
          }

          public Wheel(string i_ManufacturerName, float i_MaxAllowedAirPressure)
          {
               r_ManufacturerName = i_ManufacturerName;
               r_MaxAllowedAirPressure = i_MaxAllowedAirPressure;
               m_CurrentAirPressure = 0; 
          }
      
          public string ManufacturerName
          {
               get { return r_ManufacturerName; }
          }

          public float MaxAllowedAirPressure
          {
               get { return r_MaxAllowedAirPressure; }
          }

          public float CurrentAirPressure
          {
               get { return m_CurrentAirPressure; }
          }

          public void PumpWheel(float i_AmountOfAirToPump)
          {                                                                         
               m_CurrentAirPressure = i_AmountOfAirToPump;               
          }      
     }
}
