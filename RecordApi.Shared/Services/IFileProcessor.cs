using RecordApi.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordApi.Shared.Services
{
    public interface IFileProcessor
    {

        IEnumerable<IRecord> Records { get; }
       




    }
}
