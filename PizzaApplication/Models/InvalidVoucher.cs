using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    class InvalidVoucher : Exception
    {
        public InvalidVoucher() : base("Invalid Voucher Entered.")
        {
        }

        public InvalidVoucher(string message) : base(message)
        {
        }

        public InvalidVoucher(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
