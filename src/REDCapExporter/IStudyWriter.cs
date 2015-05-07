using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCF.REDCap
{
    public interface IStudyWriter
    {
        Task Write(string study);
    }
}