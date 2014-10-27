namespace Ex03.GarageLogic
{
     using System;
     using System.Collections.Generic;     
     using System.Text;

     public class GasCar : Car
     {          
          private FuelTank m_FuelTank;          

          public GasCar(float i_MaxFuelCapacity, FuelTank.eFuelType i_FuelType, Car.eCarColorOptions i_CarColor,
                        Car.eNumberOfDoors i_NumberOfDoors, string i_VehicleNumber, string i_VehicleModel, int i_NumberOfWheels, string i_WheelManufacturerName, float i_MaxAllowedAirPressure)
               : base(i_CarColor, i_NumberOfDoors, i_VehicleNumber, i_VehicleModel, i_NumberOfWheels, i_WheelManufacturerName, i_MaxAllowedAirPressure)
          {
               this.m_FuelTank = new FuelTank(i_MaxFuelCapacity, i_FuelType);               
               this.m_EngineType = Vehicle.eVehicleEngineType.GasEngine;
          }

          public override string ToString()
          {
               StringBuilder gasCarDetails = new StringBuilder();
               gasCarDetails.Append(base.ToString()).AppendLine().Append(string.Format("Engine type: {0}", VehicleGenerator.SpaceEnumString(m_EngineType.ToString()))).AppendLine()
                                     .Append(string.Format("Cars's gas type: {0}", this.m_FuelTank.FuelType.ToString())).AppendLine()
                                     .Append(string.Format("Remaining fuel amount: {0}", this.m_FuelTank.CurrentFuelAmmount)).AppendLine()
                                     .Append(string.Format("Maximum fuel tank capacity: {0}", this.m_FuelTank.MaximumFuelCapacity)).AppendLine();

               return gasCarDetails.ToString();
          }
    
          /// <summary>
          /// This function tries to fuel gasCar
          /// </summary>                             
          /// <param name="i_FuelType"><remarks>throw ArgumentException if fuel type does not match the enum FuelTank.eFuelType.Octan95</remarks></param>
          /// <param name="i_AmountOfLitersToFuel">throw ValueOutOfRangeException if current amount of fuel + fuel input amount exceeds maximum fuel tank capacity</param>
          public void FuelGasCar(FuelTank.eFuelType i_FuelType, float i_AmountOfLitersToFuel)
          {              
               if (i_FuelType != this.m_FuelTank.FuelType)
               {
                    throw new ArgumentException(string.Format("Error: The car's fuel type is {0}, and you chose {1}", this.m_FuelTank.FuelType.ToString(), i_FuelType.ToString()));
               }
               else if (this.m_FuelTank.CurrentFuelAmmount + i_AmountOfLitersToFuel > this.m_FuelTank.MaximumFuelCapacity)
               {
                    string errorMsg = string.Format("Error: The amount to fuel exceeds the maximum capacity, the maximum amount to fuel is {0}", this.m_FuelTank.MaximumFuelCapacity - this.m_FuelTank.CurrentFuelAmmount);
                    throw new ValueOutOfRangeException(this.m_FuelTank.MaximumFuelCapacity, 0, errorMsg);
               }
               else
               {
                    this.m_FuelTank.CurrentFuelAmmount += i_AmountOfLitersToFuel;
                    this.RemainPercentageEnergy = this.m_FuelTank.CurrentFuelAmmount / this.m_FuelTank.MaximumFuelCapacity;
               }
          }
     }
}
