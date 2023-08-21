using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotPOS.Utils
{
    internal class customRowFinal
    {
        public String machineNumber;
        public long totalIn;
        public long totalOut;
        public long jackPot;
        public long coinIn;
        public long coinOut;
        public String gameName;
        public String groupName;
        public long net;
        public long coinNet;

        public customRowFinal() { }

        public customRowFinal(String machineNumber, long totalIn, long totalOut, long net, long coinIn, long coinOut, long coinNet, String gameName, String groupName, long jackPot)
        {
            this.machineNumber = machineNumber;
            this.totalIn = totalIn;
            this.totalOut = totalOut;
            this.jackPot = jackPot;
            this.coinIn = coinIn;
            this.coinOut = coinOut;
            this.gameName = gameName;
            this.groupName = groupName;
            this.net = net;
            this.coinNet = coinNet;
        }

    }
}
