using AutoMapper;
using Inventory_Management_API.DTOs;
using Inventory_Management_API.Models;
namespace Inventory_Management_API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CustomerOrder Mappings
            CreateMap<CustomerOrder, CustomerOrderDTO>().ReverseMap();

            // Product Mappings
            CreateMap<Product, ProductDTO>().ReverseMap();

            // CustomerOrderItem Mappings
            CreateMap<CustomerOrderItem, CustomerOrderItemDTO>().ReverseMap();

            // ProductCategory Mappings
            CreateMap<ProductCategory, ProductCategoryDTO>().ReverseMap();

            // Shipment Mappings
            CreateMap<Shipment, ShipmentDTO>().ReverseMap();

            // StockOrder Mappings
            CreateMap<StockOrder, StockOrderDTO>().ReverseMap();

            // StockOrderItem Mappings
            CreateMap<StockOrderItem, StockOrderItemDTO>().ReverseMap();

            // Supplier Mappings
            CreateMap<Supplier, SupplierDTO>().ReverseMap();

            // User Mappings
            CreateMap<User, UserDTO>().ReverseMap();

        }
    }
}
