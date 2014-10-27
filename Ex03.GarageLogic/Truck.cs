namespace Ex03.GarageLogic
{
     using System;
     using System.Collections.Generic;
     using System.Text;

     public class Truck : Vehicle
     {
          private float m_VolumeCapacity;
          private eTruckCargo m_TruckCargo;
          private float m_MaximumAllowedCargoWeight;

          public Truck(float i_VolumeCapacity, eTruckCargo i_TruckCargo, float i_MaximumAllowedCargoWeight, string i_VehicleNumber, string i_VehicleModel,
                int i_NumberOfWheels, string i_WheelManufacturerName, float i_MaxAllowedAirPressure)
               : base(i_VehicleNumber, i_VehicleModel, i_NumberOfWheels, i_WheelManufacturerName, i_MaxAllowedAirPressure)
          {
               this.m_VolumeCapacity = i_VolumeCapacity;
               this.m_MaximumAllowedCargoWeight = i_MaximumAllowedCargoWeight;
               this.m_TruckCargo = i_TruckCargo;
          }

          public enum eTruckCargo
          {
               ContainsDangerousCargo,
               DoesNotContainDangerousCargo,
          }
   
          public override string ToString()
          {
               StringBuilder truckDetails = new StringBuilder();
               truckDetails.Append(base.ToString()).AppendLine().Append(string.Format("Truck's volume Capacity: {0}", this.m_VolumeCapacity.ToString()))
                                  .AppendLine().Append(string.Format("Truck contains dangerous cargo: {0}", VehicleGenerator.SpaceEnumString(this.m_TruckCargo.ToString()))).AppendLine()
                                  .Append(string.Format("Maximum allowed cargo weight: {0}", this.m_MaximumAllowedCargoWeight.ToString())).AppendLine();

               return truckDetails.ToString();
          }
     }
}
