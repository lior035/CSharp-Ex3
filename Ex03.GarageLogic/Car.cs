namespace Ex03.GarageLogic
{
     using System;
     using System.Collections.Generic;
     using System.Text;

     public class Car : Vehicle
     {
          private eCarColorOptions m_CarColor;
          private eNumberOfDoors m_NumberOfDoors;
          
          public Car(eCarColorOptions i_CarColor, eNumberOfDoors i_NumberOfDoors, string i_VehicleNumber, string i_VehicleModel, int i_NumberOfWheels, string i_WheelManufacturerName, float i_MaxAllowedAirPressure)
               : base(i_VehicleNumber, i_VehicleModel, i_NumberOfWheels, i_WheelManufacturerName, i_MaxAllowedAirPressure)
          {
               this.m_CarColor = i_CarColor;
               this.m_NumberOfDoors = i_NumberOfDoors;
          }

          public override string ToString()
          {
               StringBuilder carDetails = new StringBuilder();
               carDetails.Append(base.ToString()).AppendLine().Append(string.Format("Car number of doors: {0}", VehicleGenerator.SpaceEnumString(this.m_NumberOfDoors.ToString())))
                                 .AppendLine().Append(string.Format("Car's Color: {0}", VehicleGenerator.SpaceEnumString(this.m_CarColor.ToString()))).AppendLine();
                                   
               return carDetails.ToString();
          }
        
          public enum eNumberOfDoors
          {
               TwoDoors,
               ThreeDoors,
               FourDoors,
               FiveDoors,
          }

          public enum eCarColorOptions
          {
               Yellow,
               Red,
               Silver,
               White,
          }

          public eNumberOfDoors NumberOfDoors
          {
               get { return this.m_NumberOfDoors; }
          }

          public eCarColorOptions CarColor
          {
               get { return this.m_CarColor; }
          }
     }
}
