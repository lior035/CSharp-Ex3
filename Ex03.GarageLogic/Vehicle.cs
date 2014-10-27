namespace Ex03.GarageLogic
{
     using System;
     using System.Collections.Generic;
     using System.Text;

     public class Vehicle
     {
          public const int k_LengthOfValidVehicleNumber = 7;
          private readonly string r_VehicleNumber;
          private readonly string r_VehicleModel;
          private List<Wheel> m_VehicleWheels;
          private float m_RemainingEnergyPercentage;
          protected eVehicleEngineType m_EngineType;

          public Vehicle(string i_VehicleNumber, string i_VehicleModel, int i_NumberOfWheels, string i_WheelManufacturerName, float i_MaxAllowedAirPressure)
          {
               m_VehicleWheels = new List<Wheel>(i_NumberOfWheels);
               for (int i = 0; i < i_NumberOfWheels; i++)
               {
                    m_VehicleWheels.Insert(i, new Wheel(i_WheelManufacturerName, i_MaxAllowedAirPressure));
               }

               r_VehicleNumber = i_VehicleNumber;
               r_VehicleModel = i_VehicleModel;
               m_RemainingEnergyPercentage = 0;
          }

          public override string ToString()
          {
               StringBuilder vehicleDetails = new StringBuilder();
               vehicleDetails.Append(string.Format("Vehicle number: {0}", VehicleNumber)).AppendLine()
                                    .Append(string.Format("Vehicle model: {0}", VehicleModel)).AppendLine()
                                    .Append(string.Format("Vehicle's remaining energy: {0}", string.Format("{0:P}", m_RemainingEnergyPercentage))).AppendLine()
                                    .Append(m_VehicleWheels[0].ToString());

               return vehicleDetails.ToString();
          }

          public eVehicleEngineType EngineType
          {
               get { return m_EngineType; }
          }

          public float GetWheelMaxAirPressure()
          {
               return m_VehicleWheels[0].MaxAllowedAirPressure;
          }

          public void PumpWheels(float i_AmountOfAirToPump)
          {
               float vehicleWheelMaxAirPressureAllowed = m_VehicleWheels[0].MaxAllowedAirPressure;
               if (i_AmountOfAirToPump < Wheel.k_MinimumAmountOfAirToPumpWheel)
               {
                    throw new ValueOutOfRangeException(vehicleWheelMaxAirPressureAllowed, Wheel.k_MinimumAmountOfAirToPumpWheel, string.Format("Error: Pressure to pump must be over {0}", Wheel.k_MinimumAmountOfAirToPumpWheel));
               }

               if (i_AmountOfAirToPump > vehicleWheelMaxAirPressureAllowed)
               {
                    throw new ValueOutOfRangeException(vehicleWheelMaxAirPressureAllowed, Wheel.k_MinimumAmountOfAirToPumpWheel, string.Format("Error: Pressure to pump must be under {0}", vehicleWheelMaxAirPressureAllowed));
               }
               else
               {
                    foreach (Wheel currentWheelToPump in m_VehicleWheels)
                    {
                         currentWheelToPump.PumpWheel(i_AmountOfAirToPump);
                    }
               }
          }

          public enum eVehicleEngineType
          {
               GasEngine,
               ElectricEngine,
          }

          public string VehicleNumber
          {
               get { return r_VehicleNumber; }
          }

          public string VehicleModel
          {
               get { return r_VehicleModel; }
          }
   
          public float RemainPercentageEnergy
          {
               get { return m_RemainingEnergyPercentage; }
               set { m_RemainingEnergyPercentage = value; }
          }
     }
}
