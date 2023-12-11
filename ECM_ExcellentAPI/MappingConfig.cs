using AutoMapper;
using ECM_ExcellentAPI.Model;
using ECM_ExcellentAPI.Model.Dto;

namespace ECM_ExcellentAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<Company, CompanyCreateDTO>().ReverseMap();
            CreateMap<Company, CompanyUpdateDTO>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
            CreateMap<Category, CategoryUpdateDTO>().ReverseMap();

            CreateMap<Supplier, SupplierDTO>().ReverseMap();
            CreateMap<Supplier, SupplierCreateDTO>().ReverseMap();
            CreateMap<Supplier, SupplierUpdateDTO>().ReverseMap();

            CreateMap<CategoryType, CategoryTypeDTO>().ReverseMap();
            CreateMap<CategoryType, CategoryTypeCreateDTO>().ReverseMap();
            CreateMap<CategoryType, CategoryTypeUpdateDTO>().ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();

            CreateMap<Product_Rate_History, ProductRateHistoryDTO>().ReverseMap();
            CreateMap<Product_Rate_History, ProductRateHistoryCreateDTO>().ReverseMap();
            CreateMap<Product_Rate_History, ProductRateHistoryUpdateDTO>().ReverseMap();

            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Customer, CustomerCreateDTO>().ReverseMap();
            CreateMap<Customer, CustomerUpdateDTO>().ReverseMap();

            CreateMap<CustomerAddress, CustomerAddressDTO>().ReverseMap();
            CreateMap<CustomerAddress, CustomerAddressCreateDTO>().ReverseMap();
            CreateMap<CustomerAddress, CustomerAddressUpdateDTO>().ReverseMap();

            CreateMap<PurchaseOrder, PurchaseOrderDTO>().ReverseMap();
            CreateMap<PurchaseOrder, PurchaseOrderCreateDTO>().ReverseMap();
            CreateMap<PurchaseOrder, PurchaseOrderUpdateDTO>().ReverseMap();

            CreateMap<PurchaseOrderDetail, PurchaseOrderDetailDTO>().ReverseMap();
            CreateMap<PurchaseOrderDetail, PurchaseOrderDetailCreateDTO>().ReverseMap();
            CreateMap<PurchaseOrderDetail, PurchaseOrderDetailUpdateDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, OrderCreateDTO>().ReverseMap();
            CreateMap<Order, OrderUpdateDTO>().ReverseMap();

            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailCreateDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailUpdateDTO>().ReverseMap();

            CreateMap<OrderStatus, OrderStatusDTO>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusCreateDTO>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusUpdateDTO>().ReverseMap();
        }
    }
}
