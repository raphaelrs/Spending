using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityModels;

namespace EntityModels
{
    public class WalletModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Money { get; set; }
    }
}
