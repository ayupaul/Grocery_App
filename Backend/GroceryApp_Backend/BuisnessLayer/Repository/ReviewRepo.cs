using AutoMapper;
using BuisnessLayer.IRepository;
using BuisnessLayer.Model;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Repository
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public ReviewRepo(AppDbContext appDbContext,IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        public async Task AddReviewAsync(ReviewModel model)
        {
            var review = mapper.Map<ReviewEntity>(model);
            await appDbContext.Reviews.AddAsync(review);
            await appDbContext.SaveChangesAsync();
        }

        public async Task<List<ReviewEntity>> GetAllReviewsByProductIdAsync(int id)
        {
            var review = await appDbContext.Reviews.Where(x => x.ProductId == id).ToListAsync();
            return review;
        }
    }
}
