using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5._221ReadersWriters
{
    class Record
    {
        public string numThread;
        public string numRecord;
        public Record(string numThread, string numRecord)
        {
            this.numThread = numThread;
            this.numRecord = numRecord;
        }
        public override string ToString()
        {
            return "{Thread number = " + numThread +
                    ", Record number = " + numRecord + "}";
        }

    }
}
