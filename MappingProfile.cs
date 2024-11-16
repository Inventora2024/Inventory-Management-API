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

            // StockOrder Mappings
            CreateMap<StockOrder, StockOrderProductsDTO>();
            CreateMap<StockOrderItem, StockOrderProductItemDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));

            // CustomerOrder Mappings
            CreateMap<CustomerOrder, CustomerOrderDTO>().ReverseMap();
            CreateMap<CustomerOrder, CustomerOrderProductsDTO>();
            CreateMap<CustomerOrderItem, CustomerOrderProductItemDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));

            // Create Sale Mappings
            CreateMap<CreateSaleDTO, CustomerOrder>();
            CreateMap<CreateSaleItemDTO, CustomerOrderItem>();

            // Create Display User Mappings
            CreateMap<User, UserDisplayDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Contact, opt => opt.MapFrom(src => src.Contact))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

            CreateMap<User, LoginReturnDTO>();
            CreateMap<LoginReturnDTO, User>();
        }

    }

}
