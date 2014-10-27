namespace Ex03.GarageLogic
{
     using System;
     using System.Collections.Generic;
     using System.Text;

     public class ValueOutOfRangeException : Exception
     {
          private float m_MaxValue;
          private float m_MinValue;

          public ValueOutOfRangeException(float i_MaxValue, float i_MinValue, string i_ErrorMsg) : base(i_ErrorMsg)
          {
               this.m_MaxValue = i_MaxValue;
               this.m_MinValue = i_MinValue;               
          }

          public float MaxValue
          {
               get { return this.m_MaxValue; }
          }

          public float MinValue
          {
               get { return this.m_MinValue; }
          }
     }
}
