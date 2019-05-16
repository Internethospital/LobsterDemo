using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lobster.Web.Demo.Model
{
    public class Book
    {

        public int Id { get => _id; set => _id = value; }
        public string BookName { get => _bookName; set => _bookName = value; }
        public decimal BuyPrice { get => _buyPrice; set => _buyPrice = value; }
        public int Flag { get => _flag; set => _flag = value; }
        public int WorkId { get => _workId; set => _workId = value; }


        private int _id;
        private string _bookName;
        private decimal _buyPrice;
        private int _flag;
        private int _workId;
    }
}
