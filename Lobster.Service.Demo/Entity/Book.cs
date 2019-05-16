using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lobster.Service.Demo.Entity
{
    [Table("Books")]
    public class Book
    {
        
        [Key]
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
