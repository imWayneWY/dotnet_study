using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Models
{
    public class FormattingServices
    {
        public string AsReadableDate(DateTime data)
        {
            return data.ToString("d");
        }
    }
}
