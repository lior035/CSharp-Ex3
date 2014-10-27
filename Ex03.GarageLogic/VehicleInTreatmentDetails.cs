namespace Ex03.GarageLogic
{
     using System;
     using System.Collections.Generic;
     using System.Text;
      
     public class VehicleInTreatmentDetails
     {
          private Vehicle m_TheVehicleInTreatment;
          private string m_VehicleOnwerName;
          private string m_VehicleOwnerPhoneNumber;
          private GarageManager.eVehicleInGarageStatus m_VehicleInTreatmentStatus;

          public VehicleInTreatmentDetails(Vehicle i_TheVehicleInTreatment, string i_VehicleOwnerName, string i_VehicleOwnerPhoneNumber)
          {
               m_TheVehicleInTreatment = i_TheVehicleInTreatment;
               m_VehicleOnwerName = i_VehicleOwnerName;
               m_VehicleOwnerPhoneNumber = i_VehicleOwnerPhoneNumber;
               m_VehicleInTreatmentStatus = GarageManager.eVehicleInGarageStatus.InRepair;
          }

          /// <summary>
          /// Overriding <see cref="Object.ToString"/>
          /// This function generates a string which consists of the class's member details.
          /// It calls Vehicle.ToString() which returns a string representation of the specific vehicle details and adds it 
          /// to vehicle owner number, phone and car garage-status.
          /// </summary>
          /// <returns>a string representation of a vehicle in treatment's full details</returns>
          public override string ToString()
          {
               StringBuilder vehicleInTreatmentDetails = new StringBuilder();
               vehicleInTreatmentDetails.Append(string.Format("Owner name: {0}", m_VehicleOnwerName)).AppendLine()
                                    .Append(string.Format("Vehicle owner phone number: {0}", m_VehicleOwnerPhoneNumber)).AppendLine()
                                    .Append(string.Format("Vehicle status: {0}", VehicleGenerator.SpaceEnumString(m_VehicleInTreatmentStatus.ToString()))).AppendLine()
                                    .Append(m_TheVehicleInTreatment.ToString());

               return vehicleInTreatmentDetails.ToString();
          }

          public Vehicle TheVehicleInTreatment
          {
               get { return m_TheVehicleInTreatment; }
          }

          public string OwnerName 
          {
               get { return m_VehicleOnwerName; }
          }

          public string VehiclePhoneNumber
          {
               get { return m_VehicleOwnerPhoneNumber; }
          }

          public GarageManager.eVehicleInGarageStatus VehicleInTreatmentStatus
          {
               get { return m_VehicleInTreatmentStatus; }
               set { m_VehicleInTreatmentStatus = value; }
          }
     }
}
