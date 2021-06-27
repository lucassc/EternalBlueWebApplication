using System.Collections.Generic;
using EternalBlueWebApplication.Contracts;

namespace EternalBlueWebApplication.Services
{
    public class EternalBlueService : IEternalBlueService
    {
        private const string firstPassword = "blueb1ueBlue";
        private const string secondPassword = "blue1sF0rev3r";
        private string _firstPasswordAsciiForm;

        public string FirstPassword => firstPassword;

        public string FirstPasswordASCIIForm =>
            _firstPasswordAsciiForm ??= GetASCIIString(firstPassword);

        public string SecondPassword => secondPassword;

        private string GetASCIIString(string str)
        {
            var list = new List<string> {((byte) str[0]).ToString()};

            for (var i = 1; i < str.Length; i++)
            {
                list.Add(' ' + ((byte) str[i]).ToString());
            }

            var result = string.Concat(list);

            return result;
        }
    }
}