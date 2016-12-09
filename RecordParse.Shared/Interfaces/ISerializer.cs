using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordParse.Shared.Interfaces
{
    public interface ISerializer<T>
    {
        T Serialize(List<string> items);
    }
}
