using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REDCapExporter
{
    public interface IStudyWriter
    {
        Task Write(REDCapStudy study);
    }
}