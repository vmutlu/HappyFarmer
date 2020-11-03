using HappyFarmer.DataAccess.Concrete;
using HappyFarmer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HappyFarmer.DataAccess
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new FarmerContext();
            if (context.Database.GetPendingMigrations().Count() > 0)
                context.Database.Migrate();

            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.FarmerCategory.Count() == 0)
                    context.FarmerCategory.AddRange(Categories);

                if (context.FarmerUser.Count() == 0)
                    context.FarmerUser.AddRange(farmerUsers);

                if (context.Products.Count() == 0)
                {
                    context.Products.AddRange(Products);
                    context.AddRange(productCategory);
                }
                if (context.Blogs.Count() == 0)
                {
                    context.Blogs.AddRange(farmerBlogs);
                }

                if (context.AboutUs.Count() == 0)
                {
                    context.AboutUs.AddRange(farmerAboutUs);
                }
                if (context.Orders.Count() == 0)
                    context.Orders.AddRange(farmerOrders);

                if (context.OrderItems.Count() == 0)
                    context.OrderItems.AddRange(farmerOrderItems);
                context.SaveChanges();
            }
        }

        private static FarmerCategory[] Categories =
       {
            //new FarmerCategory(){Name="Büyük Baş"},
            //new FarmerCategory(){Name="Küçük Baş"},
            //new FarmerCategory(){Name="Tarım"},
            //new FarmerCategory(){Name="Bahçeden Eve"},
            //new FarmerCategory(){Name="Müşteri Ürünleri"},
            //new FarmerCategory(){Name="Nakliyeciler"}
            new FarmerCategory(){Name="Küçük Baş"},
            new FarmerCategory(){Name="Büyük Baş"},
            new FarmerCategory(){Name="Kümes Hayvancılığı"},
            new FarmerCategory(){Name="Evcil Hayvancılık"},
            new FarmerCategory(){Name="Süt Ürünleri"},
            new FarmerCategory(){Name="Hayvansal Gıda"},
            new FarmerCategory(){Name="Traktör "},
            new FarmerCategory(){Name="Tarım "},
            new FarmerCategory(){Name="Ekipmanlar "},
            new FarmerCategory(){Name="Sebzeler "},
            new FarmerCategory(){Name="Meyveler "},
            new FarmerCategory(){Name="Tahıl Ürünleri "},
            new FarmerCategory(){Name="Yem & Otsu Bitkiler "},
        };

        private static FarmerProduct[] Products =
        {
            new FarmerProduct(){Name="Büyük Baş",Description="Büyük",Price=1000,UserId=1,FarmerDeclareType=10,AnnouncementDate=DateTime.Now.ToLongDateString(),ImageUrl="ĞVÖşğe.jpg",PermissionToSell = true},
            new FarmerProduct(){Name="Küçük Baş",Description="Büyük",Price=1000,UserId=1,FarmerDeclareType=11,AnnouncementDate=DateTime.Now.ToLongDateString(),ImageUrl="ĞVÖşğe.jpg",PermissionToSell = true},
            new FarmerProduct(){Name="Kümes Hayvanı",Description="Büyük",Price=1000,UserId=1,FarmerDeclareType=12,AnnouncementDate=DateTime.Now.ToLongDateString(),ImageUrl="ĞVÖşğe.jpg",PermissionToSell = true},
            new FarmerProduct(){Name="Evcil Hayvan",Description="Büyük",Price=1000,UserId=1,FarmerDeclareType=13,AnnouncementDate=DateTime.Now.ToLongDateString(),ImageUrl="ĞVÖşğe.jpg",PermissionToSell = true},
            new FarmerProduct(){Name="Süt",Description="Büyük",Price=1000,UserId=1,FarmerDeclareType=14,AnnouncementDate=DateTime.Now.ToLongDateString(),ImageUrl="ĞVÖşğe.jpg",PermissionToSell = true},
            new FarmerProduct(){Name="Hayvansal yiyecek",Description="Büyük",Price=1000,UserId=1,FarmerDeclareType=15,AnnouncementDate=DateTime.Now.ToLongDateString(),ImageUrl="ĞVÖşğe.jpg",PermissionToSell = true},
            new FarmerProduct(){Name="Traktor",Description="Büyük",Price=1000,UserId=1,FarmerDeclareType=20,AnnouncementDate=DateTime.Now.ToLongDateString(),ImageUrl="ĞVÖşğe.jpg",PermissionToSell = true},
            new FarmerProduct(){Name="Tarla",Description="Büyük",Price=1000,UserId=1,FarmerDeclareType=21,AnnouncementDate=DateTime.Now.ToLongDateString(),ImageUrl="ĞVÖşğe.jpg",PermissionToSell = true },
            new FarmerProduct(){Name="Ekipman",Description="Büyük",Price=1000,UserId=1,FarmerDeclareType=22,AnnouncementDate=DateTime.Now.ToLongDateString(),ImageUrl="ĞVÖşğe.jpg",PermissionToSell = true},
            new FarmerProduct(){Name="Sebze",Description="Büyük",Price=1000,UserId=1,FarmerDeclareType=23,AnnouncementDate=DateTime.Now.ToLongDateString(),ImageUrl="ĞVÖşğe.jpg",PermissionToSell = true},
            new FarmerProduct(){Name="Meyve ",Description="Büyük",Price=1000,UserId=1,FarmerDeclareType=24,AnnouncementDate=DateTime.Now.ToLongDateString(),ImageUrl="ĞVÖşğe.jpg",PermissionToSell = true},
            new FarmerProduct(){Name="Tahıl ",Description="Büyük",Price=1000,UserId=1,FarmerDeclareType=25,AnnouncementDate=DateTime.Now.ToLongDateString(),ImageUrl="ĞVÖşğe.jpg",PermissionToSell = true},
            new FarmerProduct(){Name="Yem ve Otsu bitki ",Description="Büyük",Price=1000,UserId=1,FarmerDeclareType=26,AnnouncementDate=DateTime.Now.ToLongDateString(),ImageUrl="ĞVÖşğe.jpg",PermissionToSell = true}
        };

        private static FarmerProductCategory[] productCategory =
       {
            new FarmerProductCategory(){Product=Products[0],Category=Categories[0]},
            new FarmerProductCategory(){Product=Products[0],Category=Categories[1]},
            new FarmerProductCategory(){Product=Products[1],Category=Categories[3]},
            new FarmerProductCategory(){Product=Products[1],Category=Categories[1]},
            new FarmerProductCategory(){Product=Products[2],Category=Categories[0]},
            new FarmerProductCategory(){Product=Products[2],Category=Categories[1]},
            new FarmerProductCategory(){Product=Products[3],Category=Categories[0]},
            new FarmerProductCategory(){Product=Products[3],Category=Categories[2]},
            new FarmerProductCategory(){Product=Products[4],Category=Categories[0]},
            new FarmerProductCategory(){Product=Products[4],Category=Categories[4]},
            new FarmerProductCategory(){Product=Products[5],Category=Categories[0]},
            new FarmerProductCategory(){Product=Products[5],Category=Categories[5]}
        };

        private static FarmerUser[] farmerUsers =
      {
            new FarmerUser(){Id = 1, Name = "Veysel",Surname="Mutlu",Email="veysel_mutlu42@hotmail.com",Password="123456",RePassword="123456",UserType=1,Authority=true,UserState=true},
            new FarmerUser(){Id = 2, Name = "Veli",Surname="Mutlu",Email="veysel_mutlu42@gmail.com",Password="123456",RePassword="123456",UserType=0,Authority=false,UserState=true},
        };

        private static FarmerBlog[] farmerBlogs =
        {
            new FarmerBlog(){UserId=1}
        };

        private static FarmerAboutUs[] farmerAboutUs =
        {
            new FarmerAboutUs(){Title="sdfsdf"}
        };

        private static FarmerOrder[] farmerOrders =
        {
            new FarmerOrder(){Id=1, UserId=1}
        };
        private static FarmerOrderItem[] farmerOrderItems =
        {
            new FarmerOrderItem(){ProductId=1,OrderId=1}
        };
    }
}
