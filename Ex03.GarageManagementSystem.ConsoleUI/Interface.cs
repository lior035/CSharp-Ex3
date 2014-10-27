using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

public static class Interface
{
     public enum eMainMenuEnum
     {
          InsertVehicleToGarage,
          PresentTheNumbersOfVehiclesInGarageByStatus,
          ChangeExistingVehicleStatus,
          PumpVehicleWheelsToMaximum,
          FuelGasVehicle,
          ChargeElectricVehicle,
          PresentFullVehicleDetails,
          ExitMenu,
     }
  
     public static eMainMenuEnum ShowMenuAndGetUserInput()
     {
         return (eMainMenuEnum)System.Enum.Parse(typeof(eMainMenuEnum), getEnumStringFromEnumValues(typeof(eMainMenuEnum)));
     }

     public static void PerformUserSelection(eMainMenuEnum i_UserOptionSelection, Ex03.GarageLogic.GarageManager i_GarageManager)
     {          
          string existingVehicleNumber = null;

          if (i_UserOptionSelection >= eMainMenuEnum.ChangeExistingVehicleStatus && i_UserOptionSelection <= eMainMenuEnum.PresentFullVehicleDetails)
          {
               existingVehicleNumber = auxGetExistingVehicleNumber(i_GarageManager);
          }

          switch (i_UserOptionSelection)
          {
               case eMainMenuEnum.InsertVehicleToGarage:
                    insertVehicleToGarage(existingVehicleNumber, i_GarageManager);
                    break;
               case eMainMenuEnum.PresentTheNumbersOfVehiclesInGarageByStatus:
                    presentTheNumbersofVehiclesInGarageByStatus(i_GarageManager);
                    break;
               case eMainMenuEnum.ChangeExistingVehicleStatus:
                    changeExistingVehicleStatus(existingVehicleNumber, i_GarageManager);
                    break;
               case eMainMenuEnum.PumpVehicleWheelsToMaximum:
                    pumpVehicleWheelsToMaximum(existingVehicleNumber, i_GarageManager);
                    break;
               case eMainMenuEnum.FuelGasVehicle:
                    fuelGasVehicle(existingVehicleNumber, i_GarageManager); 
                    break;
               case eMainMenuEnum.ChargeElectricVehicle:
                    chargeElectricalVehicle(existingVehicleNumber, i_GarageManager);
                    break;
               case eMainMenuEnum.PresentFullVehicleDetails:
                    presentFullVehicleDetails(existingVehicleNumber, i_GarageManager);
                    break;
               case eMainMenuEnum.ExitMenu:
                    exitMenu();
                    break;
               default:
                    break;
          }          
     }

     private static void exitMenu()
     {
          Console.Clear();
          System.Console.WriteLine("Thank you for using the garage manager!");
          System.Console.WriteLine("Please enter any key to exit...");
          System.Console.ReadLine();
     }
          
     private static void insertVehicleToGarage(string existingVehicleNumber, Ex03.GarageLogic.GarageManager i_GarageManager)
     {
          Ex03.GarageLogic.VehicleGenerator.eVehicleToGenerate vehicleToGenerateType;
          string validVehicleNumber = auxGetValidVehicleNumber();
          bool isVehicleAlreadyExistsInGarage = i_GarageManager.IsVehicleExistsInGarage(validVehicleNumber);
          if (!isVehicleAlreadyExistsInGarage)
          {
               string vehicleOwnerName, vehicleOwnerPhoneNumber;
               auxGetValidVehicleOwnerAndHisPhoneNumber(out vehicleOwnerName, out vehicleOwnerPhoneNumber);
               List<string> vehicleToInsertData = auxGetDataOfVehicleToInsert(out vehicleToGenerateType);
               vehicleToInsertData.Insert(0, validVehicleNumber);
               Ex03.GarageLogic.Vehicle vehicleToInsert = Ex03.GarageLogic.VehicleGenerator.GenerateNewVehicle(vehicleToGenerateType, vehicleToInsertData);               
               i_GarageManager.InsertVehicleToGarage(vehicleToInsert, vehicleOwnerName, vehicleOwnerPhoneNumber);
               System.Console.WriteLine(Environment.NewLine + "The vehicle was successfuly added to the garage..." + Environment.NewLine);
          }
          else
          {
               i_GarageManager.GetVehicleInTreatmentDetailsByExistingVehicleNumber(existingVehicleNumber).VehicleInTreatmentStatus = Ex03.GarageLogic.GarageManager.eVehicleInGarageStatus.InRepair;
               System.Console.WriteLine("Vehicle already exists in garage, status was changed to 'In Repair'");
          }        
     }

     private static void presentTheNumbersofVehiclesInGarageByStatus(Ex03.GarageLogic.GarageManager i_GarageManager)
     {
          string stringStatusFilterType = getEnumStringFromEnumValues(typeof(Ex03.GarageLogic.GarageManager.eVehicleInGarageStatus));
          Ex03.GarageLogic.GarageManager.eVehicleInGarageStatus vehicleStatusFilterType =
               (Ex03.GarageLogic.GarageManager.eVehicleInGarageStatus)System.Enum.Parse(typeof(Ex03.GarageLogic.GarageManager.eVehicleInGarageStatus), stringStatusFilterType);
          List<string> arrayOfVehiclesNumbersByFilter = i_GarageManager.GenerateVehiclesNumbersListByFilterType(vehicleStatusFilterType);
          auxPresentTheNumbersOfTheVehiclesinVehicleList(arrayOfVehiclesNumbersByFilter, vehicleStatusFilterType);
     }
     
     private static void changeExistingVehicleStatus(string existingVehicleNumber, Ex03.GarageLogic.GarageManager i_GarageManager)
     {
          Ex03.GarageLogic.GarageManager.eVehicleInGarageStatus validVehicleStatus;
          string stringValidVehicleStatus = getEnumStringFromEnumValues(typeof(Ex03.GarageLogic.GarageManager.eVehicleInGarageStatus));
          validVehicleStatus = (Ex03.GarageLogic.GarageManager.eVehicleInGarageStatus)System.Enum.Parse(typeof(Ex03.GarageLogic.GarageManager.eVehicleInGarageStatus), stringValidVehicleStatus);
          i_GarageManager.GetVehicleInTreatmentDetailsByExistingVehicleNumber(existingVehicleNumber).VehicleInTreatmentStatus = validVehicleStatus;
     }

     private static void pumpVehicleWheelsToMaximum(string i_ExistingVehicleNumber, Ex03.GarageLogic.GarageManager i_GarageManager)
     {
          do
          {
               try
               {
                    i_GarageManager.PumpVehicleWheelsToMaximum(i_ExistingVehicleNumber);
                    System.Console.WriteLine("Succes: wheels' air pressure is pumped to maximum");
                    break;
               }
               catch (Ex03.GarageLogic.ValueOutOfRangeException outOfRangeException)
               {
                    System.Console.WriteLine(outOfRangeException.Message);
               }
          }
          while (true);
     }

     /// <summary>
     /// This function tries to fuel a vehicle of input number and catches 3 optional exceptions:
     /// 1. <remarks>ArgumentException</remarks> in case of trying to fuel electric car
     /// 2. <remarks>ArgumentException</remarks> in case of trying to fuel car with mismatching fuel type
     /// 3. <remarks>ValueOutOfRange</remarks> Exception in case of the action of fueling will exceed the maximum fuel tank's capacity
     /// </summary>
     /// <param name="i_ExistingVehicleNumber"></param>
     /// <param name="i_GarageManager"></param>
     private static void fuelGasVehicle(string i_ExistingVehicleNumber, Ex03.GarageLogic.GarageManager i_GarageManager)
     {
          Ex03.GarageLogic.FuelTank.eFuelType fuelType = auxGetFuelType();
          System.Console.WriteLine("Please enter amount of fuel (in Liters) to fuel");
          float amountOfLitersToFuel = getPositiveValidFloat();
          System.Console.WriteLine();
          try
          {
               i_GarageManager.FuelGasVehicle(i_ExistingVehicleNumber, fuelType, amountOfLitersToFuel);
               System.Console.WriteLine("Succes: vehicle was fueled succesfully with {0} liters..." + Environment.NewLine, amountOfLitersToFuel);
          }
          catch (Exception ex)
          {
               System.Console.WriteLine(ex.Message);
          }
     }

     /// <summary>
     /// This function tries to fuel a vehicle of input number and catches 3 optional exceptions:
     /// 1. <remarks>ArgumentException</remarks> in case of trying to charge a car that works on gas
     /// 2. <remarks>ValueOutOfRangeException</remarks> in case the charging action will exceed the maximum battery time charge allowed
     /// </summary>
     /// <param name="i_ExistingVehicleNumber"></param>
     /// <param name="i_GarageManager"></param>
     private static void chargeElectricalVehicle(string i_ExistingVehicleNumber, Ex03.GarageLogic.GarageManager i_GarageManager)
     {
          System.Console.WriteLine("Please enter time to charge in hours");
          float amountOfMinutesToCharge = getPositiveValidFloat();
          System.Console.WriteLine();
          try
          {
               i_GarageManager.ChargeElectricVehicle(i_ExistingVehicleNumber, amountOfMinutesToCharge);
               System.Console.WriteLine("Success: the vehicle battery was charged in {0} hours", amountOfMinutesToCharge);
          }
          catch (Exception ex)
          {
               System.Console.WriteLine(ex.Message);
          }
     }

     private static void presentFullVehicleDetails(string i_ExistingVehicleNumber, Ex03.GarageLogic.GarageManager i_GarageManager)
     {
          Ex03.GarageLogic.VehicleInTreatmentDetails vehicleToPresentDetails = i_GarageManager.GetVehicleInTreatmentDetailsByExistingVehicleNumber(i_ExistingVehicleNumber);                    
          System.Console.WriteLine(vehicleToPresentDetails);
     }
             
     private static void auxGetValidVehicleOwnerAndHisPhoneNumber(out string o_OwnerName, out string o_OwnerPhoneNumber)
     {
          bool validOwnerName;
          bool validOwnerPhoneNumber;
          System.Console.WriteLine("Please enter vehicle owner's name");
          do
          {
               o_OwnerName = System.Console.ReadLine();
               validOwnerName = true;
               foreach (char currentCharInInputString in o_OwnerName)
               {
                    if (!char.IsLetter(currentCharInInputString))
                    {
                         System.Console.WriteLine("Please enter a valid vehicle owner's name");
                         validOwnerName = false;
                         break;
                    }
               }
          }
          while (!validOwnerName);

          System.Console.WriteLine("Please enter vehicle owner's phone number");
          do
          {
               o_OwnerPhoneNumber = System.Console.ReadLine();
               validOwnerPhoneNumber = true;
               foreach (char currentCharInInputString in o_OwnerPhoneNumber)
               {
                    if (!char.IsDigit(currentCharInInputString))
                    {
                         System.Console.WriteLine("Please enter a valid vehicle owner's phone number");
                         validOwnerPhoneNumber = false;
                         break;
                    }
               }
          }
          while (!validOwnerPhoneNumber);
     }

     private static List<string> auxGetDataOfVehicleToInsert(out Ex03.GarageLogic.VehicleGenerator.eVehicleToGenerate o_VehicleToGenerateType)
     {
          List<string> dataOfVehicleToInsert = new List<string>();
          string stringVehicleToGenerate = getEnumStringFromEnumValues(typeof(Ex03.GarageLogic.VehicleGenerator.eVehicleToGenerate));
          o_VehicleToGenerateType = (Ex03.GarageLogic.VehicleGenerator.eVehicleToGenerate)System.Enum.Parse(typeof(Ex03.GarageLogic.VehicleGenerator.eVehicleToGenerate), stringVehicleToGenerate);
          auxGetGeneralVehicleInformation(dataOfVehicleToInsert);

          switch (o_VehicleToGenerateType)
          {
               case Ex03.GarageLogic.VehicleGenerator.eVehicleToGenerate.GasTruck:
                    getTruckData(dataOfVehicleToInsert);
                    break;
               case Ex03.GarageLogic.VehicleGenerator.eVehicleToGenerate.GasCar:
                    getGasCarData(dataOfVehicleToInsert);
                    break;
               case Ex03.GarageLogic.VehicleGenerator.eVehicleToGenerate.ElectricCar:
                    getElectricCarData(dataOfVehicleToInsert);
                    break;
               case Ex03.GarageLogic.VehicleGenerator.eVehicleToGenerate.GasMotorcycle:
                    getGasMotorcycleData(dataOfVehicleToInsert);
                    break;
               case Ex03.GarageLogic.VehicleGenerator.eVehicleToGenerate.ElectricMotorcycle:
                    getElectricMotorcycleData(dataOfVehicleToInsert);
                    break;
               default:
                    break;
          }

          return dataOfVehicleToInsert;
     }
     
     /// <summary>
     /// This function asks user for positive float input and throws and catches FormatException in case of illegal float input
     /// and makes sure that the legal float input is positive
     /// </summary>
     /// <returns>This function returns a valid positive float</returns>
     private static float getPositiveValidFloat()
     {
          float positiveFloatValueToGenerate;
          do
          {
               try
               {
                    if (!float.TryParse(System.Console.ReadLine(), out positiveFloatValueToGenerate))
                    {
                         throw new FormatException("Exception: expecting a legal float");
                    }

                    if (positiveFloatValueToGenerate < 0)
                    {
                         throw new Ex03.GarageLogic.ValueOutOfRangeException(float.MaxValue, 0, "Error: expecting positive float");
                    }
                    else
                    {
                         break;
                    }                
               }
               catch (Exception fe)
               {
                    System.Console.WriteLine(fe.Message);
               }
          }
          while (true);

          return positiveFloatValueToGenerate;
     }

     private static int getPositiveValidInt()
     {
          int positiveIntValueToGenerate;
          do
          {
               try
               {
                    if (!int.TryParse(System.Console.ReadLine(), out positiveIntValueToGenerate))
                    {
                         throw new FormatException("Exception: expecting a legal integer");
                    }

                    if (positiveIntValueToGenerate < 0)
                    {
                         throw new Ex03.GarageLogic.ValueOutOfRangeException(int.MaxValue, 0, "Error: expecting positive int");
                    }
                    else
                    {
                         break;
                    }                
               }
               catch (Exception fe)
               {
                    System.Console.WriteLine(fe.Message);
               }
          }
          while (true);

          return positiveIntValueToGenerate;
     }
     
     private static void getTruckData(List<string> i_DataOfVehicleToInsert)
     {      
          i_DataOfVehicleToInsert.Add(auxGetTruckDataGetVolumeCapacity());                                    
          i_DataOfVehicleToInsert.Add(getEnumStringFromEnumValues(typeof(Ex03.GarageLogic.Truck.eTruckCargo)));          
          i_DataOfVehicleToInsert.Add(auxGetTruckDataGetMaximumAllowedCargoWeight());      
     }
  
     private static string auxGetTruckDataGetVolumeCapacity()
     {
          System.Console.WriteLine("Please enter truck's volume capacity");
          float truckVolumeCapacity = getPositiveValidFloat();
          System.Console.WriteLine();
          
          return truckVolumeCapacity.ToString();
     }
     
     private static string auxGetTruckDataGetMaximumAllowedCargoWeight()
     {
          System.Console.WriteLine("Please enter truck's maximum allowed cargo weight");
          float maximumAllowedCargoWeight = getPositiveValidFloat();
          System.Console.WriteLine();         
          return maximumAllowedCargoWeight.ToString();
     }

     private static void getCarData(List<string> i_DataOfVehicleToInsert)
     {
          i_DataOfVehicleToInsert.Add(getEnumStringFromEnumValues(typeof(Ex03.GarageLogic.Car.eCarColorOptions)));
          i_DataOfVehicleToInsert.Add(getEnumStringFromEnumValues(typeof(Ex03.GarageLogic.Car.eNumberOfDoors)));
     }
         
     private static void getMotorcycleData(List<string> i_DataOfVehicleToInsert)
     {
          i_DataOfVehicleToInsert.Add(getEnumStringFromEnumValues(typeof(Ex03.GarageLogic.Motorcycle.eMotorcycleLicenseType)));
          i_DataOfVehicleToInsert.Add(auxGetMotorcycleDataGetEngineCapacity());
     }
    
     private static string auxGetMotorcycleDataGetEngineCapacity()
     {
          System.Console.WriteLine("Please enter engine's capacity");
          int intMotorcycleEngineCapacity = getPositiveValidInt();
          
          return intMotorcycleEngineCapacity.ToString();
     }

     private static void getGasCarData(List<string> i_DataOfVehicleToInsert)
     {
          getCarData(i_DataOfVehicleToInsert);
     }

     private static void getElectricCarData(List<string> i_DataOfVehicleToInsert)
     {
          getCarData(i_DataOfVehicleToInsert);
     }

     private static void getGasMotorcycleData(List<string> i_DataOfVehicleToInsert)
     {
          getMotorcycleData(i_DataOfVehicleToInsert);
     }

     private static void getElectricMotorcycleData(List<string> i_DataOfVehicleToInsert)
     {
          getMotorcycleData(i_DataOfVehicleToInsert);
     }
     
     private static void auxGetGeneralVehicleInformation(List<string> i_DataOfVehicleToInsertArray)
     {
          string vehicleModel;
          string wheelManufacturerName;

          System.Console.WriteLine("Please Enter the vehicle's model");
          vehicleModel = System.Console.ReadLine();
          System.Console.WriteLine();
          i_DataOfVehicleToInsertArray.Add(vehicleModel);
          System.Console.WriteLine("Please Enter the wheels manufacturer's name");
          wheelManufacturerName = System.Console.ReadLine();
          System.Console.WriteLine();
          i_DataOfVehicleToInsertArray.Add(wheelManufacturerName);
     }

     private static string auxGetGeneralVehicleInformationGetVehicleModel()
     {
          string stringVehicleModel;

          System.Console.WriteLine("Please Enter the vehicle's model");
          stringVehicleModel = System.Console.ReadLine();
          System.Console.WriteLine();

          return stringVehicleModel;
     }
     
     /// <summary>
     /// This function gets enum type and returns a string representation of an enum, which is selected from a menu
     /// which is generated by the Enum type which lists all possible Enum values
     /// and requests the user of a valid Enum value.     
     /// </summary>
     /// <param name="i_EnumType">a type of an actual enum struct</param>
     /// <returns>string representation of chosen enum from the enum value list</returns>
     private static string getEnumStringFromEnumValues(Type i_EnumType)
     {
          string stringEnumSelection;
          int intEnumValueSelection;
          auxShowEnumOptions(i_EnumType);
          intEnumValueSelection = auxGetGeneralMenuUserSelection(Enum.GetNames(i_EnumType).Length);
          string[] enumNames = Enum.GetNames(i_EnumType);
          stringEnumSelection = enumNames[intEnumValueSelection - 1];
          return stringEnumSelection;
     }

     /// <summary>
     /// This function gets enum type input and generate and show a menu listing enum possible values
     /// </summary>
     /// <param name="i_EnumType"></param>
     private static void auxShowEnumOptions(Type i_EnumType)
     {
          string[] enumNames = Enum.GetNames(i_EnumType);
          string enumToSpacedString;
          int enumValueRange = enumNames.Length;
          System.Console.WriteLine("Please choose an option from 1-{0}" + Environment.NewLine, enumValueRange);
          int foreachCtr = 1;
          foreach (string currentEnumString in enumNames)
          {
               enumToSpacedString = Ex03.GarageLogic.VehicleGenerator.SpaceEnumString(currentEnumString);
               System.Console.WriteLine("{0}. {1}", foreachCtr, enumToSpacedString);
               foreachCtr++;
          }

          System.Console.WriteLine();
     }
    
     /// <summary>
     /// This function recieves input of number of options and gets a valid selection from 1-input param size
     /// </summary>
     /// <param name="i_NumberOfOptions"></param>
     /// <returns></returns>
     private static int auxGetGeneralMenuUserSelection(int i_NumberOfOptions)
     {
          int userMenuSelection;

          do
          {
               try
               {
                    if (!int.TryParse(System.Console.ReadLine(), out userMenuSelection))
                    {
                         throw new FormatException(string.Format("Exception: input was not a legal integer, please enter an integer between 1-{0}" + Environment.NewLine, i_NumberOfOptions));
                    }

                    if (userMenuSelection < 1 || userMenuSelection > i_NumberOfOptions)
                    {
                         System.Console.WriteLine("Illegal input, please enter a number between 1-{0}" + Environment.NewLine, i_NumberOfOptions);
                    }
                    else
                    {
                         break;
                    }
               }
               catch (FormatException fe)
               {
                    System.Console.WriteLine(fe.Message);
               }
          }
          while (true);

          System.Console.WriteLine();

          return userMenuSelection;
     }

     private static Ex03.GarageLogic.FuelTank.eFuelType auxGetFuelType()
     {
          Ex03.GarageLogic.FuelTank.eFuelType fuelType = 0;
          string stringEnumType = getEnumStringFromEnumValues(typeof(Ex03.GarageLogic.FuelTank.eFuelType));
          fuelType = (Ex03.GarageLogic.FuelTank.eFuelType)System.Enum.Parse(typeof(Ex03.GarageLogic.FuelTank.eFuelType), stringEnumType);
          return fuelType;
     }
     
     private static string auxGetValidVehicleNumber()
     {
          string VehicleNumberToCheck = null;

          do
          {
               System.Console.WriteLine("Please enter a valid vehicle nubmer (7 digits)");
               VehicleNumberToCheck = System.Console.ReadLine();
               System.Console.WriteLine();
               if (isValidVehicleNumber(VehicleNumberToCheck))
               {
                    break;
               }
          }
          while (true);

          return VehicleNumberToCheck;
     }

     /// <summary>
     /// This function gets string input and checks if it is a <remarks>combination of 7 digits/letters</remarks> 
     /// </summary>
     /// <param name="i_VehicleNumberToCheck"></param>
     /// <returns>This function returns true if input string is a valid vehicle number</returns>
     private static bool isValidVehicleNumber(string i_VehicleNumberToCheck)
     {
          bool result = true;

          if (i_VehicleNumberToCheck.Length != Ex03.GarageLogic.Vehicle.k_LengthOfValidVehicleNumber)
          {
               result = false;
               System.Console.WriteLine("Vehicle number should be a combination of 7 digits/letters");
          }
          else
          {
               foreach (char CurrentCharInStringInput in i_VehicleNumberToCheck)
               {
                    if (!char.IsLetterOrDigit(CurrentCharInStringInput))
                    {
                         result = false;
                         System.Console.WriteLine("Vehicle number should contain only digits or letters");
                         break;
                    }
               }
          }

          return result;
     }
    
     private static void auxPresentTheNumbersOfTheVehiclesinVehicleList(List<string> i_VehiclesNumbersListByFilter, Ex03.GarageLogic.GarageManager.eVehicleInGarageStatus i_VehicleStatusFilterType)
     {
          if (i_VehiclesNumbersListByFilter.Count == 0)
          {
               System.Console.WriteLine("There are no vehicles in this status - " + i_VehicleStatusFilterType);
          }
          else
          {
               System.Console.WriteLine("Here are all the vehicles in the status - " + i_VehicleStatusFilterType);
               foreach (string vehicleNumber in i_VehiclesNumbersListByFilter)
               {
                    System.Console.WriteLine(vehicleNumber);
               }
          }
     }
     
     private static string auxGetExistingVehicleNumber(Ex03.GarageLogic.GarageManager i_GarageManager)
     {
          string validVehicleNumber;
          string existingVehicleNumber;

          System.Console.WriteLine("Please enter a valid existing vehicle number");
          do
          {
               validVehicleNumber = auxGetValidVehicleNumber();
               if (i_GarageManager.IsVehicleExistsInGarage(validVehicleNumber))
               {
                    existingVehicleNumber = validVehicleNumber;
                    break;
               }
               else
               {
                    System.Console.WriteLine("Vehicle number " + validVehicleNumber + " does not exist in garage, please enter new valid vehicle number");
               }
          }
          while (true);

          return existingVehicleNumber;
     }
}
