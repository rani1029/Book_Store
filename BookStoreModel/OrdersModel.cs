using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreModel
{
    public class OrdersModel
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int CartId { get; set; }
        public int AddressId { get; set; }
        //public int OrderValue { get; set; }
        //public int BookQuantity { get; set; }
        public BookModel GetBook { get; set; }

    }
}
