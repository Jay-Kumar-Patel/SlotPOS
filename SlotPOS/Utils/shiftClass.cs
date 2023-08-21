using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotPOS.Utils
{
    internal class shiftClass
    {
        public long starting_drawer;
        public long totalIn;
        public long totalOut;
        public long promo;
        public long match_play;
        public long fill;
        public long drop;
        public long expense;
        public long total_tickets_out;
        public long login_id;
        public long shift_id;
        public String duration;
        public String username;
        public long net;

        public shiftClass(long starting_drawer, long totalIn, long totalOut, long promo, long match_play, long fill, long drop, long expense, long total_tickets_out, long login_id, long shift_id, String username, String duration, long net)
        {
            this.starting_drawer = starting_drawer;
            this.totalIn = totalIn;
            this.totalOut = totalOut;
            this.promo = promo;
            this.match_play = match_play;
            this.fill = fill;
            this.drop = drop;
            this.expense = expense;
            this.total_tickets_out = total_tickets_out;
            this.login_id = login_id;
            this.shift_id = shift_id;
            this.username = username;
            this.duration = duration;
            this.net = net;
        }

    }
}
