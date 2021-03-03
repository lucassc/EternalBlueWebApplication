using EternalBlueWebApplication.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EternalBlueWebApplication.Services
{
    public class EternalBlueService : IEternalBlueService
    {
        private const string firstPassword = "blueb1ueBlue";
        private static string firstPasswordASCIIForm;
        private const string secondPassword = "blue1sF0rev3r";

        public string FirstPassword => firstPassword;

        public string FirstPasswordASCIIForm {
            get {

                if (firstPasswordASCIIForm == null)
                    firstPasswordASCIIForm = GetASCIIString(firstPassword);
                return firstPasswordASCIIForm;
            }
        }

        public string SecondPassword => secondPassword;

        private string GetASCIIString(string str)
        {
            List<string> s = new List<string>();
            s.Add(((byte)str[0]).ToString());
            for (int i = 1; i < str.Count(); i++)
            {
                s.Add(' ' + ((byte)str[i]).ToString());
            }
            var result = string.Concat(s);
            return result;
        }
    }
}
