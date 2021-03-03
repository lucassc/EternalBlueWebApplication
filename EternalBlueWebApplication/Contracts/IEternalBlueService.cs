using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EternalBlueWebApplication.Contracts
{
    public interface IEternalBlueService
    {
        string FirstPassword { get; }
        string FirstPasswordASCIIForm { get; }
        string SecondPassword { get; }
    }
}
