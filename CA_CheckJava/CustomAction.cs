using Microsoft.Deployment.WindowsInstaller;
using Microsoft.Win32;

namespace CA_CheckJava
{
    public class CustomActions
    {
        /// <summary>
        /// This action checks if a Oracle JRE or OpenJDK is present on the machine and writes it into the
        /// JRE_INSTALLED Property
        /// </summary>
        /// <param name="xiSession"></param>
        /// <returns></returns>
        [CustomAction]
        public static ActionResult CheckJREInstalled(Session xiSession)
        {
            xiSession.Log("Begin CheckJREInstalled");

            // 32bit
            // jre
            string javaJRE32bitPath = @"SOFTWARE\WOW6432Node\JavaSoft\Java Runtime Environment";
            // jdk
            string javaJDK32bitPath = @"SOFTWARE\WOW6432Node\JavaSoft\Java Development Kit";

            // 64bit
            string javaJRE64bitPath = @"SOFTWARE\JavaSoft\Java Runtime Environment";
            string javaJDK64bitPath = @"SOFTWARE\JavaSoft\Java Development Kit";

            bool javaJRE32bit = ExistsRegDir(javaJRE32bitPath, false);
            bool javaJDK32bit = ExistsRegDir(javaJDK32bitPath, false);

            bool javaJRE64bit = ExistsRegDir(javaJRE64bitPath, true);
            bool javaJDK64bit = ExistsRegDir(javaJDK64bitPath, true);

            if (javaJRE64bit || javaJDK64bit)
            {
                xiSession["JRE_INSTALLED"] = "64bit";
            } else if (javaJRE32bit || javaJDK32bit)
            {
                xiSession["JRE_INSTALLED"] = "32bit";
            } else
            {
                xiSession["JRE_INSTALLED"] = "0";
            }

            xiSession.Log("JRE_INSTALLED set to: " + xiSession["JRE_INSTALLED"]);

            return ActionResult.Success;
        }

        private static bool ExistsRegDir(string directory, bool is64Bit)
        {
            var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, (is64Bit ? RegistryView.Registry64 : RegistryView.Registry32));
            return hklm.OpenSubKey(directory) != null;
        }
    }
}
