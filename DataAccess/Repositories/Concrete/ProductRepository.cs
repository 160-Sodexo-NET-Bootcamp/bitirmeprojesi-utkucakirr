using AutoMapper;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using Entities.DataModel;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class ProductRepository:GenericRepository<Product>,IProductRepository
    {
        private readonly IMapper _mapper;
        public ProductRepository(ProjectDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Getting products that belongs to a specific category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public CategoryViewModelWithProducts GetByCategoryId(Category category)
        { 
            var listOfProducts = base.GetListByExpression(x => x.CategoryId == category.Id);

            var list = _mapper.Map<List<Product>, List<ProductViewModel>>(listOfProducts);

            CategoryViewModelWithProducts model = new CategoryViewModelWithProducts
            {
                CategoryId = category.Id, CategoryName = category.Name, Products = list
            };
            return model;
        }
        
        /// <summary>
        /// Mapping process for percentage offer.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<OfferModel> MakeChange(OfferPercentageModel entity)
        {
            var product = await base.GetByExpression(x => x.Id == entity.ProductId);
            var item = _mapper.Map<OfferAddModel>(entity);
            item.OfferedPrice =(entity.Percentage * product.Price)/100;
            return item;
        }

        /// <summary>
        /// Checking products offerablity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<Offer> OfferCheck(OfferModel entity)
        {
            var product = await base.GetByExpression(x => x.Id == entity.ProductId);
            if((bool)product.IsOfferable)
            {
                base.Update(product);
                var item = _mapper.Map<Offer>(entity);
                item.OwnerId = product.UserId;
                return item;
            }
            return null;
        }

        /// <summary>
        /// Checking if the product has already been sold.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> SellCheck(int id)
        {
            var product = await base.GetByExpression(x => x.Id == id);
            if((bool)product.IsSold)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Selling a product process.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task SellProduct(int id)
        {
            var product = await base.GetByExpression(x => x.Id == id);
            product.IsSold = true;
            product.IsOfferable = false;
            base.Update(product);
        }

        /// <summary>
        /// Offer confirmed process.
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public async Task OfferConfirmed(int ProductId)
        {
            var product = await base.GetByIdAsync(ProductId);
            product.IsOfferable = false;
            product.IsSold = true;
            base.Update(product);
        }

        /// <summary>
        /// Checking if that product belongs to user for update process.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateWithConfirmation(int UserId, ProductUpdateModel entity)
        {
            var product = await base.GetByIdAsync(entity.Id);
            if(product is null || product.UserId!=UserId)
            {
                return false;
            }
            var item = _mapper.Map<Product>(entity);
            item.UserId = UserId;
            base.Update(item);
            return true;
        }

        /// <summary>
        /// Checking if that product belongs to user for delete process.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteWithConfirmation(int UserId, int id)
        {
            var product = await base.GetByIdAsync(id);
            if(product is null || product.UserId!=UserId)
            {
                return false;
            }
            base.Delete(id);
            return true;
        }
    }
}
