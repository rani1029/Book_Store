using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreModel
{
    public class CartModel
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int BookQantity { get; set; }
    }
}
