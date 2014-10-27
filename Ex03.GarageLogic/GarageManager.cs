using System;
using System.Collections;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{        
     public class GarageManager
     {
          private Dictionary<string, VehicleInTreatmentDetails> m_VehiclesInGarageByNumber;

          public GarageManager()
          {
               m_VehiclesInGarageByNumber = new Dictionary<string, VehicleInTreatmentDetails>();               
          }
          
          public VehicleInTreatmentDetails GetVehicleInTreatmentDetailsByExistingVehicleNumber(string i_ExistingVehicleNumber)
          {
               return m_VehiclesInGarageByNumber[i_ExistingVehicleNumber];
          }

          public enum eVehicleInGarageStatus
          {
               Fixed,
               InRepair,
               PaidForRepairment,
          }

          public void InsertVehicleToGarage(Vehicle i_VehicleToInsert, string i_VehicleOwnerName, string i_VehicleOwnerPhoneNumber)
          {
               VehicleInTreatmentDetails vehicleToInsertDetails;               
               vehicleToInsertDetails = new VehicleInTreatmentDetails(i_VehicleToInsert, i_VehicleOwnerName, i_VehicleOwnerPhoneNumber);               
               m_VehiclesInGarageByNumber.Add(i_VehicleToInsert.VehicleNumber, vehicleToInsertDetails);               
          }

          public List<string> GenerateVehiclesNumbersListByFilterType(Ex03.GarageLogic.GarageManager.eVehicleInGarageStatus i_FilterType)
          {
               List<string> vehiclesNumbersListByFilter = new List<string>();

               foreach (KeyValuePair<string, VehicleInTreatmentDetails> currentVehicleInTreatment in m_VehiclesInGarageByNumber)
               {
                    if (currentVehicleInTreatment.Value.VehicleInTreatmentStatus == i_FilterType)
                    {
                         vehiclesNumbersListByFilter.Add(currentVehicleInTreatment.Key);
                    }
               }

               return vehiclesNumbersListByFilter;
          }

          public bool IsVehicleExistsInGarage(string i_VehicleNumber)
          {
               bool vehicleAlreadyExistsInGarage = m_VehiclesInGarageByNumber.ContainsKey(i_VehicleNumber);

               return vehicleAlreadyExistsInGarage;
          }

          public void PumpVehicleWheelsToMaximum(string i_ExistingVehicleNumber)
          {
               Vehicle vehicleToPumpWheels = m_VehiclesInGarageByNumber[i_ExistingVehicleNumber].TheVehicleInTreatment;
               vehicleToPumpWheels.PumpWheels(vehicleToPumpWheels.GetWheelMaxAirPressure());
          }

          public void FuelGasVehicle(string i_ExistingVehicleNumber, FuelTank.eFuelType i_FuelType, float i_AmountOfLitersToFuel)
          {
               VehicleInTreatmentDetails vehicleToFuel;
               m_VehiclesInGarageByNumber.TryGetValue(i_ExistingVehicleNumber, out vehicleToFuel);
               if (vehicleToFuel.TheVehicleInTreatment.EngineType == Vehicle.eVehicleEngineType.ElectricEngine)
               {
                    throw new ArgumentException(string.Format("The vehicle with number {0} is electric, cannot perform fuel action", i_ExistingVehicleNumber));                    
               }
               else
               {                   
                    if (vehicleToFuel.TheVehicleInTreatment is GasCar)
                    {
                         (vehicleToFuel.TheVehicleInTreatment as GasCar).FuelGasCar(i_FuelType, i_AmountOfLitersToFuel);
                    }

                    if (vehicleToFuel.TheVehicleInTreatment is GasMotorcycle)
                    {
                         (vehicleToFuel.TheVehicleInTreatment as GasMotorcycle).FuelGasMotorcycle(i_FuelType, i_AmountOfLitersToFuel);
                    }

                    if (vehicleToFuel.TheVehicleInTreatment is GasTruck)
                    {
                         (vehicleToFuel.TheVehicleInTreatment as GasTruck).FuelGasTruck(i_FuelType, i_AmountOfLitersToFuel);
                    }
               }               
          }

          public void ChargeElectricVehicle(string i_ExistingVehicleNumber, float i_AmountOfMinutesToCharge)
          {
               VehicleInTreatmentDetails vehicleToCharge;
               m_VehiclesInGarageByNumber.TryGetValue(i_ExistingVehicleNumber, out vehicleToCharge);
               if (vehicleToCharge.TheVehicleInTreatment.EngineType == Vehicle.eVehicleEngineType.GasEngine)
               {
                    throw new ArgumentException(string.Format("The vehicle with number {0} works on gas, cannot perform charge action", i_ExistingVehicleNumber));
               }
               else
               {                    
                         if (vehicleToCharge.TheVehicleInTreatment is ElectricCar)
                         {
                              (vehicleToCharge.TheVehicleInTreatment as ElectricCar).ChargeBattery(i_AmountOfMinutesToCharge);
                         }

                         if (vehicleToCharge.TheVehicleInTreatment is ElectricMotorcycle)
                         {
                              (vehicleToCharge.TheVehicleInTreatment as ElectricMotorcycle).ChargeBattery(i_AmountOfMinutesToCharge);
                         }                                       
               }
          }
     } 
}
