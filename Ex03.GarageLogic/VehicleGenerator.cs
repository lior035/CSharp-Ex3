namespace Ex03.GarageLogic
{
     using System;
     using System.Collections.Generic;
     using System.Text;

     public static class VehicleGenerator
     {
          private const int k_PlaceHolderForVehicleNumber = 0;
          private const int k_PlaceHolderForVehicleModel = 1;
          private const int k_PlaceHolderForWheelManufacturer = 2;
          private const int k_PlaceHolderForTruckVolumeCapacity = 3;
          private const int k_PlaceHolderForMotorcycleLicenseType = 3;
          private const int k_PlaceHolderForCarColor = 3;
          private const int k_PlaceHolderForTruckContainsDangerousCargo = 4;
          private const int k_PlaceHolderForMotorcycleEngineCapacity = 4;
          private const int k_PlaceHolderForCarDoorsNumber = 4;
          private const int k_PlaceHolderForTruckMaxAllowedCargoWeight = 5;
          private const int k_MotorcycleNumberOfWheels = 2;
          private const int k_CarNumberOfWheels = 4;
          private const int k_TruckNumberOfWheels = 12;
          private const float k_MotorcycleMaxAirPressure = 32;
          private const float k_CarMaxAirPressure = 30;
          private const float k_TruckMaxAirPressure = 27;
          private const FuelTank.eFuelType k_GasMotorcycleFuelType = FuelTank.eFuelType.Octan98;
          private const FuelTank.eFuelType k_GasCarFuelType = FuelTank.eFuelType.Octan95;
          private const FuelTank.eFuelType k_GasTruckFuelType = FuelTank.eFuelType.Soler;
          private const float k_GasMotorcycleMaxFuelTankCapacity = 7;
          private const float k_GasCarMaxFuelTankCapacity = 41;
          private const float k_GasTruckMaxFuelTankCapacity = 210;
          private const float k_ElectricMotorcycleMaxBatteryTimeInHours = 1.7f;
          private const float k_ElectricCarMaxBatteryTimeInHours = 2.2f;

          public enum eVehicleToGenerate
          {
               GasTruck,
               GasCar,
               ElectricCar,
               GasMotorcycle,
               ElectricMotorcycle,               
          }
       
          public static Vehicle GenerateNewVehicle(eVehicleToGenerate i_VehicleToGenerate, List<string> i_NewVehicleData)
          {
               Vehicle newVehicle = null;
               Motorcycle.eMotorcycleLicenseType motorcycleLicenseType = 0;
               Car.eCarColorOptions carColor = 0;
               Car.eNumberOfDoors carNumberOfDoors = 0;

               if (i_VehicleToGenerate == eVehicleToGenerate.ElectricCar || i_VehicleToGenerate == eVehicleToGenerate.GasCar)
               {
                    carColor = (Car.eCarColorOptions)System.Enum.Parse(typeof(GasCar.eCarColorOptions), i_NewVehicleData[k_PlaceHolderForCarColor]);
                    carNumberOfDoors = (Car.eNumberOfDoors)System.Enum.Parse(typeof(Car.eNumberOfDoors), i_NewVehicleData[k_PlaceHolderForCarDoorsNumber]);
               }

               if (i_VehicleToGenerate == eVehicleToGenerate.ElectricMotorcycle || i_VehicleToGenerate == eVehicleToGenerate.GasMotorcycle)
               {
                    motorcycleLicenseType = (Motorcycle.eMotorcycleLicenseType)System.Enum.Parse(typeof(Motorcycle.eMotorcycleLicenseType), i_NewVehicleData[k_PlaceHolderForMotorcycleLicenseType]);
               }

               switch (i_VehicleToGenerate)
               {
                    case eVehicleToGenerate.GasTruck:
                         Truck.eTruckCargo containsDangerousCargo = (Truck.eTruckCargo)System.Enum.Parse(typeof(Truck.eTruckCargo), i_NewVehicleData[k_PlaceHolderForTruckContainsDangerousCargo]);
                         newVehicle = new GasTruck(k_GasTruckMaxFuelTankCapacity, containsDangerousCargo, k_GasTruckFuelType, float.Parse(i_NewVehicleData[k_PlaceHolderForTruckVolumeCapacity]),
                                                   float.Parse(i_NewVehicleData[k_PlaceHolderForTruckMaxAllowedCargoWeight]), i_NewVehicleData[k_PlaceHolderForVehicleNumber], i_NewVehicleData[k_PlaceHolderForVehicleModel],
                                                   k_TruckNumberOfWheels, i_NewVehicleData[k_PlaceHolderForWheelManufacturer], k_TruckMaxAirPressure);
                         break;
                    case eVehicleToGenerate.GasMotorcycle:                                                  
                         newVehicle = new GasMotorcycle(k_GasMotorcycleMaxFuelTankCapacity, k_GasMotorcycleFuelType, motorcycleLicenseType,
                                                        int.Parse(i_NewVehicleData[k_PlaceHolderForMotorcycleEngineCapacity]), i_NewVehicleData[k_PlaceHolderForVehicleNumber],
                                                        i_NewVehicleData[k_PlaceHolderForVehicleModel], k_MotorcycleNumberOfWheels, i_NewVehicleData[k_PlaceHolderForWheelManufacturer],
                                                        k_MotorcycleMaxAirPressure);
                         break;
                    case eVehicleToGenerate.ElectricMotorcycle:                         
                         newVehicle = new ElectricMotorcycle(k_ElectricMotorcycleMaxBatteryTimeInHours, motorcycleLicenseType, int.Parse(i_NewVehicleData[k_PlaceHolderForMotorcycleEngineCapacity]),
                                                             i_NewVehicleData[k_PlaceHolderForVehicleNumber], i_NewVehicleData[k_PlaceHolderForVehicleModel], k_MotorcycleNumberOfWheels, i_NewVehicleData[k_PlaceHolderForWheelManufacturer],
                                                             k_MotorcycleMaxAirPressure);
                         break;
                    case eVehicleToGenerate.GasCar:
                         newVehicle = new GasCar(k_GasCarMaxFuelTankCapacity, k_GasCarFuelType, carColor, carNumberOfDoors, i_NewVehicleData[k_PlaceHolderForVehicleNumber],
                                                 i_NewVehicleData[k_PlaceHolderForVehicleModel], k_CarNumberOfWheels,
                                                 i_NewVehicleData[k_PlaceHolderForWheelManufacturer], k_CarMaxAirPressure);
                         break;
                    case eVehicleToGenerate.ElectricCar:
                         newVehicle = new ElectricCar(k_ElectricCarMaxBatteryTimeInHours, carColor, carNumberOfDoors, i_NewVehicleData[k_PlaceHolderForVehicleNumber],
                                                      i_NewVehicleData[k_PlaceHolderForVehicleModel], k_CarNumberOfWheels,
                                                      i_NewVehicleData[k_PlaceHolderForWheelManufacturer], k_CarMaxAirPressure);
                         break;
                    default:
                         break;
               }

               return newVehicle;
          }

          public static string SpaceEnumString(string currentEnumString)
          {
               StringBuilder spacedEnumString = new StringBuilder();

               spacedEnumString.Append(currentEnumString[0]);
               foreach (char currentCharInEnumString in currentEnumString.Substring(1))
               {
                    if (char.IsUpper(currentCharInEnumString))
                    {
                         spacedEnumString.Append(' ').Append(char.ToLower(currentCharInEnumString));
                    }
                    else
                    {
                         spacedEnumString.Append(currentCharInEnumString);
                    }
               }

               return spacedEnumString.ToString();
          }
     }
}
