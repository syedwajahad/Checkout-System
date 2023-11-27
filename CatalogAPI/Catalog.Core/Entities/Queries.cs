using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Entities
{
    public class Queries
    {
        public static string addProduct =
            $"insert into product values(@productId, @productName, @quantity, @price, @imageUrl, @description, @isOutOfStock, 0)";

        public static string getLastInsertedId= @"select max(productId) from product";

        public static string getallItems = $"Select * from Product";

        public static string GetItemById = $"SELECT * FROM product WHERE productId = @productId";

        public static string DeleteItemById = $"Delete from product where productId=@productId";

        public static string UpdateItem =
            "$update product set productId=@productId,productName=@productName,quantity=@quantity,price=@price,imageUrl=@imageUrl,description=@description,isOutStock=@isOutofStock";
    }
}
