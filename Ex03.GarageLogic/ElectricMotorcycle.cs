﻿namespace Ex03.GarageLogic
{
     using System;
     using System.Collections.Generic;
     using System.Text;

     public class ElectricMotorcycle : Motorcycle
     {
          private VehicleBattery m_Battery;

          public ElectricMotorcycle(float i_MaxBatteryTime, Motorcycle.eMotorcycleLicenseType i_MotorcycleLicenseType, int i_EngineCapacity,
                                    string i_VehicleNumber, string i_VehicleModel, int i_NumberOfWheels, string i_WheelManufacturerName, float i_MaxAllowedAirPressure)
                                   : base(i_MotorcycleLicenseType, i_EngineCapacity, i_VehicleNumber, i_VehicleModel, i_NumberOfWheels, i_WheelManufacturerName, i_MaxAllowedAirPressure)
          {
               this.m_Battery = new VehicleBattery(i_MaxBatteryTime);
               this.m_EngineType = eVehicleEngineType.ElectricEngine;               
          }

          public override string ToString()
          {
               StringBuilder electricMotorcycleDetails = new StringBuilder();
               electricMotorcycleDetails.Append(base.ToString()).AppendLine().Append(string.Format("Engine type: {0}", VehicleGenerator.SpaceEnumString(m_EngineType.ToString()))).AppendLine()
                                     .Append(string.Format("Remaining battery time (in hours): {0}", this.m_Battery.RemainingBatteryTime)).AppendLine()
                                     .Append(string.Format("Maximum battery charge time: {0}", this.m_Battery.MaximumBatteryTime)).AppendLine();

               return electricMotorcycleDetails.ToString();
          }

          public void ChargeBattery(float i_TimeToChargeBatteryInHours)
          {
               if (this.m_Battery.RemainingBatteryTime + i_TimeToChargeBatteryInHours > this.m_Battery.MaximumBatteryTime)
               {
                    throw new ValueOutOfRangeException(this.m_Battery.MaximumBatteryTime, 0, string.Format("Error: The amount of time to charge exceeds the maximum time allowed, the maximum amount to charge is {0}", this.m_Battery.MaximumBatteryTime - this.m_Battery.RemainingBatteryTime));
               }
               else
               {
                    this.m_Battery.RemainingBatteryTime += i_TimeToChargeBatteryInHours;
                    this.RemainPercentageEnergy = this.m_Battery.RemainingBatteryTime / this.m_Battery.MaximumBatteryTime;
               }
          }
     }
}
