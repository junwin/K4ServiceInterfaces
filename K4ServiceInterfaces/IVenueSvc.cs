using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaiTrade.Interfaces;

namespace K4ServiceInterface
{
    public interface IVenueSvc
    {
        /// <summary>
        /// Get the driver for some venue
        /// </summary>
        /// <param name="venue"></param>
        /// <returns></returns>
        IDriver GetDriver(string venueId);
    }
}
