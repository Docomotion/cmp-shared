using System;
using System.IO;
using Microsoft.Win32;

namespace Docomotion.Shared.Utils
{
    public class AfRegistry
    {
        public static string GetValueFromSoftwareSection(string productBasePath, string valueName, out string errorMessage, RegistryView registryView = RegistryView.Default)
        {
            errorMessage = string.Empty;
            string returnValue = string.Empty;

            try
            {
                RegistryKey localMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView);
                RegistryKey softwareEntry = localMachine.OpenSubKey(Path.Combine("Software", productBasePath));

                if (softwareEntry != null)
                {
                    returnValue = softwareEntry.GetValue(valueName) as string;

                    if (returnValue == null)
                    {
                        errorMessage = string.Format("cannot extract '{0}' value from the registry software section", valueName);
                    }
                }
                else
                {
                    errorMessage = string.Format("cannot find '{0}' directory in the registry software section", productBasePath);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return returnValue;
        }
    }
}
