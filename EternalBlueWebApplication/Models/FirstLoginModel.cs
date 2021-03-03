using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EternalBlueWebApplication.Models
{
    public class FirstLoginModel
    {
        public bool IsPasswordIncorrect { get; set; }
        public string FirstPasswordASCIIForm { get; set; }
    }
}
