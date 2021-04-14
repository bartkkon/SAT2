using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Tasks.Calculation_Function
{
    public class Functions
    {
        /// <summary>
        /// Check if string is PNC number
        /// </summary>
        /// <param name="Value">Value to check</param>
        /// <returns>True if Value is PNC Number</returns>
        public bool PNCCheck(string Value)
        {
            bool PNC = false;

            if (Value.Length == 9)
            {
                if (Value.Remove(3) == "911" || Value.Remove(3) == "999")
                {
                    PNC = true;
                }
            }

            return PNC;
        }

        /// <summary>
        /// Check if string in ANC number
        /// </summary>
        /// <param name="Value">Value to check</param>
        /// <returns>True if Value is ANC number</returns>
        public bool ANCCheck(string Value)
        {
            bool ANC = false;

            if (Value.Length == 9)
            {
                if (Value.Remove(3) != "911" || Value.Remove(3) != "999")
                {
                    ANC = true;
                }
            }

            return ANC;
        }

        /// <summary>
        /// Check if Value is Platform coding
        /// </summary>
        /// <param name="Value"> Value to check</param>
        /// <returns>True if  value is Platform coding</returns>
        public bool PlatformCheck(string Value)
        {
            bool Platform = false;

            if (Value.Length < 9)
            {
                if (Value.Length == 3)
                {
                    Platform = Value switch
                    {
                        "DMD" => true,
                        "D45" => true,
                        "ALL" => true,
                        _ => false,
                    };
                }
                else
                {
                    Platform = Value.Remove(3) switch
                    {
                        "DMD" => true,
                        "D45" => true,
                        _ => false,
                    };
                }
            }

            return Platform;
        }
    }
}
