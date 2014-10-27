namespace Ex03.GarageLogic
{
     using System;
     using System.Collections.Generic;
     using System.Text;

     public class Motorcycle : Vehicle
     {
          private readonly eMotorcycleLicenseType r_MotorcycleLicennseType;
          private int m_EngineCapacity;

          public Motorcycle(eMotorcycleLicenseType i_MotorcycleLicenseType, int i_EngineCapacity, string i_VehicleNumber, string i_VehicleModel,
                          int i_NumberOfWheels, string i_WheelManufacturerName, float i_MaxAllowedAirPressure)
               : base(i_VehicleNumber, i_VehicleModel, i_NumberOfWheels, i_WheelManufacturerName, i_MaxAllowedAirPressure)
          {
               this.r_MotorcycleLicennseType = i_MotorcycleLicenseType;
               this.m_EngineCapacity = i_EngineCapacity;
          }
          
          public enum eMotorcycleLicenseType
          {
               A1,
               A2,
               A3,
               B,
          }
        
          public override string ToString()
          {
               StringBuilder motorcycleDetails = new StringBuilder();
               motorcycleDetails.Append(base.ToString()).AppendLine().Append(string.Format("Motorcycle's engine capacity: {0}", this.m_EngineCapacity.ToString()))
                                 .AppendLine().Append(string.Format("Motorcycle's license type: {0}", VehicleGenerator.SpaceEnumString(this.LicenseType.ToString()))).AppendLine();

               return motorcycleDetails.ToString();
          }
      
          public eMotorcycleLicenseType LicenseType
          {
               get { return this.r_MotorcycleLicennseType; }
          }

          public int EngineCapacity
          {
               get { return this.m_EngineCapacity; }
          }
     }
}
