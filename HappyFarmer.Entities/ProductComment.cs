using HappyFarmer.Entities.Abstract;

namespace HappyFarmer.Entities
{
    public class ProductComment : IBaseEntity<int>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public FarmerUser User { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string CommentDate { get; set; }
        public int ProductId { get; set; }
        public FarmerProduct Product { get; set; }
    }
}
